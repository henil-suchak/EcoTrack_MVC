using System;
using System.Collections.Generic;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserWithActivitiesAsync(Guid userId);

    }
}