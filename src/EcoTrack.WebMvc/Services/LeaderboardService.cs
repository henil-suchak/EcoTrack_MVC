using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.ViewModels;

namespace EcoTrack.WebMvc.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaderboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<LeaderboardViewModel>> GetLeaderboardAsync(string period, int count)
        {
            var entries = await _unitOfWork.LeaderboardEntryRepository.GetTopEntriesAsync(period, count);


            return entries.Select(e => new LeaderboardViewModel
            {
                Rank = e.Rank,
                UserName = e.User?.Name ?? "Unknown User",
                CarbonEmission = e.CarbonEmission,
                UserId = e.UserId
            });
        }

        public async Task UpdateLeaderboardAsync(string period)
        {
            DateTime startDate;
            if (period.Equals("Weekly", StringComparison.OrdinalIgnoreCase))
{
    int diff = (7 + (DateTime.UtcNow.DayOfWeek - DayOfWeek.Monday)) % 7;
    startDate = DateTime.UtcNow.Date.AddDays(-diff);
}

            else if (period.Equals("Monthly", StringComparison.OrdinalIgnoreCase))
            {
                startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            }
            else
            {
                throw new ArgumentException("Invalid period specified. Use 'Weekly' or 'Monthly'.");
            }


            var recentActivities = await _unitOfWork.ActivityRepository.GetActivitiesSince(startDate);

            var userEmissions = recentActivities
                .GroupBy(a => a.UserId)
                .Select(group => new
                {
                    UserId = group.Key,
                    TotalEmission = group.Sum(a => a.CarbonEmission)
                })
                .OrderBy(u => u.TotalEmission)
                .ToList();

            var oldEntries = await _unitOfWork.LeaderboardEntryRepository.GetTopEntriesAsync(period, int.MaxValue);
            foreach (var oldEntry in oldEntries)
            {
                _unitOfWork.LeaderboardEntryRepository.Delete(oldEntry);
            }

            int rank = 1;
            foreach (var userResult in userEmissions)
            {
                var newEntry = new LeaderboardEntry
                {
                    UserId = userResult.UserId,
                    Period = period,
                    CarbonEmission = userResult.TotalEmission,
                    Rank = rank++
                };
                await _unitOfWork.LeaderboardEntryRepository.AddAsync(newEntry);
            }

            await _unitOfWork.CompleteAsync();
        }
        public async Task<LeaderboardEntry?> GetUserRankAsync(Guid userId, string period)
    {
        return await _unitOfWork.LeaderboardEntryRepository.GetEntryByUserAsync(userId, period);
    }
    }
}