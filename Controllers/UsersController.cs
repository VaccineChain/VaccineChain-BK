using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using vaccine_chain_bk.DTO.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vaccine_chain_bk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Đây là một ví dụ đơn giản, bạn nên sử dụng một cơ sở dữ liệu hoặc dịch vụ xác thực thực tế
        private static readonly string AdminEmail = "admin";
        private static readonly string AdminPassword = "admin@123";
        // GET: api/<User>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<User>/5
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest.Email == AdminEmail && loginRequest.Password == AdminPassword)
            {
                return Ok(new { message = "Login successful" });
            }
            return Unauthorized(new { message = "Invalid credentials" });
        }

        // POST api/<User>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<User>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<User>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
