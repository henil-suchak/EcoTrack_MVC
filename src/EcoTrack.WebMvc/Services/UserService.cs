using System;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using BCrypt.Net;
using EcoTrack.WebMvc.ViewModels;
namespace EcoTrack.WebMvc.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> RegisterUserAsync(string name, string email, string password)
        {
            // 1. Business Rule: Check if a user with this email already exists.
            var existingUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            // 2. Business Rule: Hash the password (CRITICAL FOR SECURITY)
            // NEVER store plain-text passwords. This is a placeholder.
            // In a real app, you would use a library like BCrypt.Net here.
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); // TODO: Replace with real hashing logic

            // 3. Create the new User entity
            var newUser = new User
            {
                UserId = Guid.NewGuid(), // Generate a new Guid for the user
                Name = name,
                Email = email,
                PasswordHash = hashedPassword
            };

            // 4. Add the new user via the repository
            await _unitOfWork.UserRepository.AddAsync(newUser);

            // 5. Save all changes to the database
            await _unitOfWork.CompleteAsync();

            return newUser;
        }
        // In src/EcoTrack.WebMvc/Services/UserService.cs
        public async Task<User?> LoginAsync(string email, string password)
        {
            // 1. Find the user by email using the repository
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return null; // User not found
            }

            // 2. Verify the password
            

            var hashedPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!hashedPassword)
            {
                return null; // Password does not match
            }

            // 3. If password is correct, return the user object
            return user;
        }
        // In your UserService class, add this new method:
        public async Task UpdateUserAsync(UserEditViewModel viewModel)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(viewModel.UserId);

            if (user != null)
            {
                // Update the properties from the ViewModel
                user.Name = viewModel.Name;
                user.Age = viewModel.Age;
                user.Location = viewModel.Location;
                user.LifestylePreferences = viewModel.LifestylePreferences;

                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}