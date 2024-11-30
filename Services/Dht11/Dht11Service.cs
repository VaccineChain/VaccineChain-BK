using AutoMapper;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Dht11;
using vaccine_chain_bk.DTO.HyperledgerResponse;
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
        private readonly HttpClientService _httpClientService;

        public Dht11Service(ILogService logService, HttpClientService httpClientService)
        {
            _logService = logService;
            _httpClientService = httpClientService;
        }

        public async void ProcessData(Dht11Dto dht11)
        {
            SensorReading smartContractRequest = new()
            {
                fcn = "addVaccineData",
                peers = new List<string> { "peer0.org1.example.com", "peer0.org2.example.com" },
                chaincodeName = "fabvaccine",
                channelName = "mychannel",
                args = new List<string>
                {
                    dht11.vaccineId, // Vaccine ID
                    dht11.deviceId,  // Device ID
                    dht11.value.ToString() // Value as string
                }
            };

            try
            {
                // Thêm vào Smart Contract trước
                var result = await _httpClientService.AddVaccineDataAsync(smartContractRequest);
                
                Console.WriteLine("Smart Contract Response: " + result);

                // Nếu thành công, tiếp tục thêm vào database
                CreateLogDto setLogDto = new()
                {
                    DeviceId = dht11.deviceId,
                    VaccineId = dht11.vaccineId,
                    Value = dht11.value,
                    Unit = "Celsius",
                    Timestamp = DateTime.UtcNow,
                    Status = 0 // Status ban đầu là 0
                };

                Console.WriteLine($"Log Timestamp: {setLogDto.Timestamp}");

                // Lưu vào cơ sở dữ liệu
                _logService.SetLogs(setLogDto);

                Console.WriteLine("Data added to database successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during ProcessDataAsync: " + ex.Message);
                throw; // Ném lỗi để cho biết thất bại
            }
        }

    }
}
