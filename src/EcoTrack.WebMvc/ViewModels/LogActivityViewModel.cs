using System.ComponentModel.DataAnnotations;
using EcoTrack.WebMvc.Enums;

namespace EcoTrack.WebMvc.ViewModels
{
    public class LogActivityViewModel
    {
        [Required]
        [Display(Name = "Activity Type")]
        public ActivityType ActivityType { get; set; }

        // --- Travel Properties ---
        [Display(Name = "Mode of Transport (e.g., Car, Bus)")]
        public string? TravelMode { get; set; }
        
        // REMOVED: [Range] attribute
        public decimal Distance { get; set; } 

        // --- Food Properties ---
        [Display(Name = "Type of Food (e.g., Beef, Chicken)")]
        public string? FoodType { get; set; }

        // REMOVED: [Range] attribute
        public decimal Quantity { get; set; }
    }
}