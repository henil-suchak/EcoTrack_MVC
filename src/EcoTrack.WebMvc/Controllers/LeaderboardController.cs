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

        public async Task<IActionResult> Index(string period = "Monthly")
        {
            // REMOVED: The expensive call to UpdateLeaderboardAsync().
            // This will now be handled automatically by your background service.
            
            var entries = await _leaderboardService.GetLeaderboardAsync(period, 20); // Show top 20
            
            ViewBag.CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new LeaderboardPageViewModel
            {
                Entries = entries,
                ActivePeriod = period
            };

            return View(viewModel);
        }
    }
}