using Microsoft.AspNetCore.Mvc;
using EcoTrack.WebMvc.Interfaces; // For IUserService
using EcoTrack.WebMvc.ViewModels; // For the ViewModels

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
    }
}