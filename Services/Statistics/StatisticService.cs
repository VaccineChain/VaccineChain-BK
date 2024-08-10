using AutoMapper;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.Statistic;
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
        private readonly IMapper _mapper;
        private readonly IStatisticRepository _statisticRepository;

        public StatisticService(IMapper mapper, IStatisticRepository statisticRepository)
        {
            _mapper = mapper;
            _statisticRepository = statisticRepository;
        }

        public StatisticLogsByVaccineId GetStatisticLog(string vaccineId)
        {

            StatisticLogsByVaccineId statisticLogs = _statisticRepository.GetStatisticsByVaccineId(vaccineId);
            return statisticLogs;
        }

        public List<StatisticAreaChart> GetStatisticsForAreaChart(string vaccineId)
        {
            List<StatisticAreaChart> statisticLogs = _statisticRepository.GetStatisticsForAreaChart(vaccineId);
            return statisticLogs;
        }
    }
}
