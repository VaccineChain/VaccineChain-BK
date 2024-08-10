using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.DTO.Log
{
    public class CreateLogDto
    {
        public int? TemperatureId { get; set; }

        public double? Value { get; set; }

        public string? Unit { get; set; }

        public DateTime? Timestamp { get; set; }

        public EStatus? Status { get; set; }

        public string VaccineId { get; set; }

        public string DeviceId { get; set; }
    }
}
