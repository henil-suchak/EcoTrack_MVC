using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTrack.WebMvc.Entities
{
    public class Badge
    {
        [Key] // Explicitly sets this as the primary key
        public Guid BadgeId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(500)]
        public string CriteriaMet { get; set; } = string.Empty;

        [StringLength(500)]
        public string IconUrl { get; set; } = string.Empty;
        
        public DateTime DateEarned { get; set; }

        // Foreign Key for the User relationship
        public Guid UserId { get; set; }

        // Navigation property to the User
        public User? User { get; set; }
    }
}