using System.Collections.Generic;

namespace EcoTrack.WebMvc.ViewModels
{

    public class LeaderboardPageViewModel
    {
        public IEnumerable<LeaderboardViewModel> Entries { get; set; } = new List<LeaderboardViewModel>();
        public string ActivePeriod { get; set; } = "Monthly";
    }
}