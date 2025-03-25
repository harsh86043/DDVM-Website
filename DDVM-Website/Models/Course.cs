using System.ComponentModel.DataAnnotations;

namespace DDVM_Website.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; } // Primary Key

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; } // e.g., B.A., B.Com., D.Pharma, M.A., M.Com.

        [Required]
        [MaxLength(50)]
        public string CourseType { get; set; } // e.g., Undergraduate, Postgraduate, Diploma

        [Required]
        [MaxLength(50)]
        public string Department { get; set; } // e.g., Science, Commerce, Arts

        [MaxLength(500)]
        public string Description { get; set; } // Details about the course

        [Required]
        public int DurationInYears { get; set; } // e.g., 3 years, 2 years, 4 years

        [Required]
        public bool IsActive { get; set; } = true; // Determines if the course is currently available

        // Navigation Property: A course can have multiple students
        public List<ApplicationUser> Students { get; set; } = new List<ApplicationUser>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
