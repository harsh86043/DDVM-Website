using DDVM_Website.DTOs;

namespace DDVM_Website.Repository.IRepository
{
    public interface IApplicationUserRepository
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task<ApplicationUserDto> GetUserByIdAsync(string userId);
    }
}