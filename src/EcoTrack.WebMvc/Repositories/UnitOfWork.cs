using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;

namespace EcoTrack.WebMvc.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IUserRepository _userRepository;
        private IActivityRepository _activityRepository;

        public UnitOfWork(ApplicationDbContext context) { _context = context; }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IActivityRepository ActivityRepository => _activityRepository ??= new ActivityRepository(_context);

        public void Save() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}