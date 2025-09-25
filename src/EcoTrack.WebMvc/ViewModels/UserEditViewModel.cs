using System;
using System.ComponentModel.DataAnnotations;

namespace EcoTrack.WebMvc.ViewModels
{
    public class UserEditViewModel
    {
        // Hidden field in the form to identify the user
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [StringLength(150)]
        public string Location { get; set; } = string.Empty;
        
        [Display(Name = "Lifestyle Preferences")]
        public string? LifestylePreferences { get; set; }
    }
}