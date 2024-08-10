using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.Vaccine;

namespace vaccine_chain_bk.DTO.Statistic
{
    public class StatisticAreaChart
    {
        public List<SensorValue> SensorValue { get; set; }
        public string DeviceId { get; set; }
    }

    public class SensorValue { 
        public double Value { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
