using vaccine_chain_bk.Constraints;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Vaccine;

namespace vaccine_chain_bk.DTO.Log
{
    public class LogDto
    {
        public double Value { get; set; }

        public string? Unit { get; set; }

        public DateTime? Timestamp { get; set; }

        public EStatus? Status { get; set; }

        public VaccineDto Vaccine { get; set; }

        public DeviceDto Device { get; set; }
    }
}
