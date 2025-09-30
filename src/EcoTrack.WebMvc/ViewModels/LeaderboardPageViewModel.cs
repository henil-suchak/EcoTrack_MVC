using System.Collections.Generic;

namespace EcoTrack.WebMvc.ViewModels
{
    // This represents the ENTIRE page, containing a LIST of entries.
    public class LeaderboardPageViewModel
    {
        public IEnumerable<LeaderboardViewModel> Entries { get; set; } = new List<LeaderboardViewModel>();
        public string ActivePeriod { get; set; } = "Monthly";
    }
}