using EcoTrack.WebMvc.Models;
namespace EcoTrack.WebMvc.Interfaces
{
    public interface IFamilyRepository: IGenericRepository<Family,Guid>
    {
        Task<Family?> GetFamilyWithMembersAsync(Guid FamilyId);
    }
}