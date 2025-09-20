using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.WebMvc.Repositories
{
    public class FamilyRepository : GenericRepository<Family, Guid>, IFamilyRepository
    {

        public FamilyRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<Family?> GetFamilyWithMembersAsync(Guid familyId)
        {
            return await _context.Families
                .Include(f => f.Members)
                .FirstOrDefaultAsync(f => f.FamilyId == familyId);
        }
    }
}