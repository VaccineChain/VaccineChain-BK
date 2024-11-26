using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        try
        {
            // Gọi phương thức đăng ký user từ HttpClientService
            string token = await _httpClientService.RegisterUserAsync(request.Username, request.OrgName);

            return Ok(new
            {
                Message = $"{request.Username} registered successfully.",
                Token = token
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during user registration.", error = ex.Message });
        }
    }


    [HttpPost("add")]
    public async Task<IActionResult> AddVaccineData([FromBody] SensorReading request)
    {
        string token;

        // Kiểm tra Authorization Header
        var authorizationHeader = Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            // Nếu Authorization Header không tồn tại hoặc không hợp lệ, tự động đăng ký user
            try
            {
                var defaultUsername = "defaultUser"; // Username mặc định
                var defaultOrgName = "Org1";        // OrgName mặc định

                token = await _httpClientService.RegisterUserAsync(defaultUsername, defaultOrgName);

                Console.WriteLine($"User '{defaultUsername}' registered successfully. Token: {token}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to register user.", error = ex.Message });
            }
        }
        else
        {
            // Trích xuất token từ Authorization Header
            token = authorizationHeader.Substring("Bearer ".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Bearer token is missing." });
            }
        }

        // Gọi phương thức để thêm dữ liệu vaccine
        try
        {
            var result = await _httpClientService.AddVaccineDataAsync(request);

            return Ok(new { Message = "Vaccine data added successfully.", Result = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding vaccine data.", error = ex.Message });
        }
    }


    [HttpGet("get/{vaccineId}")]
    public async Task<IActionResult> GetVaccineById(string vaccineId)
    {
        var authorizationHeader = Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return Unauthorized(new { message = "Authorization header is missing." });
        }

        // Extract Bearer token
        var token = authorizationHeader.StartsWith("Bearer ")
                    ? authorizationHeader.Substring("Bearer ".Length).Trim()
                    : null;

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new { message = "Bearer token is missing." });
        }

        try
        {
            var result = await _httpClientService.GetVaccineByIdAsync(vaccineId, token);
            return Ok(new { Message = "Vaccine data retrieved successfully.", Data = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
        }
    }

}
