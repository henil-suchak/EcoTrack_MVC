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
        public async Task<EmissionFactor?>  GetFactorAsync(string activityType, string subType)
        {
            return await _context.EmissionFactors
                .FirstOrDefaultAsync(ef => ef.ActivityType == activityType && ef.SubType == subType);
        }
    }
}