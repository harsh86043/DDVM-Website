using DDVM_Website.DTOs;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;

namespace DDVM_Website.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departmentList = await _departmentRepository.GetAllDepartmentsAsync();
            return departmentList.Select(department => new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description
            }).ToList();
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int departmentId)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
            if (department == null) return null;

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description
            };
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                Name = departmentDto.Name,
                Description = departmentDto.Description
            };

            department = await _departmentRepository.CreateDepartmentAsync(department);

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description
            };
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(departmentDto.DepartmentId);
            if (department == null) return null;

            department.Name = departmentDto.Name;
            department.Description = departmentDto.Description;

            department = await _departmentRepository.UpdateDepartmentAsync(department);

            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Description = department.Description
            };
        }

        public async Task<bool> DeleteDepartmentAsync(int departmentId)
        {
            return await _departmentRepository.DeleteDepartmentAsync(departmentId);
        }
    }
}