using System;
using System.ComponentModel.DataAnnotations;

namespace EcoTrack.WebMvc.ViewModels
{
    public class SuggestionViewModel
    {
        public Guid SuggestionId { get; set; }

        public string Description { get; set; } = string.Empty;
        
        public string Category { get; set; } = string.Empty;

        [Display(Name = "Potential CO₂ Saving")]
        public string SavingAmountDisplay { get; set; } = string.Empty; 

        [Display(Name = "Received")]
        public string DateIssuedDisplay { get; set; } = string.Empty; 
    }
}