using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{

    public interface ISuggestionService
    {
        Task GenerateSuggestionsForUserAsync(Guid userId);

        Task<IEnumerable<Suggestion>> GetUnreadSuggestionsAsync(Guid userId);
         Task MarkSuggestionAsReadAsync(Guid suggestionId);
    }
}