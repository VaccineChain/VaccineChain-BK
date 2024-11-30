using Microsoft.AspNetCore.Mvc;
using vaccine_chain_bk.DTO;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.LogValue;
using vaccine_chain_bk.DTO.Vaccine;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Services.Devices;
using vaccine_chain_bk.Services.Logs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vaccine_chain_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        // GET: api/<TemperatureLogsController>
        [HttpGet]
        public IActionResult Get()
        {
            List<LogDto> allLogs = _logService.GetAll();
            return Ok(allLogs);
        }

        // GET: api/<TemperatureLogsController>
        [HttpGet("{vaccineId}")]
        public IActionResult GetAllByVaccineId(string vaccineId)
        {
            List<LogDto> allLogs = _logService.GetAllByVaccineId(vaccineId);
            return Ok(allLogs);
        }


        [HttpGet("GetExistConnection")]
        public IActionResult GetExistConnection([FromQuery] string deviceId, [FromQuery] string vaccineId)
        {
            try
            {
                List<LogDto> logDto = _logService.GetExistConnection(vaccineId, deviceId);
                return Ok(logDto);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<TemperatureLogsController>
        [HttpPost]
        public IActionResult Create([FromBody] CreateLogDto createLogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                LogDto logDto = _logService.CreateLog(createLogDto);
                return Ok(logDto);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateStatus")]
        public IActionResult UpdateStatus([FromBody] UpdateLogStatusDto updateLogStatusDto)
        {
            ResponseDto response = new();
            try
            {
                response.Message = _logService.UpdateStatus(updateLogStatusDto.DeviceId, updateLogStatusDto.VaccineId);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<VaccinesController>/5
        [HttpDelete]
        public IActionResult Delete([FromQuery] string deviceId, [FromQuery] string vaccineId)
        {
            ResponseDto response = new();
            try
            {
                response.Message = _logService.DeleteLog(deviceId, vaccineId);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
