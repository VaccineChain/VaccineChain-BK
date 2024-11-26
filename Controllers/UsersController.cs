using Microsoft.AspNetCore.Mvc;
using vaccine_chain_bk.DTO;
using vaccine_chain_bk.DTO.User;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Services.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vaccine_chain_bk.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseDto response = new();
            try
            {
                response.Message = _userService.Register(registerDto);
                return Ok(response);
            }
            catch (ConflictException e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            catch (InvalidException e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseDto response = new();
            try
            {
                AuthResponse authResponse = await _userService.Login(loginDto);
                return Ok(authResponse);
            }
            catch (AuthenticationException e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var email = HttpContext.Items.ContainsKey("Email")
                        ? HttpContext.Items["Email"]
                        : throw new AuthenticationException("Unauthorized");  // Provide a default message

            ResponseDto response = new();
            try
            {
                response.Message = _userService.ChangePassword(email.ToString(), changePasswordDto);
                return Ok(response);
            }
            catch (NotFoundException e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (InvalidException e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
