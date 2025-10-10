using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Repositories
{

    public class ActivityRepository : GenericRepository<Activity, Guid>, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext context) : base(context)
        {
        }


        public override async Task<IEnumerable<Activity>> GetAllAsync()
        {

            return await _context.Activities.Include(a => a.User).ToListAsync();
        }

       
        public async Task<IEnumerable<Activity>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Activities
                           .Include(a => a.User)
                           .Where(a => a.UserId == userId)
                           .ToListAsync();
        }
        public async Task<IEnumerable<Activity>> GetActivitiesByUserIdSince(Guid userId, DateTime sinceDate)
        {
            return await _context.Activities
                .Where(a => a.UserId == userId && a.DateTime >= sinceDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Activity>> GetActivitiesSince(DateTime sinceDate)
        {
            return await _context.Activities
                .Where(a => a.DateTime >= sinceDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Activity>> GetActivitiesForUserListSince(List<Guid> userIds, DateTime sinceDate)
        {
            return await _context.Activities
                .Where(a => userIds.Contains(a.UserId) && a.DateTime >= sinceDate)
                .ToListAsync();
        }
        public async Task<decimal> GetTotalEmissionsForUserAsync(Guid userId, DateTime since)
        {
            return await _context.Activities
                .Where(a => a.UserId == userId && a.DateTime >= since)
                .SumAsync(a => a.CarbonEmission);
        }

        public async Task<(int, decimal)> GetUserStatsAsync(Guid userId, DateTime startDate, DateTime endDate)
        {

            var inclusiveEndDate = endDate.AddDays(1);

            var query = _context.Activities
                .Where(a => a.UserId == userId && a.DateTime >= startDate && a.DateTime < inclusiveEndDate);

            var count = await query.CountAsync();
            var totalEmissions = await query.SumAsync(a => a.CarbonEmission);

            return (count, totalEmissions);
        }
    }
}