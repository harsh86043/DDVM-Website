using DDVM_Website.DTOs;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDVM_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAllTeachers()
        {
            return Ok(await _teacherService.GetAllTeachersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null) return NotFound();
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult<TeacherDto>> CreateTeacher(TeacherDto teacherDto)
        {
            var createdTeacher = await _teacherService.CreateTeacherAsync(teacherDto);
            return CreatedAtAction(nameof(GetTeacherById), new { id = createdTeacher.Id }, createdTeacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, TeacherDto teacherDto)
        {
            if (id != teacherDto.Id) return BadRequest();
            var updatedTeacher = await _teacherService.UpdateTeacherAsync(teacherDto);
            if (updatedTeacher == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var deleted = await _teacherService.DeleteTeacherAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
