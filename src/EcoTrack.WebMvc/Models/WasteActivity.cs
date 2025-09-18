using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTrack.WebMvc.Entities
{
    public class WasteActivity : Activity
    {
        [Required]
        [StringLength(100)]
        public string WasteType { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 1000, ErrorMessage = "Amount must be a positive number.")]
        public decimal Amount { get; set; }

        // Foreign Key to the EmissionFactor entity
        public int EmissionFactorId { get; set; }

        // Navigation property
        public EmissionFactor? EmissionFactor { get; set; }
    }
}