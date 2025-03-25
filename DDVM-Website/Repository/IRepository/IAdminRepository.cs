
using DDVM_Website.DTOs;
using DDVM_Website.Models;

namespace DDVM_Website.Repository.IRepository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAllAdminsAsync();
        Task<Admin> GetAdminByIdAsync(int id);
        Task<Admin> GetAdminByEmailAsync(string email);
        Task<Admin> AddAdminAsync(Admin admin);
        Task<Admin> UpdateAdminAsync(Admin admin);
        Task<bool> DeleteAdminAsync(int id);
    }
}