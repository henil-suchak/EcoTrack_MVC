using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrack.WebMvc.Controllers
{
    [Authorize]
    public class BadgesController : Controller
    {
        private readonly IBadgeService _badgeService;
        private readonly IMapper _mapper;

        public BadgesController(IBadgeService badgeService, IMapper mapper)
        {
            _badgeService = badgeService;
            _mapper = mapper;
        }

        // GET: /Badges
        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var badges = await _badgeService.GetUserBadgesAsync(userId);
            var viewModel = _mapper.Map<IEnumerable<BadgeViewModel>>(badges);

            return View(viewModel);
        }
    }
}