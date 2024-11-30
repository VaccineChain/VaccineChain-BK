using AutoMapper;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Devices;
using vaccine_chain_bk.Repositories.Logs;
using vaccine_chain_bk.Repositories.Vaccines;
using vaccine_chain_bk.Services.Devices;
using vaccine_chain_bk.Services.Vaccines;

namespace vaccine_chain_bk.Services.Logs
{
    public class LogService : ILogService
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        private readonly IDeviceService _deviceService;
        private readonly IVaccineService _vaccineService;

        public LogService(IMapper mapper, ILogRepository logRepository, IDeviceService deviceService, IVaccineService vaccineService)
        {
            _mapper = mapper;
            _logRepository = logRepository;
            _deviceService = deviceService;
            _vaccineService = vaccineService;
        }

        public LogDto CreateLog(CreateLogDto createLogDto)
        {
            // Check exist
            _ = _deviceService.GetById(createLogDto.DeviceId);
            _ = _vaccineService.GetById(createLogDto.VaccineId);

            Log log = _mapper.Map<Log>(createLogDto);
            _logRepository.SaveLog(log);

            return _mapper.Map<LogDto>(log);
        }

        public List<LogDto> GetAllByVaccineId(string vaccineId)
        {
            List<Log> logs = _logRepository.GetLogByVaccineId(vaccineId);
            return _mapper.Map<List<LogDto>>(logs);
        }

        public List<LogDto> GetAllByDeviceId(string deviceId)
        {
            List<Log> logs = _logRepository.GetLogByVaccineId(deviceId);
            return _mapper.Map<List<LogDto>>(logs);
        }

        public void SetLogs(CreateLogDto createLogDto)
        {
            Log log = _mapper.Map<Log>(createLogDto);

            _logRepository.SaveLog(log);
            _mapper.Map<LogDto>(log);
        }

        public List<LogDto> GetAll()
        {
            List<Log> logs = _logRepository.GetConnections();
            return _mapper.Map<List<LogDto>>(logs);
        }


        public string DeleteLog(string deviceId, string vaccineId)
        {
            List<Log> logs = _logRepository.FindLog(deviceId, vaccineId);

            if (logs.Count == 0)
            {
                throw new NotFoundException("Not found connections");
            }

            foreach (var log in logs)
            {
                _logRepository.DeleteLog(log);
            }

            return "Remove Log successfully!";
        }

        public List<LogDto> FindLogs(CreateLogDto createLogDto)
        {
            List<Log> logs = _logRepository.FindLog(createLogDto.DeviceId, createLogDto.VaccineId);
            return _mapper.Map<List<LogDto>>(logs);
        }

        public List<LogDto> GetExistConnection(string vaccineId, string deviceId)
        {
            List<Log> logs = _logRepository.GetExistConnection(vaccineId, deviceId);
            return _mapper.Map<List<LogDto>>(logs);
        }

        public string UpdateStatus(string deviceId, string vaccineId)
        {
            List<Log> logs = _logRepository.FindLog(deviceId, vaccineId);

            if (logs.Count == 0)
            {
                throw new NotFoundException("Not found connections");
            }

            foreach (var log in logs)
            {
                log.Status = Constraints.EStatus.Completed;
                _logRepository.UpdateLog(log);
            }

            return "Update Log Status successfully!";
        }
    }
}
