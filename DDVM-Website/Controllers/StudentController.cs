using DDVM_Website.DTOs;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services;
using DDVM_Website.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDVM_Website.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }


        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(StudentDto studentDto)
        {
            var createdStudent = await _studentService.CreateStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetStudentById), 
                new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDto studentDto)
        {
            if (id != studentDto.Id) return BadRequest();
            var updatedStudent = await _studentService.UpdateStudentAsync(id,studentDto);
            if (updatedStudent == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var deleted = await _studentService.DeleteStudentAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}