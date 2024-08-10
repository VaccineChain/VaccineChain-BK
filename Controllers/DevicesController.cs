using Microsoft.AspNetCore.Mvc;
using vaccine_chain_bk.DTO;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.Services.Devices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vaccine_chain_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        // GET: api/<DevicesController>
        [HttpGet]
        public IActionResult Get()
        {
            List<DeviceDto> vaccines = _deviceService.GetAll();
            return Ok(vaccines);
        }

        // GET api/<DevicesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            DeviceDto deviceDto = _deviceService.GetById(id);
            return Ok(deviceDto);
        }

        // POST api/<DevicesController>
        [HttpPost]
        public IActionResult Create([FromBody] CreateDeviceDto createDeviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                DeviceDto deviceDto = _deviceService.CreateDevice(createDeviceDto);
                return Ok(deviceDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<DevicesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UpdateDeviceDto updateDeviceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                DeviceDto deviceDto = _deviceService.UpdateDevice(id, updateDeviceDto);
                return Ok(deviceDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // DELETE api/<DevicesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            ResponseDto response = new();
            try
            {
                response.Message = _deviceService.DeleteDevice(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
