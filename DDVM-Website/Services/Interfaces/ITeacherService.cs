using DDVM_Website.DTOs;

namespace DDVM_Website.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto> GetTeacherByIdAsync(int id);
        Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto);
        Task<TeacherDto> UpdateTeacherAsync(TeacherDto teacherDto);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
