using DDVM_Website.Data;
using DDVM_Website.DTOs;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;
using Igor.Gateway.Dtos.Events;

namespace DDVM_Website.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<AdminDto>> GetAllAdminsAsync()
        {
            var admins = await _adminRepository.GetAllAdminsAsync();
            return admins.Select(a => new AdminDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email
            }).ToList();
        }

        public async Task<AdminDto> GetAdminByIdAsync(int id)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(id);
            if (admin == null)
                return null;

            return new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email
            };
        }

        public async Task<AdminDto> CreateAdminAsync(AdminDto adminDto)
        {
            var admin = new Admin
            {
                FirstName = adminDto.FirstName,
                LastName = adminDto.LastName,
                Email = adminDto.Email
            };

            var createdAdmin = await _adminRepository.AddAdminAsync(admin);

            return new AdminDto
            {
                Id = createdAdmin.Id,
                FirstName = createdAdmin.FirstName,
                LastName = createdAdmin.LastName,
                Email = createdAdmin.Email
            };
        }

        public async Task<AdminDto> UpdateAdminAsync(int id, AdminDto adminDto)
        {
            var existingAdmin = await _adminRepository.GetAdminByIdAsync(id);
            if (existingAdmin == null)
                return null;

            existingAdmin.FirstName = adminDto.FirstName;
            existingAdmin.LastName = adminDto.LastName;
            existingAdmin.Email = adminDto.Email;

            var updatedAdmin = await _adminRepository.UpdateAdminAsync(existingAdmin);

            return new AdminDto
            {
                Id = updatedAdmin.Id,
                FirstName = updatedAdmin.FirstName,
                LastName = updatedAdmin.LastName,
                Email = updatedAdmin.Email
            };
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            return await _adminRepository.DeleteAdminAsync(id);
        }
    }
}