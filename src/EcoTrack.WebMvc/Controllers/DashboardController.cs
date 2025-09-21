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
        private readonly IUnitOfWork _unitOfWork; // Using UnitOfWork to access all repositories
        private readonly ILeaderboardService _leaderboardService;
        private readonly ISuggestionService _suggestionService;
        private readonly IMapper _mapper;

        public DashboardController(IUnitOfWork unitOfWork, ILeaderboardService leaderboardService, ISuggestionService suggestionService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _leaderboardService = leaderboardService;
            _suggestionService = suggestionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {

                return Unauthorized();
            }
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            var activities = await _unitOfWork.ActivityRepository.GetByUserIdAsync(userId);
            var suggestions = await _suggestionService.GetUnreadSuggestionsAsync(userId);
            var weeklyRank = await _leaderboardService.GetUserRankAsync(userId, "Weekly");
            var monthlyRank = await _leaderboardService.GetUserRankAsync(userId, "Monthly");
            var badges = await _unitOfWork.BadgeRepository.GetBadgesByUserIdAsync(userId);
            var viewModel = new DashboardViewModel
            {
                UserProfile = _mapper.Map<UserProfileViewModel>(user),
                RecentActivities = _mapper.Map<IEnumerable<ActivityViewModel>>(activities.OrderByDescending(a => a.DateTime).Take(5)),
                UnreadSuggestions = _mapper.Map<IEnumerable<SuggestionViewModel>>(suggestions),
                EarnedBadges = _mapper.Map<IEnumerable<BadgeViewModel>>(badges),
                WeeklyRankInfo = weeklyRank,
                MonthlyRankInfo = monthlyRank
            };

            return View(viewModel);
        }
    }
}