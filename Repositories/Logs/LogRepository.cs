using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Logs
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext _context;

        public LogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Log> GetLogByVaccineId(string vaccineId)
        {
            try
            {
                List<Log> logs = _context.Logs.Where(l => l.VaccineId == vaccineId).ToList();
                return logs;
            }
            catch (Exception)   
            {
                throw new Exception("Error getting Log");
            }
        }

        public List<Log> GetLogByDeviceId(string id)
        {
            try
            {
                List<Log> logs = _context.Logs.Where(l => l.DeviceId == id).ToList();
                return logs;
            }
            catch (Exception)
            {
                throw new Exception("Error getting Log");
            }
        }


        public List<Log> GetExistConnection(string vaccineId, string deviceId)
        {
            try
            {
                return _context.Logs.Where(d => d.VaccineId == vaccineId && d.DeviceId == deviceId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void SaveLog(Log Log)
        {
            try
            {
                _context.Logs.Add(Log);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Log> GetConnections()
        {
            try
            {
                List<Log> groupedLogs = _context.Logs
                    .Include(l => l.Device)
                    .Include(l => l.Vaccine)
                    .GroupBy(log => new { log.VaccineId, log.DeviceId }) // Nhóm theo vaccineId và deviceId
                    .Select(group => new Log
                    {
                        Vaccine = group.FirstOrDefault().Vaccine,
                        Device = group.FirstOrDefault().Device
                    })
                    .ToList();

                return groupedLogs;
            }
            catch (Exception)
            {
                throw new Exception("Error getting Log");
            }
        }

        public Log FindLog(string deviceId, string vaccineId)
        {
            try
            {
                Log logs = _context.Logs.Where(l => l.DeviceId == deviceId && l.VaccineId == vaccineId).FirstOrDefault();
                return logs;
            }
            catch (Exception)
            {
                throw new Exception("Error getting Log");
            }
        }

        List<Log> ILogRepository.FindLog(string deviceId, string vaccineId)
        {
            try
            {
                return _context.Logs.Where(l => l.DeviceId == deviceId && l.VaccineId == vaccineId).ToList();
            }
            catch
            {
                throw new Exception("Error find Logs");
            }
        }

        public void DeleteLog(Log log)
        {
            try
            {
                _context.Logs.Remove(FindLog(log.DeviceId, log.VaccineId));
                _context.SaveChanges();

            }
            catch
            {
                throw new Exception("Error delete Log");
            }
        }
    }
}
