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
        public ActivityRepository(ApplicationDbContext context) : base(context)
        {
        }

        // We still need to implement the special method here.
        public List<Activity> GetByUserId(Guid userId)
        {
            return _context.Activities
                           .Include(a => a.User)
                           .Where(a => a.UserId == userId)
                           .ToList();
        }

        // We can override GetAll() to add the .Include() logic
        public new List<Activity> GetAll()
        {
            return _context.Activities
                           .Include(a => a.User)
                           .ToList();
        }
    }
}