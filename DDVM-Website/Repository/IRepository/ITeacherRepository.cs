using DDVM_Website.Models;

namespace DDVM_Website.Repository.IRepository
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task<Teacher> AddTeacherAsync(Teacher teacher);
        Task<Teacher> UpdateTeacherAsync(Teacher teacher);
        Task<bool> DeleteTeacherAsync(int id);
    }
}