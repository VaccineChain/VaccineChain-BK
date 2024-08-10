using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.DTO.Device
{
    public class CreateDeviceDto
    {
        public string DeviceId { get; set; }
        public string Location { get; set; }
        public EType SensorType { get; set; }
    }
}
