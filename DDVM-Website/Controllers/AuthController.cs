using DDVM_Website.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using DDVM_Website.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DDVM_Website.DTOs;
using DDVM_Website.Services;

namespace DDVM_Website.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtHelper _jwtHelper;
        private readonly Services.EmailService _emailService;

        public AuthController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              JwtHelper jwtHelper,
                              Services.EmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtHelper = jwtHelper;
            _emailService = emailService;
        }

        /// <summary>
        /// Registers a new user (Student, Teacher, or Admin)
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest(new { message = "Email already exists" });

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Role = model.UserRole
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _roleManager.RoleExistsAsync(model.UserRole))
                await _roleManager.CreateAsync(new IdentityRole(model.UserRole));

            await _userManager.AddToRoleAsync(user, model.UserRole);

            // Send Email Confirmation
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"https://yourdomain.com/verify-email?email={model.Email}&token={token}";
            await _emailService.SendEmailAsync(user.Email, "Email Verification", $"Click here to verify: {confirmationLink}");

            return Ok(new { message = "User registered successfully. Please verify your email." });
        }

        /// <summary>
        /// Verifies user email
        /// </summary>
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest(new { message = "Invalid email" });

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return BadRequest(new { message = "Email verification failed" });

            return Ok(new { message = "Email verified successfully" });
        }

        /// <summary>
        /// Logs in a user and returns JWT token
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new { message = "Invalid credentials" });

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest(new { message = "Please verify your email before logging in." });

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtHelper.GenerateJwtToken(user, roles);

            return Ok(new
            {
                token,
                user = new
                {
                    id = user.Id,
                    email = user.Email,
                    role = roles[0]
                }
            });
        }

        /// <summary>
        /// Assigns roles to users (Admin Only)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest(new { message = "User not found" });

            if (!await _roleManager.RoleExistsAsync(model.Role))
                return BadRequest(new { message = "Invalid role" });

            await _userManager.AddToRoleAsync(user, model.Role);
            return Ok(new { message = $"User assigned to role {model.Role}" });
        }
    }
}