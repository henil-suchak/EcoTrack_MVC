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
    public class BadgeRepository : GenericRepository<Badge, Guid>, IBadgeRepository
    {
        public BadgeRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implementation for the custom method defined in the interface.
        public async Task<IEnumerable<Badge>> GetBadgesByUserIdAsync(Guid userId)
        {
            return await _context.Badges
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}