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
    // UPDATED: Inherits from the new generic repository with the key type
    public class ActivityRepository : GenericRepository<Activity, Guid>, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext context) : base(context)
        {
        }

        // UPDATED: Overriding the new async base method
        public override async Task<IEnumerable<Activity>> GetAllAsync()
        {
            // Your Include() is a great addition for loading related User data. Let's keep it.
            return await _context.Activities.Include(a => a.User).ToListAsync();
        }

        // NOTE: GetByIdAsync is now inherited from the generic base class.
        // You only need to override it if you want to add an .Include() here as well.

        // UPDATED: Custom method is now async
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
    }
}