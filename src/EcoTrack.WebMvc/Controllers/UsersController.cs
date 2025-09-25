using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.ViewModels;
using EcoTrack.WebMvc.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace EcoTrack.WebMvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork; // Needed for fetching data for the edit form
        private readonly IMapper _mapper;       // Needed to map User -> UserEditViewModel

        public UsersController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // --- REGISTRATION ---
        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistrationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = await _userService.RegisterUserAsync(viewModel.Name, viewModel.Email, viewModel.Password);
                    await SignInUserAsync(newUser); // Automatically sign in after registration
                    return RedirectToAction("Index", "Dashboard");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("Email", ex.Message);
                }
            }
            return View(viewModel);
        }

        // --- LOGIN & LOGOUT ---
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.LoginAsync(viewModel.Email, viewModel.Password);
                if (user != null)
                {
                    await SignInUserAsync(user, viewModel.RememberMe);
                    return RedirectToAction("Index", "Dashboard");
                }
                ModelState.AddModelError("", "Invalid email or password.");
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // --- EDIT PROFILE ---
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<UserEditViewModel>(user);
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (viewModel.UserId.ToString() != userIdString)
            {
                return Unauthorized(); // Prevent users from editing other users' profiles
            }

            try
            {
                await _userService.UpdateUserAsync(viewModel);
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while saving your profile.");
                return View(viewModel);
            }
        }

        // --- PRIVATE HELPER METHOD FOR SIGN-IN ---
        private async Task SignInUserAsync(User user, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = isPersistent };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}