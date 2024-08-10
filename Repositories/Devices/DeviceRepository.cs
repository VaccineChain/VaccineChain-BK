using Microsoft.EntityFrameworkCore;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Devices
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _context;

        public DeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteDevice(Device device)
        {
            try
            {
                _context.Devices.Remove(device);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error deleting Device");
            }
        }

        public List<Device> GetAll()
        {
            try
            {
                return _context.Devices.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Device GetDevice(string id)
        {
            try
            {
                return _context.Devices.Find(id);
            }
            catch (Exception)   
            {
                throw new Exception("Error getting Device");
            }
        }

        public void SaveDevice(Device device)
        {
            try
            {
                _context.Devices.Add(device);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateDevice(Device Device)
        {
            try
            {
                _context.Entry<Device>(Device).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error updating Device");

            }
        }
    }
}
