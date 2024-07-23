using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using vaccine_chain_bk.DTO;
using vaccine_chain_bk.Models;

[Route("api/[controller]")]
[ApiController]
public class SensorController : ControllerBase
{
    private readonly HttpClient _httpClient;
    public string ngrokUrl = "https://7a58-125-235-237-232.ngrok-free.app"; /* change url ngrok in chaincode here*/


    public SensorController()
    {
        _httpClient = new HttpClient();
    }

    [HttpGet("query")]
    public async Task<IActionResult> QueryChaincode(
        [FromQuery] string peer,
        [FromQuery] string channelName,
        [FromQuery] string chaincodeName,
        [FromQuery] string args,
        [FromQuery] string fcn)
    {

        // Set up the Bearer token
        string token = await GetUserToken(ngrokUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        try
        {
            // Send the GET request
            var response = await _httpClient.GetAsync(ngrokUrl + "/channels/mychannel/chaincodes/fabcar" + $"?args=[\"{args}\"]&peer={peer}&fcn={fcn}");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SmartContractResponse>(responseData);
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode, "Failed to query chaincode.");
        }
        catch (HttpRequestException e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

    private async Task<string> GetUserToken(string ngrokUrl)
    {
        // Create the registerUser object
        var registerUser = new
        {
            username = "tran",
            orgName = "Org1"
        };

        // Serialize the registerUser object to JSON
        var json = JsonConvert.SerializeObject(registerUser);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Send the POST request
        var response = await _httpClient.PostAsync(ngrokUrl + "/user", content);

        // Handle the response
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegisterUserResponse>(responseData);
            return result.Token;
        }
        else
        {
            // Handle the case where the response was not successful
            throw new Exception("Failed to retrieve token from the server.");
        }
    }


    [HttpPost("send")]
    public async Task<IActionResult> SendSensorReading([FromBody] SensorReading sensorReading)
    {
        // Serialize the sensorReading object to JSON
        var json = JsonConvert.SerializeObject(sensorReading);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Set up the Bearer token
        string token = await GetUserToken(ngrokUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Send the POST request
        var response = await _httpClient.PostAsync(ngrokUrl + "channels/mychannel/chaincodes/fabcar", content);

        // Handle the response
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SmartContractResponse>(responseData);
            return Ok(result);
        }

        return StatusCode((int)response.StatusCode, "Failed to send sensor reading.");
    }
}
