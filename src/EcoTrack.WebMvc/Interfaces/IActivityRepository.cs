using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    // No changes needed here, it's already using the new generic interface
    public interface IActivityRepository : IGenericRepository<Activity, Guid>
    {
        // UPDATED: Method renamed to follow the async convention
        Task<IEnumerable<Activity>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Activity>> GetActivitiesByUserIdSince(Guid userId, DateTime sinceDate);
        Task<IEnumerable<Activity>> GetActivitiesSince(DateTime sinceDate);
        Task<IEnumerable<Activity>> GetActivitiesForUserListSince(List<Guid> userIds, DateTime sinceDate);
        // Change the GetUserStatsAsync signature
        Task<(int, decimal)> GetUserStatsAsync(Guid userId, DateTime startDate, DateTime endDate);
    }
}