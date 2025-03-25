using System.ComponentModel.DataAnnotations;

namespace DDVM_Website.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; } // Primary Key

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } // e.g., "Admissions Open for 2025", "New Course Introduced"

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } // Detailed news content

        [MaxLength(255)]
        public string ImageUrl { get; set; } // URL for an optional news image

        [Required]
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow; // When the news was published

        [Required]
        public bool IsActive { get; set; } = true; // Determines if the news is visible

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When the news was added
    }
}