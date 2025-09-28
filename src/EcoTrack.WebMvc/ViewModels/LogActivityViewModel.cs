using System.ComponentModel.DataAnnotations;
using EcoTrack.WebMvc.Enums;

namespace EcoTrack.WebMvc.ViewModels
{
    public class LogActivityViewModel 
    {
        [Required(ErrorMessage = "Please select an activity type.")]
        [Display(Name = "Activity Type")]
        public ActivityType? ActivityType { get; set; } // Make the enum nullable

        // --- Travel Properties ---
        [Display(Name = "Mode of Transport (e.g., Car, Bus,Train)")]
        public string? TravelMode { get; set; }
        public decimal? Distance { get; set; }

        // --- Food Properties ---
        [Display(Name = "Type of Food (e.g., Beef, Chicken,vegetables)")]
        public string? FoodType { get; set; }
        public decimal? Quantity { get; set; }

        // --- Electricity Properties ---
        [Display(Name = "Consumption (kWh)")]
        public decimal? ElectricityConsumption { get; set; }

        // --- Appliance Properties ---
        [Display(Name = "Appliance Type (e.g., Fridge, AC,Grid)")]
        public string? ApplianceType { get; set; }
        [Display(Name = "Usage Time (hours)")]
        public decimal? UsageTime { get; set; }
        [Display(Name = "Power Rating (watts)")]
        public decimal? PowerRating { get; set; }
        
        // --- Waste Properties ---
        [Display(Name = "Waste Type (e.g., Recyclable,Landfill)")]
        public string? WasteType { get; set; }
        [Display(Name = "Amount (kg)")]
        public decimal? Amount { get; set; }
    }
}