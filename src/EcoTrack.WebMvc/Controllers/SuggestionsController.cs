using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.WebMvc.Controllers
{
    [Authorize]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionService _suggestionService;
        private readonly IMapper _mapper;

        public SuggestionsController(ISuggestionService suggestionService, IMapper mapper)
        {
            _suggestionService = suggestionService;
            _mapper = mapper;
        }

        // GET: /Suggestions
        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            // 1. Get the raw suggestion models from the service
            var suggestions = await _suggestionService.GetUnreadSuggestionsAsync(userId);
            
            // 2. Map them to the ViewModel for display
            var viewModel = _mapper.Map<IEnumerable<SuggestionViewModel>>(suggestions);

            return View(viewModel);
        }

        // POST: /Suggestions/MarkAsRead/some-guid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(Guid suggestionId)
        {
            if (suggestionId != Guid.Empty)
            {
                await _suggestionService.MarkSuggestionAsReadAsync(suggestionId);
            }
            
            // Redirect back to the suggestions list
            return RedirectToAction(nameof(Index));
        }
    }
}