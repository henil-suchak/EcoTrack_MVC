using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTrack.WebMvc.Models
{
    public class Suggestion
    {
        [Key]
        public Guid SuggestionId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SavingAmount { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;
        
        public DateTime DateTimeIssued { get; set; }
        
        public bool IsRead { get; set; }

        // Foreign Key to User
        public Guid UserId { get; set; }
        
        // Navigation property
        public User? User { get; set; }
    }
}