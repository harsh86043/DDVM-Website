using DDVM_Website.DTOs;

namespace DDVM_Website.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto model);
        Task<AuthResponseDto> LoginAsync(LoginDto model);
        Task<AuthResponseDto> AssignRoleAsync(AssignRoleDto model);
    }
}