using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using vaccine_chain_bk.DTO.HyperledgerResponse;
using vaccine_chain_bk.DTO.Request;
using vaccine_chain_bk.Services;

[Route("api/[controller]")]
[ApiController]
public class SensorController : ControllerBase
{
    private readonly HttpClientService _httpClientService;

    public SensorController(HttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequest request)
    {
        try
        {
            var result = await _httpClientService.RegisterUserAsync(request);
            return Ok(new { Message = result.Message, Token = result.Token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Registration failed.", Error = ex.Message });
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddVaccineData([FromBody] SensorReading request)
    {
        var token = HttpContext.Items.ContainsKey("Token")
                        ? HttpContext.Items["Token"]
                        : throw new AuthenticationException("Bearer token is missing.");  // Provide a default message

        var result = await _httpClientService.AddVaccineDataAsync(request, token.ToString());

        return Ok(new { Message = "Vaccine data added successfully.", Result = result });
    }

    [HttpGet("get/{vaccineId}")]
    public async Task<IActionResult> GetVaccineById(string vaccineId)
    {
        var token = HttpContext.Items.ContainsKey("Token")
                        ? HttpContext.Items["Token"]
                        : throw new AuthenticationException("Bearer token is missing.");  // Provide a default message

        try
        {
            var result = await _httpClientService.GetVaccineByIdAsync(vaccineId, token.ToString());
            return Ok(new { Message = "Vaccine data retrieved successfully.", Data = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
        }
    }

}
