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
            var recentActivities = await _unitOfWork.ActivityRepository.GetByUserIdAsync(userId);
            var unreadSuggestions = await GetUnreadSuggestionsAsync(userId);
            bool hasNewSuggestion = false;

            // --- Business Rule for Travel ---
            var shortCarTrips = recentActivities.OfType<TravelActivity>()
                .Where(t => t.Mode == "Car" && t.Distance < 5);

            if (shortCarTrips.Any())
            {
                var suggestionText = "We noticed you took a short car trip. Try cycling or walking next time to save CO₂!";
                if (!unreadSuggestions.Any(s => s.Description == suggestionText))
                {
                    // CORRECTED: The object is now fully populated
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
                    hasNewSuggestion = true;
                }
            }
            // --- Business Rule for Food ---
            var meatMeals = recentActivities.OfType<FoodActivity>()
                .Count(f => f.FoodType.ToLower() == "beef" || f.FoodType.ToLower() == "lamb");

            if (meatMeals > 1)
            {
                var suggestionText = "Reducing meat consumption is a great way to lower your footprint. How about trying a 'Meatless Monday'?";
                if (!unreadSuggestions.Any(s => s.Description == suggestionText))
                {
                    // CORRECTED: The object is now fully populated
                    await _unitOfWork.SuggestionRepository.AddAsync(new Suggestion
                    {
                        UserId = userId,
                        Description = suggestionText,
                        Category = "Food",
                        SavingAmount = 20m,
                        IsRead = false,
                        DateTimeIssued = DateTime.UtcNow
                    });
                    hasNewSuggestion = true;
                }
            }

            // --- Business Rule for Electricity ---
            var totalKwh = recentActivities.OfType<ElectricityActivity>().Sum(e => e.Consumption);
            if (totalKwh > 70)
            {
                var suggestionText = "Your electricity usage seems high. Try switching to LED bulbs or unplugging electronics when not in use.";
                if (!unreadSuggestions.Any(s => s.Description == suggestionText))
                {
                    await _unitOfWork.SuggestionRepository.AddAsync(new Suggestion
                    {
                        UserId = userId,
                        Description = suggestionText,
                        Category = "Electricity",
                        SavingAmount = 5m,
                        IsRead = false,
                        DateTimeIssued = DateTime.UtcNow
                    });
                    hasNewSuggestion = true;
                }
            }

            // --- Business Rule for Appliance ---
            var acUsageHours = recentActivities.OfType<ApplianceActivity>().Where(a => a.ApplianceType == "AC").Sum(a => a.UsageTime);
            if (acUsageHours > 10)
            {
                var suggestionText = "Using your AC contributes to your footprint. Consider using a fan or setting the thermostat a few degrees higher.";
                if (!unreadSuggestions.Any(s => s.Description == suggestionText))
                {
                    await _unitOfWork.SuggestionRepository.AddAsync(new Suggestion
                    {
                        UserId = userId,
                        Description = suggestionText,
                        Category = "Appliance",
                        SavingAmount = 8m,
                        IsRead = false,
                        DateTimeIssued = DateTime.UtcNow
                    });
                    hasNewSuggestion = true;
                }
            }

            // --- Business Rule for Waste ---
            var landfillAmount = recentActivities.OfType<WasteActivity>().Where(w => w.WasteType == "Landfill").Sum(w => w.Amount);
            if (landfillAmount > 5)
            {
                var suggestionText = "Composting food scraps and choosing products with less packaging can greatly reduce landfill waste.";
                if (!unreadSuggestions.Any(s => s.Description == suggestionText))
                {
                    await _unitOfWork.SuggestionRepository.AddAsync(new Suggestion
                    {
                        UserId = userId,
                        Description = suggestionText,
                        Category = "Waste",
                        SavingAmount = 3m,
                        IsRead = false,
                        DateTimeIssued = DateTime.UtcNow
                    });
                    hasNewSuggestion = true;
                }
            }

            // Save any new suggestions that were created.
            if (hasNewSuggestion)
            {
                await _unitOfWork.CompleteAsync();
            }
        }
        public async Task<IEnumerable<Suggestion>> GetAllSuggestionsAsync(Guid userId)
        {
            return await _unitOfWork.SuggestionRepository.GetAllByUserIdAsync(userId);
        }
    }
}