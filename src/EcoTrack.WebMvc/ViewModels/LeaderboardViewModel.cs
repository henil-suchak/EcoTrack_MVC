using System;

namespace EcoTrack.WebMvc.ViewModels
{
    // This represents a SINGLE entry on the leaderboard.
    public class LeaderboardViewModel
    {
        public int Rank { get; set; }
        public string UserName { get; set; } = string.Empty;
        public decimal CarbonEmission { get; set; }
        public Guid UserId { get; set; }
    }
}