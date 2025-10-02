using System;
using System.Linq;
using System.Threading.Tasks;
using EcoTrack.WebMvc.DTO;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.ViewModels;

namespace EcoTrack.WebMvc.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardDto?> GetDashboardDataAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserWithActivitiesAsync(userId);
            if (user == null)
            {
                return null;
            }

            var suggestions = await _unitOfWork.SuggestionRepository.GetUnreadSuggestionsByUserIdAsync(userId);
            var badges = await _unitOfWork.BadgeRepository.GetBadgesByUserIdAsync(userId);
            var weeklyRank = await _unitOfWork.LeaderboardEntryRepository.GetEntryByUserAsync(userId, "Weekly");
        
            var dashboardData = new DashboardDto
            {
                UserProfile = user,
                RecentActivities = user.Activities.OrderByDescending(a => a.DateTime).Take(10),
                UnreadSuggestions = suggestions,
                EarnedBadges = badges,
                WeeklyRankInfo = weeklyRank,
               
            };

            return dashboardData;
        }
        // In your DashboardService class, add this new method:
        public async Task<DashboardStatsViewModel> GetUserStatsAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            // The repository is now called with the specific dates
            var (count, total) = await _unitOfWork.ActivityRepository.GetUserStatsAsync(userId, startDate, endDate);

            return new DashboardStatsViewModel
            {
                NumberOfActivities = count,
                TotalCarbonEmitted = total,
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}