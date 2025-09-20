using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.WebMvc.Repositories
{
    public class LeaderboardEntryRepository : GenericRepository<LeaderboardEntry, Guid>, ILeaderboardEntryRepository
    {
        public LeaderboardEntryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LeaderboardEntry>> GetTopEntriesAsync(string period, int count)
        {
            return await _context.LeaderboardEntries
                .Include(e => e.User)
                .Where(e => e.Period == period)
                .OrderBy(e => e.Rank)
                .Take(count)
                .ToListAsync();
        }
    }
}