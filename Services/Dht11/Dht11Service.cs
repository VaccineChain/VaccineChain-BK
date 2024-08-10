using AutoMapper;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Dht11;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.Vaccine;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Repositories.Devices;
using vaccine_chain_bk.Services.Logs;
using vaccine_chain_bk.Services.Vaccines;

namespace vaccine_chain_bk.Services.Dht11
{
    public class Dht11Service : IDht11Service
    {
        private readonly ILogService _logService;
        private readonly IVaccineService _vaccineService;

        public Dht11Service(ILogService logService, IVaccineService vaccineService)
        {
            _logService = logService;
            _vaccineService = vaccineService;
        }

        public void ProcessData(Dht11Dto dht11)
        {
            if (dht11 != null)
            {

                CreateLogDto setLogDto = new()
                {
                    DeviceId = dht11.deviceId,
                    VaccineId = dht11.vaccineId,
                    Value = dht11.value,
                    Unit = "Celsius",
                    Timestamp = DateTime.UtcNow,
                    Status = 0
                };

                Console.WriteLine(setLogDto.Timestamp);

                _logService.SetLogs(setLogDto);
            }
            else
            {
                throw new Exception("DHT11 is empty!");
            }
        }
    }
}
