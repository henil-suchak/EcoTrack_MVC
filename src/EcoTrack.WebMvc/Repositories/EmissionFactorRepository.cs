using System;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.WebMvc.Repositories
{
    public class EmissionFactorRepository : GenericRepository<EmissionFactor, Guid>, IEmissionFactorRepository
    {
        public EmissionFactorRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<EmissionFactor?> GetFactorAsync(string activityType, string subType)
        {
            
            activityType = activityType.ToLowerInvariant().Trim();
            subType = subType.ToLowerInvariant().Trim();


            var factor = await _context.EmissionFactors
                .FirstOrDefaultAsync(ef => 
                    ef.ActivityType.ToLower() == activityType && 
                    ef.SubType.ToLower() == subType);


            if (factor != null)
            {
                return factor;
            }

            
            return await _context.EmissionFactors
        .FirstOrDefaultAsync(ef =>
            ef.ActivityType.ToLower() == activityType.ToLower() &&
            ef.SubType.ToLower() == subType.ToLower());
        }
    }
}