using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Services
{
    public class SuggestionService : ISuggestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SuggestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Suggestion>> GetUnreadSuggestionsAsync(Guid userId)
        {
            // This simply calls the custom repository method you already created.
            return await _unitOfWork.SuggestionRepository.GetUnreadSuggestionsByUserIdAsync(userId);
        }

        public async Task MarkSuggestionAsReadAsync(Guid suggestionId)
        {
            var suggestion = await _unitOfWork.SuggestionRepository.GetByIdAsync(suggestionId);
            if (suggestion != null)
            {
                suggestion.IsRead = true;
                _unitOfWork.SuggestionRepository.Update(suggestion);
                await _unitOfWork.CompleteAsync();
            }
        }
        
        public async Task GenerateSuggestionsForUserAsync(Guid userId)
        {
            // 1. Fetch the user's activities from the last 7 days.
            var recentActivities = await _unitOfWork.ActivityRepository.GetActivitiesByUserIdSince(userId, DateTime.UtcNow.AddDays(-7));
            var unreadSuggestions = await GetUnreadSuggestionsAsync(userId);

            // --- Business Rule 1: Suggest cycling for short car trips ---
            var shortCarTrips = recentActivities.OfType<TravelActivity>()
                .Where(t => t.Mode == "Car" && t.Distance < 5); // Trips less than 5 km

            if (shortCarTrips.Any())
            {
                var suggestionText = "We noticed you took a short car trip. Try cycling or walking next time to save CO₂!";
                // Avoid creating a duplicate suggestion if one already exists.
                if (!unreadSuggestions.Any(s => s.Description == suggestionText))
                {
                    var newSuggestion = new Suggestion
                    {
                        UserId = userId,
                        Description = suggestionText,
                        Category = "Travel",
                        SavingAmount = 1.5m, // Placeholder saving amount
                        IsRead = false,
                        DateTimeIssued = DateTime.UtcNow
                    };
                    await _unitOfWork.SuggestionRepository.AddAsync(newSuggestion);
                }
            }

            // --- Business Rule 2: Suggest "Meatless Monday" ---
            var meatMeals = recentActivities.OfType<FoodActivity>()
                .Count(f => f.FoodType.ToLower() == "beef" || f.FoodType.ToLower() == "lamb");

            if (meatMeals > 1)
            {
                var suggestionText = "Reducing meat consumption is a great way to lower your footprint. How about trying a 'Meatless Monday'?";
                if (!unreadSuggestions.Any(s => s.Description == suggestionText))
                {
                    var newSuggestion = new Suggestion
                    {
                        UserId = userId,
                        Description = suggestionText,
                        Category = "Food",
                        SavingAmount = 20m, // Placeholder saving amount
                        IsRead = false,
                        DateTimeIssued = DateTime.UtcNow
                    };
                    await _unitOfWork.SuggestionRepository.AddAsync(newSuggestion);
                }
            }

            // You can add many more rules here for electricity, waste, etc.

            // 2. Save any new suggestions that were created.
            await _unitOfWork.CompleteAsync();
        }
    }
}