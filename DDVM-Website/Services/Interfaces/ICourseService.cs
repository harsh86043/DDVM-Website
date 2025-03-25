using DDVM_Website.DTOs;

namespace DDVM_Website.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int courseId);
        Task<CourseDto> CreateCourseAsync(CourseDto courseDto);
        Task<CourseDto> UpdateCourseAsync(CourseDto courseDto);
        Task<bool> DeleteCourseAsync(int courseId);
    }
}