using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IEmissionFactorRepository
    {
        Task<EmissionFactor?> GetFactorAsync(string activityType, string subType);
    }
}