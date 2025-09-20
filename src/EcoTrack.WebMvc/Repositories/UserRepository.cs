using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.WebMvc.Repositories
{
    public class UserRepository : GenericRepository<User, Guid>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        // --- ADD THIS METHOD IMPLEMENTATION ---
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            // FirstOrDefaultAsync will return the matching user or null if no user is found.
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}