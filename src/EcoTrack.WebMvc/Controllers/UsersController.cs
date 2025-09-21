using Microsoft.AspNetCore.Mvc;
using EcoTrack.WebMvc.Interfaces; // For IUserService
using EcoTrack.WebMvc.ViewModels; // For the ViewModels
using EcoTrack.WebMvc.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace EcoTrack.WebMvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        // The IUserService is injected by the DI container
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // --- REGISTRATION ---

        // This action responds to a GET request (e.g., when a user clicks "Register")
        // Its job is to simply display the empty registration form.
        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new UserRegistrationViewModel();
            return View(viewModel);
        }

        // This action responds to a POST request (when the user submits the form)
        [HttpPost]
        [ValidateAntiForgeryToken] // Security feature to prevent cross-site request forgery
        public async Task<IActionResult> Register(UserRegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Use the service to perform the registration logic
                    var newUser = await _userService.RegisterUserAsync(
                        viewModel.Name,
                        viewModel.Email,
                        viewModel.Password);

                    // TODO: Log the user in automatically after registration.
                    await SignInUserAsync(newUser);
                    // Redirect to the home page after successful registration
                    return RedirectToAction("Index", "Home");
                }
                catch (InvalidOperationException ex)
                {
                    // If the email already exists, add an error to the model state
                    // This will be displayed on the view.
                    ModelState.AddModelError("Email", ex.Message);
                }
            }

            // If the model state is not valid, or if there was an error,
            // return the view with the data the user already entered.
            return View(viewModel);
        }
        // In src/EcoTrack.WebMvc/Controllers/UsersController.cs

        // --- LOGIN ---

        // GET: /Users/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginViewModel());
        }

        // POST: /Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Use the service to validate the user
                var user = await _userService.LoginAsync(viewModel.Email, viewModel.Password);

                if (user != null)
                {
                    // TODO: Actually sign the user in using ASP.NET Core Identity or Cookies
                    // This is a critical security step for later.
                    await SignInUserAsync(user, viewModel.RememberMe);

                    return RedirectToAction("Index", "Home"); // Redirect to home on successful login
                }

                // If login fails, add an error message
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
         private async Task SignInUserAsync(User user, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}