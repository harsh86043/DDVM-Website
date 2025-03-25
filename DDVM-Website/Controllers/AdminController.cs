using DDVM_Website.DTOs;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;
using Igor.Gateway.Dtos.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventDto = DDVM_Website.DTOs.EventDto;

namespace DDVM_Website.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: api/admins
        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminService.GetAllAdminsAsync();
            return Ok(admins);
        }

        // GET: api/admins/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            var admin = await _adminService.GetAdminByIdAsync(id);
            if (admin == null)
                return NotFound(new { message = "Admin not found" });

            return Ok(admin);
        }

        // POST: api/admins
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminDto adminDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newAdmin = await _adminService.CreateAdminAsync(adminDto);
            return CreatedAtAction(nameof(GetAdminById), new { id = newAdmin.Id }, newAdmin);
        }

        // PUT: api/admins/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] AdminDto adminDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedAdmin = await _adminService.UpdateAdminAsync(id, adminDto);
            if (updatedAdmin == null)
                return NotFound(new { message = "Admin not found" });

            return Ok(updatedAdmin);
        }

        // DELETE: api/admins/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var isDeleted = await _adminService.DeleteAdminAsync(id);
            if (!isDeleted)
                return NotFound(new { message = "Admin not found" });

            return Ok(new { message = "Admin deleted successfully" });
        }
    }
}
