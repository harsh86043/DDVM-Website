using DDVM_Website.DTOs;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;

namespace DDVM_Website.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _userRepository;

        public ApplicationUserService(IApplicationUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            return await _userRepository.RegisterAsync(registerDto);
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            return await _userRepository.LoginAsync(loginDto);
        }

        public async Task<ApplicationUserDto> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
    }
}