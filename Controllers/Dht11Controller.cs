using Microsoft.AspNetCore.Mvc;
using vaccine_chain_bk.DTO.Dht11;
using vaccine_chain_bk.Services.Dht11;

namespace vaccine_chain_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dht11Controller : ControllerBase
    {
        private readonly IDht11Service _dht11Service;

        public Dht11Controller(IDht11Service dht11Service)
        {
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
    }

}
