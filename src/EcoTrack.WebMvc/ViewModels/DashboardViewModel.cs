using System.Collections.Generic;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.ViewModels
{
    public class DashboardViewModel
    {
        public UserProfileViewModel UserProfile { get; set; } = new();
        public IEnumerable<ActivityViewModel> RecentActivities { get; set; } = new List<ActivityViewModel>();
        public IEnumerable<SuggestionViewModel> UnreadSuggestions { get; set; } = new List<SuggestionViewModel>();
        public IEnumerable<BadgeViewModel> EarnedBadges { get; set; } = new List<BadgeViewModel>();
        public LeaderboardEntry? WeeklyRankInfo { get; set; }
        public LeaderboardEntry? MonthlyRankInfo { get; set; }

        public decimal TotalCarbonEmitted { get; set; }
    }
}