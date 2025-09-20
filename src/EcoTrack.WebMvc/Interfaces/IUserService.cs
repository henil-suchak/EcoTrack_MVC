using System.Threading.Tasks;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string name, string email, string password);
        Task<User?> LoginAsync(string email, string password); 
    }
}