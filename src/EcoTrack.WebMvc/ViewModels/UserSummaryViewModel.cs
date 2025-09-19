using System;

namespace EcoTrack.WebMvc.ViewModels
{
    public class UserSummaryViewModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}