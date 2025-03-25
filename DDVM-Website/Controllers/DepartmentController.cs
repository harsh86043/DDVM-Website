using DDVM_Website.DTOs;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDVM_Website.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments()
        {
            return Ok(await _departmentService.GetAllDepartmentsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound();
            return Ok(department);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            var createdDepartment = await _departmentService.CreateDepartmentAsync(departmentDto);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.DepartmentId }, createdDepartment);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            if (id != departmentDto.DepartmentId) return BadRequest();
            var updatedDepartment = await _departmentService.UpdateDepartmentAsync(departmentDto);
            if (updatedDepartment == null) return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            var success = await _departmentService.DeleteDepartmentAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}