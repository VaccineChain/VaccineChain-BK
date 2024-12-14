namespace vaccine_chain_bk.DTO.Statistic
{
    public class VaccinesTemperatureRangeDto
    {
        public string VaccineName { get; set; }
        public double? HighestTemperature { get; set; }
        public double? LowestTemperature { get; set; }
    }
}
