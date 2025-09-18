using System;
using System.Collections.Generic;
using EcoTrack.WebMvc.Models.User;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(Guid id);
        void Insert(User user);
        void Update(User user);
        void Delete(User user);
    }
}