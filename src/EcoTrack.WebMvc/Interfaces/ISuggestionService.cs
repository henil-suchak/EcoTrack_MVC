using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface ISuggestionService
    {
        Task<IEnumerable<Suggestion>> GetUnreadSuggestionsAsync(Guid userId);
        Task<IEnumerable<Suggestion>> GetAllSuggestionsAsync(Guid userId);
        Task MarkSuggestionAsReadAsync(Guid suggestionId);
        
        // UPDATED: The method now takes the specific activity as a parameter
        Task GenerateSuggestionsForActivityAsync(Activity activity);
    }
}