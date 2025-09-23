using System;
using System.Threading.Tasks;
using AutoMapper;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using EcoTrack.WebMvc.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EcoTrack.WebMvc.Controllers
{   
     [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly IActivityService _activityService;
         private readonly ISuggestionService _suggestionService;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivityService activityService, ISuggestionService suggestionService, IMapper mapper)
        {
            _activityService = activityService;
            _suggestionService = suggestionService;
            _mapper = mapper;
        }

        public IActionResult Log()
        {
            return View(new LogActivityViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Log(LogActivityViewModel viewModel)
        {
            // Manual Validation Logic
            if (viewModel.ActivityType == ActivityType.Travel && (string.IsNullOrWhiteSpace(viewModel.TravelMode) || viewModel.Distance <= 0))
            {
                ModelState.AddModelError("", "For Travel, Mode and Distance are required.");
            }
            else if (viewModel.ActivityType == ActivityType.Food && (string.IsNullOrWhiteSpace(viewModel.FoodType) || viewModel.Quantity <= 0))
            {
                ModelState.AddModelError("", "For Food, Type and Quantity are required.");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                Activity newActivity;
                switch (viewModel.ActivityType)
                {
                    case ActivityType.Travel:
                        newActivity = new TravelActivity
                        {
                            Mode = viewModel.TravelMode!,
                            Distance = viewModel.Distance
                        };
                        break;
                    case ActivityType.Food:
                        newActivity = new FoodActivity
                        {
                            FoodType = viewModel.FoodType!,
                            Quantity = viewModel.Quantity
                        };
                        break;
                    default:
                        ModelState.AddModelError("ActivityType", "Selected activity type is not supported yet.");
                        return View(viewModel);
                }

                newActivity.ActivityType = viewModel.ActivityType;
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString))
                {
                    // This should not happen if [Authorize] is used, but it's a good safeguard
                    return Unauthorized();
                }
                // IMPORTANT: Make sure this is a REAL UserId that exists in your Users table.
                // Go to your database, copy the Guid for a user you created, and paste it here.
                // newActivity.UserId = Guid.Parse("C81E6A10-67E3-4228-BB74-D2668A54E8C0");
                 newActivity.UserId = Guid.Parse(userIdString);
                await _activityService.LogActivityAsync(newActivity);
                 await _suggestionService.GenerateSuggestionsForUserAsync(newActivity.UserId);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View(viewModel);
            }
        }
    }
}