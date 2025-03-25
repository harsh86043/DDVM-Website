using DDVM_Website.Models;

namespace DDVM_Website.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string LastQualifiedExam { get; set; }
        public string Country { get; set; }
        public Course Course { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }
    }
}