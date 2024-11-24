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

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserRegistrationResponse> RegisterUserAsync(UserRegistrationRequest request)
        {
            var url = Environment.GetEnvironmentVariable("NODE_VACCINE_URL");
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{url}/users", jsonContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize phản hồi từ API
            var registrationResponse = JsonSerializer.Deserialize<UserRegistrationResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (registrationResponse == null || !registrationResponse.Success)
            {
                throw new Exception($"User registration failed: {registrationResponse?.Message ?? "Unknown error"}");
            }

            // Lưu token để sử dụng cho các yêu cầu sau
            return registrationResponse;
        }

        public async Task<string> AddVaccineDataAsync(SensorReading request, string token)
        {
            var url = Environment.GetEnvironmentVariable("NODE_VACCINE_URL");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{url}/channels/mychannel/chaincodes/fabvaccine", jsonContent);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<DataResonse>> GetVaccineByIdAsync(string vaccineId, string token)
        {
            var url = Environment.GetEnvironmentVariable("NODE_VACCINE_URL");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var queryParams = $"?args=[\"{vaccineId}\"]&peer=peer0.org1.example.com&fcn=queryVaccineDataByVaccineID";
            var response = await _httpClient.GetAsync($"{url}/channels/mychannel/chaincodes/fabvaccine{queryParams}");

            // Đảm bảo phản hồi thành công
            response.EnsureSuccessStatusCode();

            // Lấy dữ liệu phản hồi JSON
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Body: {responseBody}");

            // Deserialize trực tiếp thành danh sách SensorDTO
            var sensors = JsonSerializer.Deserialize<List<DataResonse>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Bỏ qua phân biệt chữ hoa/thường
            });

            if (sensors == null || sensors.Count == 0)
            {
                throw new Exception("No data found in the API response.");
            }

            return sensors;
        }

    }
}
