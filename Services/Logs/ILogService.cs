using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.Vaccine;

namespace vaccine_chain_bk.Services.Logs
{
    public interface ILogService
    {
        List<LogDto> GetAll();

        List<LogDto> GetAllByVaccineId(string vaccineId);

        List<LogDto> GetExistConnection(string vaccineId, string deviceId);

        LogDto CreateLog(CreateLogDto createVaccineDto);

        void SetLogs(CreateLogDto createLogDto);

        List<LogDto> FindLogs(CreateLogDto createLogDto);

        string UpdateStatus(string deviceId, string vaccineId);
        string DeleteLog(string deviceId, string vaccineId);
    }
}
