using System.Threading.Tasks;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.DTO;
namespace EcoTrack.WebMvc.Interfaces
{
    public interface IActivityService
    {
        Task<Activity> LogActivityAsync(LogActivityDto dto);
    }
}