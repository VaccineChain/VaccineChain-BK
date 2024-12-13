using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using vaccine_chain_bk.DTO.HyperledgerResponse;
using vaccine_chain_bk.DTO.Request;
using vaccine_chain_bk.DTO.Response;
using vaccine_chain_bk.DTO.Sensor;

namespace vaccine_chain_bk.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private string? _authToken;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(5); // Tăng timeout lên 5 phút
        }

        // Đăng ký người dùng và lấy token
        public async Task<string> RegisterUserAsync(string username, string orgName)
        {
            try
            {
                var url = Environment.GetEnvironmentVariable("NODE_VACCINE_URL") ?? throw new Exception("NODE_VACCINE_URL is not set.");

                var userRegistrationRequest = new
                {
                    username,
                    orgName
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(userRegistrationRequest),
                    Encoding.UTF8,
                    "application/json"
                );

                Console.WriteLine("Registering user...");

                using var response = await _httpClient.PostAsync($"{url}/users", jsonContent);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response from /users: {responseBody}");

                var responseObject = JsonSerializer.Deserialize<RegisterUserResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (responseObject == null || string.IsNullOrEmpty(responseObject.Token))
                {
                    throw new Exception("Failed to register user or retrieve token.");
                }

                _authToken = responseObject.Token; // Lưu token để tái sử dụng
                return _authToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering user: {ex.Message}");
                throw;
            }
        }

        // Thêm dữ liệu vaccine vào blockchain
        public async Task<string> AddVaccineDataAsync(SensorReading request)
        {
            try
            {
                if (string.IsNullOrEmpty(_authToken))
                {
                    Console.WriteLine("Auth token not found, registering user...");
                    _authToken = await RegisterUserAsync("authenToAdd", "Org1");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);

                var url = Environment.GetEnvironmentVariable("NODE_VACCINE_URL") ?? throw new Exception("NODE_VACCINE_URL is not set.");

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json"
                );

                Console.WriteLine("Sending vaccine data...");

                using var response = await _httpClient.PostAsync($"{url}/channels/mychannel/chaincodes/fabvaccine", jsonContent);

                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response from addVaccineData: {responseBody}");

                return responseBody;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding vaccine data: {ex.Message}");
                throw;
            }
        }

        // Lấy dữ liệu vaccine từ blockchain
        public async Task<List<DataResonse>> GetVaccineByIdAsync(string vaccineId, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var url = Environment.GetEnvironmentVariable("NODE_VACCINE_URL") ?? throw new Exception("NODE_VACCINE_URL is not set.");

                var queryParams = $"?args=[\"{vaccineId}\"]&peer=peer0.org1.example.com&fcn=queryVaccineDataByVaccineID";

                Console.WriteLine("Fetching vaccine data...");

                using var response = await _httpClient.GetAsync($"{url}/channels/mychannel/chaincodes/fabvaccine{queryParams}");

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Body: {responseBody}");

                var sensors = JsonSerializer.Deserialize<List<DataResonse>>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (sensors == null || sensors.Count == 0)
                {
                    throw new Exception("No data found in the API response.");
                }

                return sensors;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching vaccine data: {ex.Message}");
                throw;
            }
        }
    }
}
