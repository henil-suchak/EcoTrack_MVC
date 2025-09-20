using System;
using System.Linq;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BadgeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CheckAndAwardBadgesAsync(Guid userId)
        {
            // 1. Get all the data we need in one go.
            var userBadges = await _unitOfWork.BadgeRepository.GetBadgesByUserIdAsync(userId);
            var userActivities = await _unitOfWork.ActivityRepository.GetByUserIdAsync(userId);
            
            bool hasNewBadge = false;

            // --- Business Rule 1: "First Step" Badge ---
            // Criteria: Log at least 1 activity.
            if (!userBadges.Any(b => b.Name == "First Step") && userActivities.Any())
            {
                var newBadge = new Badge { /* ... Badge details ... */ };
                await _unitOfWork.BadgeRepository.AddAsync(newBadge);
                hasNewBadge = true;
            }

            // --- Business Rule 2: "Green Commuter" Badge ---
            // Criteria: Log 5 walking or cycling trips.
            if (!userBadges.Any(b => b.Name == "Green Commuter"))
            {
                int greenTrips = userActivities.OfType<TravelActivity>()
                    .Count(t => t.Mode.ToLower() == "cycle" || t.Mode.ToLower() == "walk");
                
                if (greenTrips >= 5)
                {
                    var newBadge = new Badge { /* ... Badge details ... */ };
                    await _unitOfWork.BadgeRepository.AddAsync(newBadge);
                    hasNewBadge = true;
                }
            }

            // --- Business Rule 3: "Veggie Lover" Badge ---
            // Criteria: Log 10 vegetarian meals.
            if (!userBadges.Any(b => b.Name == "Veggie Lover"))
            {
                int veggieMeals = userActivities.OfType<FoodActivity>()
                    .Count(f => f.FoodType.ToLower() == "vegetables");
                
                if (veggieMeals >= 10)
                {
                    var newBadge = new Badge { /* ... Badge details ... */ };
                    await _unitOfWork.BadgeRepository.AddAsync(newBadge);
                    hasNewBadge = true;
                }
            }
            
            // 2. If any new badges were added, save the changes.
            if (hasNewBadge)
            {
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}