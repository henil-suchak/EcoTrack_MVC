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
            // --- This is the more robust and recommended logic ---

            // 1. Clean up the input strings to be safe
            activityType = activityType.ToLowerInvariant().Trim();
            subType = subType.ToLowerInvariant().Trim();

            // 2. Try to find an exact match for both type and subtype
            var factor = await _context.EmissionFactors
                .FirstOrDefaultAsync(ef => 
                    ef.ActivityType.ToLower() == activityType && 
                    ef.SubType.ToLower() == subType);

            // 3. If an exact match is found, return it
            if (factor != null)
            {
                return factor;
            }

            // 4. FALLBACK: If no exact match, try to find a default factor for the main activity type
            //    This is useful for things like "Electricity" which might not have a subtype.
            return await _context.EmissionFactors
        .FirstOrDefaultAsync(ef =>
            ef.ActivityType.ToLower() == activityType.ToLower() &&
            ef.SubType.ToLower() == subType.ToLower());
        }
    }
}