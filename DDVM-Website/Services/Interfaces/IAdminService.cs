using DDVM_Website.DTOs;
using Igor.Gateway.Dtos.Events;
using EventDto = DDVM_Website.DTOs.EventDto;

namespace DDVM_Website.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminDto>> GetAllAdminsAsync();
        Task<AdminDto> GetAdminByIdAsync(int id);
        Task<AdminDto> CreateAdminAsync(AdminDto adminDto);
        Task<AdminDto> UpdateAdminAsync(int id, AdminDto adminDto);
        Task<bool> DeleteAdminAsync(int id);
    }
}