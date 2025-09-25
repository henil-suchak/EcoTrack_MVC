using System;
using System.Threading.Tasks;
using EcoTrack.WebMvc.DTO;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto?> GetDashboardDataAsync(Guid userId);
    }
}