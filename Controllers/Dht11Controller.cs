using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using vaccine_chain_bk.DTO.Dht11;
using vaccine_chain_bk.Hubs;
using vaccine_chain_bk.Services.Dht11;

namespace vaccine_chain_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dht11Controller : ControllerBase
    {
        private readonly IHubContext<TemperatureHub> _hubContext;
        private readonly IDht11Service _dht11Service;

        public Dht11Controller(IDht11Service dht11Service, IHubContext<TemperatureHub> hubContext)
{
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext)); // Kiểm tra null để tránh lỗi
            _dht11Service = dht11Service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Dht11Dto data)
        {
            if (data == null)
            {
                return BadRequest("Invalid data.");
            }

            // Log the received data to the console
            Console.WriteLine($"Received Device ID: {data.deviceId}");
            Console.WriteLine($"Received Vaccine ID: {data.vaccineId}");
            Console.WriteLine($"Received Temperature: {data.value}");



            // Process the data using the service (if necessary)
            _dht11Service.ProcessData(data);

            // Respond with a success message
            return Ok(new
            {
                message = "Data saved successfully.",
                temperature = data.value
            });
        }

        /*[HttpPost]
        public async Task<IActionResult> Post([FromBody] Dht11Dto data)
        {
            if (data == null)
            {
                return BadRequest("Invalid data.");
            }

            Console.WriteLine(data.vaccineId);
            Console.WriteLine(data.deviceId);
            Console.WriteLine(data.value);

            // Phát dữ liệu thời gian thực qua SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveTemperatureData", data);
            return Ok(new { Message = "Data broadcasted in real-time." });
        }*/

    }

}
