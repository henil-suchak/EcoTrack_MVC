using System;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Data;
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
    if (string.IsNullOrWhiteSpace(activityType))
        return null;

    activityType = activityType.Trim().ToLowerInvariant();
    subType = subType?.Trim().ToLowerInvariant() ?? string.Empty;

    // exact subtype match (case/space insensitive)
    var factor = await _context.EmissionFactors
        .FirstOrDefaultAsync(ef => ef.ActivityType.ToLower() == activityType && ef.SubType.ToLower() == subType);

    if (factor != null) return factor;

    // fallback: return any factor that matches the activity type (ignore subtype)
    factor = await _context.EmissionFactors
        .FirstOrDefaultAsync(ef => ef.ActivityType.ToLower() == activityType);

    return factor;
}

    }
}