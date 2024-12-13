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
                              Status = (EStatus)grouped.Key.Status
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

    }
}
