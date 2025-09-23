using System.Security.Claims;
using EcoTrack.WebMvc.Interfaces;
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
        public async Task<IActionResult> Index()
        {
            var weeklyLeaderboard = await _leaderboardService.GetLeaderboardAsync("Weekly", 10);
            // var monthlyLeaderboard=await _leaderboardService.GetLeaderboardAsync("Monthly",10);
            ViewBag.CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(weeklyLeaderboard);
        }
        
    }

}