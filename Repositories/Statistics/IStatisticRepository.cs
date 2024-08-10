using vaccine_chain_bk.DTO.Statistic;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Statistics
{
    public interface IStatisticRepository
    {
        StatisticLogsByVaccineId GetStatisticsByVaccineId(string vaccineId);
        List<StatisticAreaChart> GetStatisticsForAreaChart(string vaccineId);
    }
}
