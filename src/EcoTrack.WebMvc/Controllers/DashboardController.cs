using System;
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

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            // 1. Call the service to get all business data
            var dashboardData = await _dashboardService.GetDashboardDataAsync(userId);
            if (dashboardData == null)
            {
                return NotFound();
            }

            // 2. Use AutoMapper to map the DTO to the ViewModel for the UI
            var viewModel = _mapper.Map<DashboardViewModel>(dashboardData);
            
            return View(viewModel);
        }
    }
}