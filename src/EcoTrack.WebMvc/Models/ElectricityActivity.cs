using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTrack.WebMvc.Models
{
    public class ElectricityActivity : Activity
    {
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 100000, ErrorMessage = "Consumption must be a positive number.")]
        public decimal Consumption { get; set; } // in kWh

        [Required]
        [StringLength(50)]
        public string SourceType { get; set; } = string.Empty; // "smart meter", "bill upload"
        
        // Foreign Key to the EmissionFactor entity
        public int EmissionFactorId { get; set; }
        
        // Navigation property
        public EmissionFactor? EmissionFactor { get; set; }
    }
}