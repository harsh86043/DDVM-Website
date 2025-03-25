using DDVM_Website.DTOs;
using DDVM_Website.Models;
using DDVM_Website.Repository.IRepository;
using DDVM_Website.Services.Interfaces;

namespace DDVM_Website.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return students.Select(s => new StudentDto
            {
                Id = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.AddressLine1,
                LastQualifiedExam = s.LastQualifiedExam,
                Country = s.Country,
                Course = s.Course,
                Remarks = s.Remarks
            }).ToList();
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
                return null;

            return new StudentDto
            {
                Id = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.AddressLine1,
                LastQualifiedExam = student.LastQualifiedExam,
                Country = student.Country,
                Course = student.Course,
                Remarks = student.Remarks
            };
        }

        public async Task<StudentDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Email = studentDto.Email,
                Phone = studentDto.Phone,
                AddressLine1 = studentDto.Address,
                LastQualifiedExam = studentDto.LastQualifiedExam,
                Country = studentDto.Country,
                Course = studentDto.Course,
                Remarks = studentDto.Remarks
            };

            var createdStudent = await _studentRepository.AddStudentAsync(student);

            return new StudentDto
            {
                Id = createdStudent.StudentId,
                FirstName = createdStudent.FirstName,
                Email = createdStudent.Email,
                Phone = createdStudent.Phone,
                Address = createdStudent.AddressLine1,
                LastQualifiedExam = createdStudent.LastQualifiedExam,
                Country = createdStudent.Country,
                Course = createdStudent.Course,
                Remarks = createdStudent.Remarks
            };
        }

        public async Task<StudentDto> UpdateStudentAsync(int id, StudentDto studentDto)
        {
            var existingStudent = await _studentRepository.GetStudentByIdAsync(id);
            if (existingStudent == null)
                return null;

            existingStudent.FirstName = studentDto.FirstName;
            existingStudent.LastName = studentDto.LastName;
            existingStudent.Email = studentDto.Email;
            existingStudent.Phone = studentDto.Phone;
            existingStudent.AddressLine1 = studentDto.Address;
            existingStudent.LastQualifiedExam = studentDto.LastQualifiedExam;
            existingStudent.Country = studentDto.Country;
            existingStudent.Course = studentDto.Course;
            existingStudent.Remarks = studentDto.Remarks;

            var updatedStudent = await _studentRepository.UpdateStudentAsync(existingStudent);

            return new StudentDto
            {
                Id = updatedStudent.StudentId,
                FirstName = updatedStudent.FirstName,
                LastName = updatedStudent.LastName,
                Email = updatedStudent.Email,
                Phone = updatedStudent.Phone,
                Address = updatedStudent.AddressLine1,
                LastQualifiedExam = updatedStudent.LastQualifiedExam,
                Country = updatedStudent.Country,
                Course = updatedStudent.Course,
                Department = updatedStudent.Department.Name,
                Remarks = updatedStudent.Remarks
            };
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteStudentAsync(id);
        }
    }
}