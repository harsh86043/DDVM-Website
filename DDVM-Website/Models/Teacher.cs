using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDVM_Website.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; } // Primary Key

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } // First Name of Teacher

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } // Last Name of Teacher

        [Required]
        [Phone]
        public string Phone { get; set; } // Contact Number

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Email Address

        [Required]
        [MaxLength(255)]
        public string Address { get; set; } // Address of Teacher

        [Required]
        public string Qualification { get; set; } // Qualification (e.g., M.Sc., Ph.D., M.Tech)

        [Required]
        public string Designation { get; set; } // Role (e.g., Assistant Professor, HOD, Lecturer)

        [Required]
        public int DepartmentId { get; set; } // Foreign Key for Department

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; } // Navigation Property for Department

        public DateTime JoiningDate { get; set; } = DateTime.UtcNow; // Date when the teacher joined

        public bool IsActive { get; set; } = true; // Active status of the teacher
    }
}
