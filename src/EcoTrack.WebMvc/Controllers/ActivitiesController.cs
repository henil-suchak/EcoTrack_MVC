using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EcoTrack.WebMvc.DTOs;
using EcoTrack.WebMvc.Enums;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.WebMvc.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly ISuggestionService _suggestionService;
        private readonly IBadgeService _badgeService; 

        public ActivitiesController(
            IActivityService activityService, 
            ISuggestionService suggestionService,
            IBadgeService badgeService)
        {
            _activityService = activityService;
            _suggestionService = suggestionService;
            _badgeService = badgeService;
        }

        // GET: /Activities/Log
        public IActionResult Log()
        {
            var viewModel = new LogActivityViewModel();
            // You can pre-populate dropdowns here if needed
            return View(viewModel);
        }

        // POST: /Activities/Log
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Log(LogActivityViewModel viewModel)
        {
            // --- Manual Validation Logic for each Activity Type ---
            switch (viewModel.ActivityType)
            {
                case ActivityType.Travel:
                    if (string.IsNullOrWhiteSpace(viewModel.TravelMode))
                        ModelState.AddModelError("TravelMode", "Mode of Transport is required.");
                    if (viewModel.Distance <= 0)
                        ModelState.AddModelError("Distance", "Distance must be a positive number.");
                    break;
                case ActivityType.Food:
                    if (string.IsNullOrWhiteSpace(viewModel.FoodType))
                        ModelState.AddModelError("FoodType", "Type of Food is required.");
                    if (viewModel.Quantity <= 0)
                        ModelState.AddModelError("Quantity", "Quantity must be a positive number.");
                    break;
                case ActivityType.Electricity:
                    if (viewModel.ElectricityConsumption <= 0)
                        ModelState.AddModelError("ElectricityConsumption", "Consumption must be a positive number.");
                    break;
                case ActivityType.Appliance:
                     if (string.IsNullOrWhiteSpace(viewModel.ApplianceType))
                        ModelState.AddModelError("ApplianceType", "Appliance Type is required.");
                     if (viewModel.UsageTime <= 0)
                        ModelState.AddModelError("UsageTime", "Usage Time must be a positive number.");
                     if (viewModel.PowerRating <= 0)
                        ModelState.AddModelError("PowerRating", "Power Rating must be a positive number.");
                    break;
                case ActivityType.Waste:
                    if (string.IsNullOrWhiteSpace(viewModel.WasteType))
                        ModelState.AddModelError("WasteType", "Waste Type is required.");
                    if (viewModel.Amount <= 0)
                        ModelState.AddModelError("Amount", "Amount must be a positive number.");
                    break;
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(userIdString, out var userId))
                {
                    return Unauthorized();
                }

                // Create the DTO from the ViewModel
                var activityDto = new LogActivityDto
                {
                    UserId = userId,
                    ActivityType = viewModel.ActivityType!.Value,
                    Distance = viewModel.Distance.GetValueOrDefault(),
                    Quantity = viewModel.Quantity.GetValueOrDefault(),
                    ElectricityConsumption = viewModel.ElectricityConsumption.GetValueOrDefault(),
                    UsageTime = viewModel.UsageTime.GetValueOrDefault(),
                    PowerRating = viewModel.PowerRating.GetValueOrDefault(),
                    Amount = viewModel.Amount.GetValueOrDefault(),
                    TravelMode = viewModel.TravelMode,
                    FoodType = viewModel.FoodType,
                    ApplianceType = viewModel.ApplianceType,
                    WasteType = viewModel.WasteType
                };
                
                // --- Call all necessary services in order ---
                var newActivity = await _activityService.LogActivityAsync(activityDto);
                await _suggestionService.GenerateSuggestionsForActivityAsync(newActivity);
                await _badgeService.CheckAndAwardBadgesAsync(userId);

                return RedirectToAction("Index", "Dashboard"); // Redirect to dashboard after logging
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View(viewModel);
            }
        }
    }
}