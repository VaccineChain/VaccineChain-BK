using Microsoft.AspNetCore.Mvc;
using vaccine_chain_bk.DTO.Statistic;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Services.Statistics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vaccine_chain_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        // GET: api/<StatisticController>

        [HttpGet("Logs/{vaccineId}")]
        public IActionResult GetStatisticsByVaccineId(string vaccineId)
        {
            try
            {
                StatisticLogsByVaccineId statisticLogs = _statisticService.GetStatisticLog(vaccineId);
                return Ok(statisticLogs);
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

        [HttpGet("GetVaccineStatistics")]
        public IActionResult GetVaccineStatistics()
        {
            try
            {
                List<VaccineDeviceStatus> vaccineDeviceStatuses = _statisticService.GetVaccineStatistics();
                return Ok(vaccineDeviceStatuses);
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

        [HttpGet("Area-Chart/{vaccineId}")]
        public IActionResult GetStatisticsForAreaChart(string vaccineId)
        {
            try
            {
                List<StatisticAreaChart> statisticLogs = _statisticService.GetStatisticsForAreaChart(vaccineId);
                return Ok(statisticLogs);
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
