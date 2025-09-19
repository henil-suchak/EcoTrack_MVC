

using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    // Inherits generic methods for Badge, using Guid as the key.
    public interface IBadgeRepository : IGenericRepository<Badge, Guid>
    {
        // Custom method to get all badges for a specific user.
        Task<IEnumerable<Badge>> GetBadgesByUserIdAsync(Guid userId);
    }
}