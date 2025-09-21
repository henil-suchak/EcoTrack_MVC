using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface ILeaderboardEntryRepository : IGenericRepository<LeaderboardEntry, Guid>
    {
        Task<IEnumerable<LeaderboardEntry>> GetTopEntriesAsync(string period, int count);
         Task<LeaderboardEntry?> GetEntryByUserAsync(Guid userId, string period);
    }
}