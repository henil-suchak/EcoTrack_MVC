using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.WebMvc.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardService dashboardService, IMapper mapper)
        {
            _dashboardService = dashboardService;
            _mapper = mapper;
        }

        // GET: /Dashboard
        // This is your main dashboard page.
        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var dashboardData = await _dashboardService.GetDashboardDataAsync(userId);
            if (dashboardData == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<DashboardViewModel>(dashboardData);
            return View(viewModel);
        }

        // --- NEW STATISTICS ACTIONS ---

        // GET: /Dashboard/Statistics
        // This action shows the statistics page with a default date range (last 7 days).
        [HttpGet]
        public async Task<IActionResult> Statistics()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }
            
            var statsViewModel = await _dashboardService.GetUserStatsAsync(userId, DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
            return View(statsViewModel);
        }

        // POST: /Dashboard/Statistics
        // This action handles the custom date range form submission.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Statistics(DashboardStatsViewModel statsViewModel)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            // Get the stats for the custom date range provided by the user.
            var newStatsViewModel = await _dashboardService.GetUserStatsAsync(userId, statsViewModel.StartDate, statsViewModel.EndDate);
            
            // Return the same view, but now populated with the new stats.
            return View(newStatsViewModel);
        }
    }
}