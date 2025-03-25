using DDVM_Website.DTOs;

namespace DDVM_Website.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(int departmentId);
        Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto);
        Task<DepartmentDto> UpdateDepartmentAsync(DepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(int departmentId);
    }
}