using vaccine_chain_bk.Models;

namespace vaccine_chain_bk.Repositories.Logs
{
    public interface ILogRepository
    {
        List<Log> GetConnections();
        List<Log> GetLogByVaccineId(string vaccineId);
        List<Log> GetLogByDeviceId(string deviceId);
        List<Log> GetExistConnection(string vaccineId, string deviceId);
        List<Log> FindLog(string deviceId, string vaccineId);
        void SaveLog(Log log);
        void DeleteLog(Log log);
        void UpdateLog(Log log);
    }
}
