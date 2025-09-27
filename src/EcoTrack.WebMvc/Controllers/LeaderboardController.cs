using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.ViewModels; 
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.WebMvc.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ILeaderboardService _leaderboardService;
        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        // The 'period' parameter will come from the button links in the view.
        // It defaults to "Monthly" if no period is provided.
        public async Task<IActionResult> Index(string period = "Monthly")
        {
            await _leaderboardService.UpdateLeaderboardAsync(period);
            var entries = await _leaderboardService.GetLeaderboardAsync(period, 10);
            

            ViewBag.CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Create the new ViewModel to pass all necessary data to the view.
            var viewModel = new LeaderboardPageViewModel
            {
                Entries = entries,
                ActivePeriod = period
            };

            return View(viewModel);
        }
    }
}