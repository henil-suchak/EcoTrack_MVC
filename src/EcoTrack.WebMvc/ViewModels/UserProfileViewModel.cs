using System;

namespace EcoTrack.WebMvc.ViewModels
{
    public class UserProfileViewModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? LifestylePreferences { get; set; }
        

        public int TotalActivitiesLogged { get; set; }
        public int BadgesEarned { get; set; }
    }
}