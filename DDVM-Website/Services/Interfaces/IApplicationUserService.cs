using DDVM_Website.DTOs;

namespace DDVM_Website.Services.Interfaces
{
    public interface IApplicationUserService
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<ApplicationUserDto> GetUserByIdAsync(string userId);
    }
}
