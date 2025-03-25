using DDVM_Website.DTOs;
using DDVM_Website.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DDVM_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _userService;

        public ApplicationUserController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _userService.RegisterAsync(registerDto);
            if (result == null) return BadRequest("Registration failed!");
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _userService.LoginAsync(loginDto);
            if (token == null) return Unauthorized();
            return Ok(new { Token = token });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}