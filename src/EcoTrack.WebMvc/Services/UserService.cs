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

        public async Task<User> RegisterUserAsync(string name, string email, string password, int age, string location, string? lifestylePreferences)
        {
            var existingUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Name = name,
                Email = email,
                PasswordHash = hashedPassword,
                // ADDED: Set the new properties
                Age = age,
                Location = location,
                LifestylePreferences = lifestylePreferences
            };

            await _unitOfWork.UserRepository.AddAsync(newUser);
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