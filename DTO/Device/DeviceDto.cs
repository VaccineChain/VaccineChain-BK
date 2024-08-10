using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.DTO.Device
{
    public class DeviceDto
    {
        public string DeviceId { get; set; }

        public string Location { get; set; }

        public EType SensorType { get; set; }
    }
}
