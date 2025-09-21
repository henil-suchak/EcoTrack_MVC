using System;

namespace EcoTrack.WebMvc.ViewModels
{
    public class ActivityViewModel
    {
        public string ActivityType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal CarbonEmission { get; set; }
        public DateTime DateTime { get; set; }
    }
}