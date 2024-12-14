using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.PortableExecutable;
using vaccine_chain_bk.Constraints;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.Statistic;
using vaccine_chain_bk.DTO.Vaccine;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Statistics
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly ApplicationDbContext _context;

        public StatisticRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<VaccineDeviceStatus> GetVaccineStatistics()
        {
            var result = (from l in _context.Logs
                          join v in _context.Vaccines on l.VaccineId equals v.VaccineId
                          join d in _context.Devices on l.DeviceId equals d.DeviceId
                          group l by new { v.VaccineId, v.VaccineName, l.Status } into grouped
                          select new VaccineDeviceStatus
                          {
                              VaccineId = grouped.Key.VaccineId,
                              VaccineName = grouped.Key.VaccineName,
                              NumberOfDevices = grouped.Select(g => g.DeviceId).Distinct().Count(),
                              Status = grouped.Key.Status.ToString()
                          }).OrderBy(stat => stat.VaccineId).ToList();

            return result;
        }


        public List<StatisticAreaChart> GetStatisticsForAreaChart(string vaccineId)
        {
            var logs = _context.Logs
                .Where(log => log.VaccineId == vaccineId && log.Value != null)
                .OrderBy(log => log.Timestamp)
                .ToList();

            if (logs == null || logs.Count == 0)
            {
                return new List<StatisticAreaChart>();
            }

            var result = logs
                .GroupBy(log => log.DeviceId)
                .Select(group => new StatisticAreaChart
                {
                    DeviceId = group.Key,
                    SensorValue = group.Select(log => new SensorValue
                    {
                        Value = log.Value.Value,
                        Timestamp = log.Timestamp
                    }).ToList()
                }).ToList();

            return result;
        }

        public StatisticLogsByVaccineId GetStatisticsByVaccineId(List<Log> logs, Vaccine vaccine)
        {
            var validLogs = logs.Where(l => l.Value.HasValue && l.Timestamp.HasValue).ToList(); //Tránh Các Tính Toán Dư Thừa
            var highestLog = validLogs.MaxBy(l => l.Value); //Sử dụng LINQ cho các Phép Toán Min/Max
            var lowestLog = validLogs.MinBy(l => l.Value);

            var statistics = new StatisticLogsByVaccineId
            {
                Vaccine = new VaccineDto
                {
                    VaccineId = vaccine.VaccineId,
                    VaccineName = vaccine.VaccineName,
                    Manufacturer = vaccine.Manufacturer,
                    BatchNumber = vaccine.BatchNumber,
                    ExpirationDate = vaccine.ExpirationDate,
                    CreatedAt = vaccine.CreatedAt
                },
                DeviceId = validLogs.Select(l => l.DeviceId).Distinct().ToList(),
                AverageValue = validLogs.Average(l => l.Value.Value),
                HighestValue = highestLog.Value.Value,
                TimeHighestValue = highestLog.Timestamp.Value,
                LowestValue = lowestLog.Value.Value,
                TimeLowestValue = lowestLog.Timestamp.Value,
                DateRangeStart = validLogs.Min(l => l.Timestamp).Value,
                DateRangeEnd = validLogs.Max(l => l.Timestamp).Value,
                NumberRecords = validLogs.Count
            };

            return statistics;
        }

        public List<VaccinesTemperatureRangeDto> VaccinesTemperatureRange()
        {
            List<VaccinesTemperatureRangeDto> result = (from l in _context.Logs
                                                        join d in _context.Devices on l.DeviceId equals d.DeviceId
                                                        join v in _context.Vaccines on l.VaccineId equals v.VaccineId
                                                        join ds in _context.Doses on v.VaccineId equals ds.VaccineId
                                                        where d.SensorType == 0 && l.Value != null
                                                        group new { l.Value } by v.VaccineName into g
                                                        select new VaccinesTemperatureRangeDto
                                                        {
                                                            VaccineName = g.Key,
                                                            HighestTemperature = g.Max(x => x.Value),
                                                            LowestTemperature = g.Min(x => x.Value)
                                                        }).ToList();

            return result;
        }

        public List<DataCollectionStatusDto> DataCollectionStatus()
        {
            // Devices
            var totalDevices = _context.Devices.Count();
            var devicesCollecting = _context.Logs
                .Select(l => l.DeviceId)
                .Distinct()
                .Count();
            var devicesCompleted = totalDevices - devicesCollecting;

            // Vaccines
            var totalVaccines = _context.Vaccines.Count();
            var vaccinesCollecting = _context.Logs
                .Select(l => l.VaccineId)
                .Distinct()
                .Count();
            var vaccinesCompleted = totalVaccines - vaccinesCollecting;

            // Combine results
            List<DataCollectionStatusDto> result = new List<DataCollectionStatusDto>
            {
                new DataCollectionStatusDto
                {
                    Category = "Devices",
                    Collecting = devicesCollecting,
                    Completed = devicesCompleted
                },
                new DataCollectionStatusDto
                {
                    Category = "Vaccines",
                    Collecting = vaccinesCollecting,
                    Completed = vaccinesCompleted
                }
            };

            return result;
        }

        public ConnectionOverviewDto ConnectionOverview()
        {
            // Total distinct connections (VaccineId, DeviceId) in Logs
            var totalConnection = _context.Logs
                .Select(l => new { l.VaccineId, l.DeviceId })
                .Distinct()
                .Count();

            // Devices not connected (not appearing in Logs)
            var notConnectedDevices = _context.Devices
                .GroupJoin(_context.Logs,
                           device => device.DeviceId,
                           log => log.DeviceId,
                           (device, logs) => new { Device = device, Logs = logs })
                .Where(group => !group.Logs.Any())
                .Count();

            // Vaccines not connected (not appearing in Logs)
            var notConnectedVaccines = _context.Vaccines
                .GroupJoin(_context.Logs,
                           vaccine => vaccine.VaccineId,
                           log => log.VaccineId,
                           (vaccine, logs) => new { Vaccine = vaccine, Logs = logs })
                .Where(group => !group.Logs.Any())
                .Count();

            // Prepare response
            ConnectionOverviewDto result = new ConnectionOverviewDto
            {
                TotalConnection = totalConnection,
                NotConnectedDevice = notConnectedDevices,
                NotConnectedVaccine = notConnectedVaccines
            };

            return result;
        }
    }
}
