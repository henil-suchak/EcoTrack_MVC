using System;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IFamilyService
    {
        Task<Family> CreateFamilyAsync(string familyName, Guid creatorUserId);

        
        Task AddMemberAsync(Guid familyId, Guid userId);

        
        Task RemoveMemberAsync(Guid familyId, Guid userId);
        
       
        Task<decimal> GetFamilyTotalEmissionAsync(Guid familyId, string period);

        
        Task<Family?> GetFamilyDetailsAsync(Guid familyId);
    }
}