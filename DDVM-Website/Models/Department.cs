using System.ComponentModel.DataAnnotations;

namespace DDVM_Website.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }  // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation Properties (One-to-Many)
        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
