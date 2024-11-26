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

        public async Task<string> RegisterUserAsync(string username, string orgName)
        {
            var url = Environment.GetEnvironmentVariable("NODE_VACCINE_URL");

            // Tạo nội dung JSON cho yêu cầu
            var userRegistrationRequest = new
            {
                username = username,
                orgName = orgName
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(userRegistrationRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{url}/users", jsonContent);

            // Đảm bảo phản hồi thành công
            response.EnsureSuccessStatusCode();

            // Deserialize phản hồi để lấy token
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

            return responseObject.Token;
        }


        public async Task<string> AddVaccineDataAsync(SensorReading request)
        {
            var url = Environment.GetEnvironmentVariable("NODE_VACCINE_URL");

            string token = await RegisterUserAsync("authenToAdd", "Org1");
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Authentication token is missing or invalid.");
            }

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
