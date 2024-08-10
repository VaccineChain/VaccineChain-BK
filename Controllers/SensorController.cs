using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using vaccine_chain_bk.DTO.HyperledgerResponse;

[Route("api/[controller]")]
[ApiController]
public class SensorController : ControllerBase
{
    private readonly HttpClient _httpClient;
    public string ngrokUrl = "https://ee2a-171-247-209-97.ngrok-free.app"; /* change url ngrok in chaincode here*/


    public SensorController()
    {
        _httpClient = new HttpClient();
    }

    [HttpGet("query")]
    public async Task<IActionResult> QueryChaincode([FromQuery] SensorReading queryData)
    {
        try
        {
            User registerUser = new(queryData.args.Last(), "Org1");
            var token = await EnrollUser(registerUser);

            if (string.IsNullOrEmpty(token))
            {
                return StatusCode(500, "Failed to enroll user and obtain token.");
            }

            // Set up the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = ngrokUrl + $"/channels/{queryData.channelName}/chaincodes/{queryData.chaincodeName}" +
                      $"?args=[\"{queryData.args.First()}\"]&peer={queryData.peers.First()}&fcn={queryData.fcn}";

            Console.WriteLine("Request URL: " + url);
            Console.WriteLine("Authorization Token: " + token);

            // Send the GET request
            var response = await _httpClient.GetAsync(url);

            Console.WriteLine("Response Status Code: " + response.StatusCode);
            Console.WriteLine("Response Reason Phrase: " + response.ReasonPhrase);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetResults>(responseData);
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode, "Failed to query chaincode.");
        }
        catch (HttpRequestException e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }


    [HttpPost("send")]
    public async Task<IActionResult> SendSensorReading([FromBody] SensorReading sensorReading)
    {
        // Serialize the sensorReading object to JSON
        var json = JsonConvert.SerializeObject(sensorReading);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        User registerUser = new User(sensorReading.args.Last(), "Org1");
        var token = await EnrollUser(registerUser);

        if (token == null)
        {
            return StatusCode(500, "Failed to enroll user.");
        }

        // Set up the Bearer token
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Send the POST request
        var response = await _httpClient.PostAsync($"{ngrokUrl}/channels/mychannel/chaincodes/fabcar", content);

        // Handle the response
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SmartContractResponse>(responseData);
            return Ok(result);
        }

        return StatusCode((int)response.StatusCode, "Failed to send sensor reading.");
    }

    private async Task<string> EnrollUser(User user)
    {
        // Serialize the user object to JSON
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Send the POST request
        var response = await _httpClient.PostAsync($"{ngrokUrl}/users", content);

        // Handle the response
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegisterUserResponse>(responseData);
            Console.WriteLine($"Token: {result.token}");
            return result.token;
        }

        return null;
    }
}
