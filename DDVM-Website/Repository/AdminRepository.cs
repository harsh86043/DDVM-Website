using DDVM_Website.Data;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DDVM_Website.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Admin> UpdateAdminAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
                return false;

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}