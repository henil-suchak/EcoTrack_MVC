using System.Threading.Tasks;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IActivityService
    {
        Task<Activity> LogActivityAsync(Activity activity);
    }
}