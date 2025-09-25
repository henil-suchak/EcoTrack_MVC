using System;
using System.Collections.Generic;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.DTO
{
    public class DashboardDto
    {
        public User? UserProfile { get; set; }
        public IEnumerable<Activity> RecentActivities { get; set; } = new List<Activity>();
        public IEnumerable<Suggestion> UnreadSuggestions { get; set; } = new List<Suggestion>();
        public IEnumerable<Badge> EarnedBadges { get; set; } = new List<Badge>();
        public LeaderboardEntry? WeeklyRankInfo { get; set; }
    }
}