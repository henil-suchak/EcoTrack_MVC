using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTrack.WebMvc.Models
{
    public class TravelActivity : Activity
    {
        [Required]
        [StringLength(100)]
        public string Mode { get; set; } = string.Empty; // e.g., "Car", "Bus"

        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 100000, ErrorMessage = "Distance must be a positive number.")]
        public decimal Distance { get; set; } // in km

        [StringLength(50)]
        public string? FuelType { get; set; } // e.g., "Petrol", "Electric"
        
        [StringLength(200)]
        public string? LocationStart { get; set; }

        [StringLength(200)]
        public string? LocationEnd { get; set; }
        
        // Foreign Key to the specific emission factor used
        public int EmissionFactorId { get; set; }
        
        // Navigation property
        public EmissionFactor? EmissionFactor { get; set; }
    }
}