using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTrack.WebMvc.Models
{
    public class LeaderboardEntry
    {
        [Key]
        public int LeaderboardEntryId { get; set; }

        public int Rank { get; set; }

        [Required]
        [StringLength(50)]
        public string Period { get; set; } = string.Empty; // e.g., "Weekly", "Monthly"

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CarbonEmission { get; set; }

        public int? CommunityId { get; set; } // Optional for group leaderboards

        // Foreign Key to User
        public Guid UserId { get; set; } // CORRECTED to Guid
        
        // Navigation property
        public User? User { get; set; }
    }
}