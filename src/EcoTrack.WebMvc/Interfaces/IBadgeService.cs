using System;
using System.Threading.Tasks;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IBadgeService
    {
        /// <summary>
        /// Checks a user's activities against badge criteria and awards any new badges.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        Task CheckAndAwardBadgesAsync(Guid userId);
    }
}