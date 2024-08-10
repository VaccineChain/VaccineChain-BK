using Microsoft.AspNetCore.Mvc;
using vaccine_chain_bk.DTO.Device;
using vaccine_chain_bk.DTO;
using vaccine_chain_bk.DTO.Dose;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Does;
using vaccine_chain_bk.Services.Doses;

namespace vaccine_chain_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosesController : ControllerBase
    {
        private readonly IDoseService _doseService;
        public DosesController(IDoseService doseService)
        {
            _doseService = doseService;
        }

        // GET: api/<DevicesController>
        [HttpGet]
        public IActionResult Get()
        {
            List<DoseDto> vaccines = _doseService.GetAll();
            return Ok(vaccines);
        }

        // GET api/<DevicesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DoseDto doseDto = _doseService.GetById(id);
            return Ok(doseDto);
        }

        // POST api/<DevicesController>
        [HttpPost]
        public IActionResult Create([FromBody] CreateDoseDto createDoseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                DoseDto doseDto = _doseService.CreateDose(createDoseDto);
                return Ok(doseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<DevicesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateDoseDto updateDoseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                DoseDto doseDto = _doseService.UpdateDose(id, updateDoseDto);
                return Ok(doseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // DELETE api/<DevicesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ResponseDto response = new();
            try
            {
                response.Message = _doseService.DeleteDose(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
