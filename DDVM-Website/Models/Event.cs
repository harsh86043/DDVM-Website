using System.ComponentModel.DataAnnotations;

namespace DDVM_Website.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; } // Primary Key

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } // e.g., "Annual Cultural Fest", "Sports Meet 2025"

        [MaxLength(500)]
        public string Description { get; set; } // Brief details about the event

        [Required]
        public DateTime EventDate { get; set; } // When the event is happening

        public string Location { get; set; } // Where the event is held (Optional)

        [MaxLength(255)]
        public string ImageUrl { get; set; } // URL for event poster/image

        [Required]
        public bool IsActive { get; set; } = true; // Determines if the event is visible

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When the event was added
    }
}