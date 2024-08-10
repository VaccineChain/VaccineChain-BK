

using vaccine_chain_bk.DTO.Device;

namespace vaccine_chain_bk.Services.Devices
{
    public interface IDeviceService
    {
        List<DeviceDto> GetAll();
        DeviceDto GetById(string id);
        DeviceDto CreateDevice(CreateDeviceDto createDeviceDto);
        DeviceDto UpdateDevice(string id, UpdateDeviceDto updateDeviceDto);
        string DeleteDevice(string id);
    }
}
