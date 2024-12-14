using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.Statistic;
using vaccine_chain_bk.DTO.Vaccine;

namespace vaccine_chain_bk.Services.Statistics
{
    public interface IStatisticService
    {
        StatisticLogsByVaccineId GetStatisticLog(string vaccineId);
        List<StatisticAreaChart> GetStatisticsForAreaChart(string vaccineId);
        List<VaccineDeviceStatus> GetVaccineStatistics();
        List<VaccinesTemperatureRangeDto> VaccinesTemperatureRange();
        List<DataCollectionStatusDto> DataCollectionStatus();
        ConnectionOverviewDto ConnectionOverview();
    }
}
