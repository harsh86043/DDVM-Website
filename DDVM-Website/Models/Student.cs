using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DDVM_Website.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; } // Primary Key

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } // First Name of Student

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } // Last Name of Student

        [Required]
        [Phone]
        public string Phone { get; set; } // Contact Number

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Email Address

        [Required]
        [MaxLength(255)]
        public string AddressLine1 { get; set; } // First Line of Address

        [MaxLength(255)]
        public string AddressLine2 { get; set; } // Second Line of Address (Optional)

        [Required]
        [MaxLength(20)]
        public string ZipCode { get; set; } // Zip/Postal Code

        [Required]
        public string LastQualifiedExam { get; set; } // Dropdown: High School, Inter, Graduation, etc.

        [Required]
        public string Country { get; set; } // Country of Student

        [Required]
        public int CourseId { get; set; } // Foreign Key for Course

        [ForeignKey("CourseId")]
        public Course Course { get; set; } // Navigation Property for Course

        [Required]
        public int DepartmentId { get; set; } // Foreign Key for Department

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; } // Navigation Property for Department

        public string Remarks { get; set; } // Additional Comments

        [Required]
        public bool IsTermsAccepted { get; set; } // Terms and Conditions Agreement

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow; // Registration Timestamp
    }
}
