using AutoMapper;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.Statistic;
using vaccine_chain_bk.DTO.Vaccine;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Devices;
using vaccine_chain_bk.Repositories.Logs;
using vaccine_chain_bk.Repositories.Statistics;
using vaccine_chain_bk.Repositories.Vaccines;
using vaccine_chain_bk.Services.Devices;
using vaccine_chain_bk.Services.Vaccines;

namespace vaccine_chain_bk.Services.Statistics
{
    public class StatisticService : IStatisticService
    {
        private readonly IStatisticRepository _statisticRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly ILogRepository _logRepository;


        public StatisticService(IStatisticRepository statisticRepository, IVaccineRepository vaccineRepository, ILogRepository logRepository)
        {
            _statisticRepository = statisticRepository;
            _vaccineRepository = vaccineRepository;
            _logRepository = logRepository;
        }

        public StatisticLogsByVaccineId GetStatisticLog(string vaccineId)
        {
            var vaccine = _vaccineRepository.GetVaccineById(vaccineId);
            if (vaccine == null)
            {
                throw new NotFoundException("Vaccine not found");
            }

            var logs = _logRepository.GetLogByVaccineId(vaccineId);
            if (!logs.Any())
            {
                throw new NotFoundException("No logs found");
            }           

            return _statisticRepository.GetStatisticsByVaccineId(logs, vaccine);
        }
        public List<VaccineDeviceStatus> GetVaccineStatistics()
        {
            return _statisticRepository.GetVaccineStatistics();
        }

        public List<StatisticAreaChart> GetStatisticsForAreaChart(string vaccineId)
        {
            List<StatisticAreaChart> statisticLogs = _statisticRepository.GetStatisticsForAreaChart(vaccineId);
            return statisticLogs;
        }
    }
}
