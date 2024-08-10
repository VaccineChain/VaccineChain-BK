using Azure;
using Microsoft.AspNetCore.Mvc;
using vaccine_chain_bk.DTO;
using vaccine_chain_bk.DTO.Vaccine;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Vaccines;
using vaccine_chain_bk.Services.Vaccines;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vaccine_chain_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinesController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;

        public VaccinesController(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        // GET: api/<VaccinesController>
        [HttpGet]
        public IActionResult Get()
        {
            List<VaccineDto> vaccines = _vaccineService.GetAll();
            return Ok(vaccines);
        }

        // GET api/<VaccinesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            VaccineDto vaccineDto = _vaccineService.GetById(id);
            return Ok(vaccineDto);
        }

        // POST api/<VaccinesController>
        [HttpPost]
        public IActionResult Create([FromBody] CreateVaccineDto createVaccineDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                VaccineDto vaccineDto = _vaccineService.CreateVaccine(createVaccineDto);
                return Ok(vaccineDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<VaccinesController>/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UpdateVaccineDto updateVaccineDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                VaccineDto vaccineDto = _vaccineService.UpdateVaccine(id, updateVaccineDto);
                return Ok(vaccineDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<VaccinesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            ResponseDto response = new();
            try
            {
                response.Message = _vaccineService.DeleteVaccine(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
