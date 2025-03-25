using DDVM_Website.DTOs;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;

namespace DDVM_Website.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courseList = await _courseRepository.GetAllCoursesAsync();
            return courseList.Select(course => new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.CourseName,
                Description = course.Description,
                Duration = course.DurationInYears,
                Department = course.Department
            }).ToList();
        }

        public async Task<CourseDto> GetCourseByIdAsync(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null) return null;

            return new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.CourseName,
                Description = course.Description,
                Duration = course.DurationInYears,
                Department = course.Department
            };
        }

        public async Task<CourseDto> CreateCourseAsync(CourseDto courseDto)
        {
            var course = new Course
            {
                CourseName = courseDto.Name,
                Description = courseDto.Description,
                DurationInYears = courseDto.Duration,
                Department = courseDto.Department
            };

            course = await _courseRepository.CreateCourseAsync(course);

            return new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.CourseName,
                Description = course.Description,
                Duration = course.DurationInYears,
                Department = course.Department
            };
        }

        public async Task<CourseDto> UpdateCourseAsync(CourseDto courseDto)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseDto.CourseId);
            if (course == null) return null;

            course.CourseName = courseDto.Name;
            course.Description = courseDto.Description;
            course.DurationInYears = courseDto.Duration;
            course.Department = courseDto.Department;

            course = await _courseRepository.UpdateCourseAsync(course);

            return new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.CourseName,
                Description = course.Description,
                Duration = course.DurationInYears,
                Department = course.Department
            };
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            return await _courseRepository.DeleteCourseAsync(courseId);
        }
    }
}
