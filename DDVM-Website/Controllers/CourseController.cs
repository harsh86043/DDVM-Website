using DDVM_Website.DTOs;
using DDVM_Website.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DDVM_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
        {
            return Ok(await _courseService.GetAllCoursesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CourseDto courseDto)
        {
            var createdCourse = await _courseService.CreateCourseAsync(courseDto);
            return CreatedAtAction(nameof(GetCourseById), new { id = createdCourse.CourseId }, createdCourse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] CourseDto courseDto)
        {
            if (id != courseDto.CourseId) return BadRequest();
            var updatedCourse = await _courseService.UpdateCourseAsync(courseDto);
            if (updatedCourse == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var success = await _courseService.DeleteCourseAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
