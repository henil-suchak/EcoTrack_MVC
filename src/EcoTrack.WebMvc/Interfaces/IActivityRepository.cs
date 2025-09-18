using System;
using System.Collections.Generic;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        // We can add methods here that ONLY apply to Activities.
        List<Activity> GetByUserId(Guid userId);
    }
}