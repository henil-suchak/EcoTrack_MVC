using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        // No need for a private _context here, the base class already has a protected one.
        public ActivityRepository(ApplicationDbContext context) : base(context) 
        {
        }

        // This will now work correctly because the base method is virtual.
        public override List<Activity> GetAll()
        {
            return _context.Activities.Include(a => a.User).ToList();
        }
        
        // The return type is Activity? to match the base class and interface.
        public override Activity? GetById(Guid id)
        {
            return _context.Activities.FirstOrDefault(a => a.ActivityId == id);
        }

        public List<Activity> GetByUserId(Guid userId)
        {
            return _context.Activities
                           .Include(a => a.User)
                           .Where(a => a.UserId == userId)
                           .ToList();
        }
    }
}