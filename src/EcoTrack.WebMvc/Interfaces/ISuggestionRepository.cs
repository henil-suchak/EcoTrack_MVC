using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface ISuggestionRepository : IGenericRepository<Suggestion, Guid>
    {
        // Custom method to get all unread suggestions for a specific user.
        Task<IEnumerable<Suggestion>> GetUnreadSuggestionsByUserIdAsync(Guid userId);
    }
}