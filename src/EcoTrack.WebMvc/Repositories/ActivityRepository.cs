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
        private readonly ApplicationDbContext _context;
        public ActivityRepository(ApplicationDbContext context) { _context = context; }

        public List<Activity> GetAll()
        {
            // Include User data so the user's name can be displayed in the view.
            return _context.Activities.Include(a => a.User).ToList();
        }

        public Activity GetById(Guid id)
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

        // We can override GetAll() to add the .Include() logic
        public new List<Activity> GetAll()
        {
            return _context.Activities
                           .Include(a => a.User)
                           .ToList();
        }
    }
}