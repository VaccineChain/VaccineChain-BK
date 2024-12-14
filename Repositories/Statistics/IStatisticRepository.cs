using vaccine_chain_bk.DTO.Statistic;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Statistics
{
    public interface IStatisticRepository
    {
        StatisticLogsByVaccineId GetStatisticsByVaccineId(List<Log> logs, Vaccine vaccine);
        List<StatisticAreaChart> GetStatisticsForAreaChart(string vaccineId);
        List<VaccineDeviceStatus> GetVaccineStatistics();
        List<VaccinesTemperatureRangeDto> VaccinesTemperatureRange();
        List<DataCollectionStatusDto> DataCollectionStatus();
        ConnectionOverviewDto ConnectionOverview();
    }
}
