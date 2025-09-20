using System.ComponentModel.DataAnnotations;

namespace EcoTrack.WebMvc.ViewModels
{
    public class BadgeViewModel
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string IconUrl { get; set; } = string.Empty;

        [Display(Name = "Earned On")]
        public string DateEarnedDisplay { get; set; } = string.Empty;
    }
}