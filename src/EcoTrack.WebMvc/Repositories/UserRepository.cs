using System;
using System.Collections.Generic;
using System.Linq;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) { _context = context; }

        public List<User> GetAll() => _context.Users.ToList();
        public User GetById(Guid id) => _context.Users.FirstOrDefault(u => u.UserId == id);
        public void Insert(User user) => _context.Users.Add(user);
        public void Update(User user) => _context.Users.Update(user);
        public void Delete(User user) => _context.Users.Remove(user);
    }
}