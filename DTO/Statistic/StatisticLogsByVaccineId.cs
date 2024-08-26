
using vaccine_chain_bk.DTO.Vaccine;

namespace vaccine_chain_bk.DTO.Statistic
{
    public class StatisticLogsByVaccineId
    {
        public VaccineDto Vaccine { get; set; }
        public List<string> DeviceId { get; set; } = new List<string>();
        public double AverageValue { get; set; }
        public double HighestValue { get; set; }
        public DateTime TimeHighestValue { get; set; }
        public double LowestValue { get; set; }
        public DateTime TimeLowestValue { get; set; }
        public DateTime? DateRangeStart { get; set; }
        public DateTime? DateRangeEnd { get; set; }
        public int NumberRecords { get; set; }
    }

}
