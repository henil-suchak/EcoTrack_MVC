using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EcoTrack.WebMvc.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [Range(0.1, 10000)]
        public decimal Distance { get; set; }

        // --- Food Properties ---
        [Display(Name = "Type of Food (e.g., Beef, Chicken)")]
        public string? FoodType { get; set; }
        [Range(0.1, 100)]
        public decimal Quantity { get; set; }

        // ... You can add properties for other activity types here
    }
}