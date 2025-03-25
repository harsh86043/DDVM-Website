using DDVM_Website.Data;
using DDVM_Website.DTOs;
using DDVM_Website.Models;
using DDVM_Website.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DDVM_Website.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(UserManager<ApplicationUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IConfiguration configuration,
                           ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        /// <summary>
        /// Registers a new user with a specific role
        /// </summary>
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "User already exists!" };
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "User creation failed!" };
            }

            // Assign Role
            if (!await _roleManager.RoleExistsAsync(model.UserRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.UserRole));
            }

            await _userManager.AddToRoleAsync(user, model.UserRole);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "User registered successfully!"
            };
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token
        /// </summary>
        public async Task<AuthResponseDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid credentials!" };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                Message = "Login successful!"
            };
        }

        /// <summary>
        /// Assigns a role to an existing user
        /// </summary>
        public async Task<AuthResponseDto> AssignRoleAsync(AssignRoleDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "User not found!" };
            }

            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Role));
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            return new AuthResponseDto { IsSuccess = true, Message = "Role assigned successfully!" };
        }

        /// <summary>
        /// Generates a JWT token for authenticated users
        /// </summary>
        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add user roles as claims
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
