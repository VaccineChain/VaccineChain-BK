using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.Statistic;
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

        public StatisticLogsByVaccineId GetStatisticsByVaccineId(string vaccineId)
        {
            List<Log> logs = _context.Logs
            .Where(log => log.VaccineId == vaccineId && log.Value != null)
            .ToList();

            if (logs == null || logs.Count == 0)
            {
                throw new NotFoundException("Not Found Logs");
            }

            var highestLog = logs.OrderByDescending(l => l.Value).First();
            var lowestLog = logs.OrderBy(l => l.Value).First();

            var statistics = new StatisticLogsByVaccineId
            {
                VaccineId = vaccineId,
                DeviceId = logs.Select(l => l.DeviceId).Distinct().ToList(),
                AverageValue = logs.Average(l => l.Value.Value),
                HighestValue = highestLog.Value.Value,
                TimeHighestValue = highestLog.Timestamp.Value,
                LowestValue = lowestLog.Value.Value,
                TimeLowestValue = lowestLog.Timestamp.Value,
                DateRangeStart = logs.Min(l => l.Timestamp),
                DateRangeEnd = logs.Max(l => l.Timestamp),
                NumberRecords = logs.Count
            };

            return statistics;
        }
    }
}
