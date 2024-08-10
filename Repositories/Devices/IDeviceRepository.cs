using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Devices
{
    public interface IDeviceRepository
    {
        List<Device> GetAll();
        void SaveDevice(Device device);
        Device GetDevice(string id);
        void DeleteDevice(Device device);
        void UpdateDevice(Device device);
    }
}
