using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vaccine_chain_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureLogsController : ControllerBase
    {
        // GET: api/<TemperatureLogsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TemperatureLogsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TemperatureLogsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TemperatureLogsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TemperatureLogsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
