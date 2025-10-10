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



        // GET: /Dashboard/Statistics

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Statistics(DashboardStatsViewModel statsViewModel)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }


            var newStatsViewModel = await _dashboardService.GetUserStatsAsync(userId, statsViewModel.StartDate, statsViewModel.EndDate);
            

            return View(newStatsViewModel);
        }
    }
}