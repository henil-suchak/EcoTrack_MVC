using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTrack.WebMvc.Entities
{
    public class ApplianceActivity : Activity
    {
        [Required]
        [StringLength(100)]
        public string ApplianceType { get; set; } = string.Empty; // e.g., "Fridge", "AC"

        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 10000, ErrorMessage = "Usage time must be a positive number.")]
        public decimal UsageTime { get; set; } // in hours

        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, 100000, ErrorMessage = "Power rating must be a positive number.")]
        public decimal PowerRating { get; set; } // in watts
        
        // Foreign Key to the EmissionFactor entity
        public int EmissionFactorId { get; set; }
        
        // Navigation property
        public EmissionFactor? EmissionFactor { get; set; }
    }
}