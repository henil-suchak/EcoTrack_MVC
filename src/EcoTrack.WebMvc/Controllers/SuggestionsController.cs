using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcoTrack.WebMvc.Models; // Add this if missing

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


        public async Task<IActionResult> Index(bool showAll = false)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            IEnumerable<Suggestion> suggestions;
            if (showAll)
            {

                suggestions = await _suggestionService.GetAllSuggestionsAsync(userId);
                ViewBag.Title = "All Suggestions";
                ViewBag.ShowAll = true;
            }
            else
            {

                suggestions = await _suggestionService.GetUnreadSuggestionsAsync(userId);
                ViewBag.Title = "New Suggestions";
                ViewBag.ShowAll = false;
            }
            
            var viewModel = _mapper.Map<IEnumerable<SuggestionViewModel>>(suggestions);

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(Guid suggestionId)
        {
            if (suggestionId != Guid.Empty)
            {
                await _suggestionService.MarkSuggestionAsReadAsync(suggestionId);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}