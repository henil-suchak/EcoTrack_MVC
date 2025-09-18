
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTrack.WebMvc.Models
{
    public class FoodActivity : Activity
    {
        [Required]
        [StringLength(100)]
        public string FoodType { get; set; } = string.Empty; // e.g., "Beef", "Rice"

        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 1000, ErrorMessage = "Quantity must be a positive number.")]
        public decimal Quantity { get; set; } // in kg or servings

        [Required]
        [StringLength(100)]
        public string Source { get; set; } = string.Empty; // "home-cooked", "restaurant"
        
        // Foreign Key to the EmissionFactor entity
        public int EmissionFactorId { get; set; }
        
        // Navigation property
        public EmissionFactor? EmissionFactor { get; set; }
    }
}