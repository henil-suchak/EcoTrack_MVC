using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.WebMvc.Repositories
{
    public class SuggestionRepository : GenericRepository<Suggestion, Guid>, ISuggestionRepository
    {
        public SuggestionRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implementation for the custom method.
        // It filters for the user AND where IsRead is false.
        public async Task<IEnumerable<Suggestion>> GetUnreadSuggestionsByUserIdAsync(Guid userId)
        {
            return await _context.Suggestions
                .Where(s => s.UserId == userId && s.IsRead == false)
                .OrderByDescending(s=>s.DateTimeIssued)
                .ToListAsync();
        }
        public async Task<IEnumerable<Suggestion>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Suggestions
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.DateTimeIssued)
                .ToListAsync();
        }
    }
}