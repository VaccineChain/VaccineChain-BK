using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.DTO.Log
{
    public class CreateLogDto
    {
        public double? Value { get; set; }

        public string? Unit { get; set; }

        public DateTime? Timestamp { get; set; }

        public EStatus? Status { get; set; } = EStatus.Collecting;

        public string VaccineId { get; set; }

        public string DeviceId { get; set; }
    }
}
