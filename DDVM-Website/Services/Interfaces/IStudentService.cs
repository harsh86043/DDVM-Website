using DDVM_Website.DTOs;
using DDVM_Website.Models;

namespace DDVM_Website.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<StudentDto> CreateStudentAsync(StudentDto studentDto);
        Task<StudentDto> UpdateStudentAsync(int id, StudentDto studentDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}