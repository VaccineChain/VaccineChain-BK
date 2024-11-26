using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vaccine_chain_bk.DTO;
using vaccine_chain_bk.DTO.Log;
using vaccine_chain_bk.DTO.Vaccine;
using vaccine_chain_bk.Exceptions;
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

        [HttpGet("GetById")]
        public IActionResult GetVaccineById(string vaccineId)
        {
            try
            {
                VaccineDto vaccineDto = _vaccineService.GetById(vaccineId);
                return Ok(vaccineDto);
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

        [HttpGet("GetByName")]
        public IActionResult GetVaccineByName(string vaccineName)
        {
            try
            {
                List<VaccineDto> vaccineDto = _vaccineService.GetByName(vaccineName);
                return Ok(vaccineDto);
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
