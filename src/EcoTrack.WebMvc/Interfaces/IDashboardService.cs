using System;
using System.Threading.Tasks;
using EcoTrack.WebMvc.DTO;
using EcoTrack.WebMvc.ViewModels;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto?> GetDashboardDataAsync(Guid userId);
        Task<DashboardStatsViewModel> GetUserStatsAsync(Guid userId, DateTime startDate, DateTime endDate);    }
}