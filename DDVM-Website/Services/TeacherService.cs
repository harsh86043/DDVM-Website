using DDVM_Website.DTOs;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;

namespace DDVM_Website.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepository.GetAllTeachersAsync();
            return teachers.Select(t => new TeacherDto
            {
                Id = t.TeacherId,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                Phone = t.Phone,
                Department = t.Department
            });
        }

        public async Task<TeacherDto> GetTeacherByIdAsync(int id)
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(id);
            if (teacher == null) return null;

            return new TeacherDto
            {
                Id = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                Department = teacher.Department
            };
        }

        public async Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto)
        {
            var teacher = new Teacher
            {
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                Email = teacherDto.Email,
                Phone = teacherDto.Phone,
                Department = teacherDto.Department
            };

            teacher = await _teacherRepository.AddTeacherAsync(teacher);

            return new TeacherDto
            {
                Id = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                Department = teacher.Department
            };
        }

        public async Task<TeacherDto> UpdateTeacherAsync(TeacherDto teacherDto)
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(teacherDto.Id);
            if (teacher == null) return null;

            teacher.FirstName = teacherDto.FirstName;
            teacher.LastName = teacherDto.LastName;
            teacher.Email = teacherDto.Email;
            teacher.Phone = teacherDto.Phone;
            teacher.Department = teacherDto.Department;

            teacher = await _teacherRepository.UpdateTeacherAsync(teacher);

            return new TeacherDto
            {
                Id = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                Department = teacher.Department
            };
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            return await _teacherRepository.DeleteTeacherAsync(id);
        }
    }
}
