using System;
using System.Threading.Tasks;
using AutoMapper;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.WebMvc.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivityService activityService, IMapper mapper)
        {
            _activityService = activityService;
            _mapper = mapper;
        }

        // GET: Activities/Log
        // This action shows the empty form to the user.
        public IActionResult Log()
        {
            var viewModel = new LogActivityViewModel();
            return View(viewModel);
        }

        // POST: Activities/Log
        // This action processes the form data when the user clicks "Submit".
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Log(LogActivityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel); // Return the form with validation errors
            }

            try
            {
                Activity newActivity;
                // Based on the selected type, map to the correct model
                switch (viewModel.ActivityType)
                {
                    case Enums.ActivityType.Travel:
                        newActivity = _mapper.Map<TravelActivity>(viewModel);
                        // Manually map properties that don't match by name
                        ((TravelActivity)newActivity).Mode = viewModel.TravelMode ?? "Unknown";
                        break;
                    case Enums.ActivityType.Food:
                        newActivity = _mapper.Map<FoodActivity>(viewModel);
                        break;
                    // Add cases for other activity types
                    default:
                        // Handle error
                        return View(viewModel);
                }

                // A placeholder for the logged-in user's ID
                newActivity.UserId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                
                await _activityService.LogActivityAsync(newActivity);
                return RedirectToAction("Index", "Home"); // Redirect on success
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View(viewModel);
            }
        }
    }
}