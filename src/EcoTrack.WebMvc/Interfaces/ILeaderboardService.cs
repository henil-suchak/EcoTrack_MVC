using System.Collections.Generic;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.ViewModels;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface ILeaderboardService
    {
        /// <summary>
        /// Gets the ranked leaderboard for a specific period.
        /// </summary>
        /// <param name="period">The period, e.g., "Weekly" or "Monthly".</param>
        Task<IEnumerable<LeaderboardViewModel>> GetLeaderboardAsync(string period, int count);

        /// <summary>
        /// Calculates total emissions for all users and updates the leaderboard entries.
        /// </summary>
        /// <param name="period">The period, e.g., "Weekly" or "Monthly".</param>
        Task UpdateLeaderboardAsync(string period);
         Task<LeaderboardEntry?> GetUserRankAsync(Guid userId, string period);
    }
}