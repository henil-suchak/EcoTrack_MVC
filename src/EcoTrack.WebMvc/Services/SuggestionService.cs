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

        public async Task GenerateSuggestionsForActivityAsync(Activity activity)
        {
            string? suggestionText = null;
            string category = "";

            // Use a switch to create a specific suggestion for the logged activity
            switch (activity)
            {
                // UPDATED: This case now has its own switch for subtypes
                case TravelActivity travel:
                    category = "Travel";
                    switch (travel.Mode.ToLower()) // Check the specific mode
                    {
                        case "car" when travel.Distance < 5:
                            suggestionText = "We noticed you took a short car trip. Try cycling or walking next time!";
                            break;
                        case "bus":
                            suggestionText = "Taking the bus is a great choice for reducing traffic and emissions!";
                            break;
                        case "train":
                            suggestionText = "Train travel is one of the most eco-friendly ways to travel long distances. Great job!";
                            break;
                    }
                    break;

                // UPDATED: This case now handles multiple food subtypes
                case FoodActivity food:
                    category = "Food";
                    switch (food.FoodType.ToLower()) // Check the specific food type
                    {
                        case "beef":
                    
                            suggestionText = "Reducing red meat consumption is a great way to lower your footprint. How about trying chicken or vegetables?";
                            break;
                        case "chicken":
                            suggestionText = "Chicken is a great lower-impact choice. To reduce your footprint even further, consider plant-based proteins like beans or lentils.";
                            break;
                        case "vegetables":
                            suggestionText = "Excellent choice! Plant-based meals are one of the best ways to reduce your carbon footprint.";
                            break;
                    }
                    break;

                case ElectricityActivity electricity when electricity.Consumption > 20: // e.g., > 20 kWh in one go
                    suggestionText = "High electricity usage can be reduced by switching to LED bulbs and energy-efficient appliances.";
                    category = "Electricity";
                    break;

                case WasteActivity waste:
                    category = "Waste";
                    switch (waste.WasteType.ToLower()) // Check the specific waste type
                    {
                        case "landfill" when waste.Amount > 2:
                            suggestionText = "Composting food scraps and choosing products with less packaging can greatly reduce landfill waste.";
                            break;
                        case "recyclable":
                            suggestionText = "Great job recycling! This significantly reduces the need for raw materials and saves energy.";
                            break;
                    }
                    break;
                case ApplianceActivity appliance:
                   var suggestions = new List<string>();

    if (appliance.ApplianceType.ToLower() == "ac" && appliance.UsageTime > 2)
    {
        suggestions.Add("Using your AC contributes to your footprint. Consider using a fan or setting the thermostat a few degrees higher.");
    }

    if (appliance.PowerRating > 1500)
    {
        suggestions.Add("High-power appliances can use a lot of energy. Remember to turn them off when not in use!");
    }

    foreach (var text in suggestions)
    {
        var newSuggestion = new Suggestion
        {
            SuggestionId = Guid.NewGuid(),
            UserId = activity.UserId,
            Description = text,
            Category = category,
            SavingAmount = 0,
            IsRead = false,
            DateTimeIssued = DateTime.UtcNow
        };
        await _unitOfWork.SuggestionRepository.AddAsync(newSuggestion);
    }

    if (suggestions.Count > 0)
        await _unitOfWork.CompleteAsync();
                    break;

            }

            // If a relevant suggestion was found, create and save it.
            if (!string.IsNullOrEmpty(suggestionText))
            {
                var newSuggestion = new Suggestion
                {
                    SuggestionId = Guid.NewGuid(),
                    UserId = activity.UserId,
                    Description = suggestionText,
                    Category = category,
                    SavingAmount = 0, // You can calculate a more specific saving later
                    IsRead = false,
                    DateTimeIssued = DateTime.UtcNow
                };
                await _unitOfWork.SuggestionRepository.AddAsync(newSuggestion);
                await _unitOfWork.CompleteAsync();
            }
        }
        public async Task<IEnumerable<Suggestion>> GetAllSuggestionsAsync(Guid userId)
        {
            return await _unitOfWork.SuggestionRepository.GetAllByUserIdAsync(userId);
        }
    }
}