using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Repositories; 
namespace EcoTrack.WebMvc.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IUserRepository? _userRepository;
        private IActivityRepository? _activityRepository;
        private IBadgeRepository? _badgeRepository;
        private ISuggestionRepository? _suggestionRepository;
        private ILeaderboardEntryRepository? _leaderboardEntryRepository; // ADD THIS LINE BACK

        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context; 
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IActivityRepository ActivityRepository => _activityRepository ??= new ActivityRepository(_context);
        public IBadgeRepository BadgeRepository => _badgeRepository ??= new BadgeRepository(_context);
        public ISuggestionRepository SuggestionRepository => _suggestionRepository ??= new SuggestionRepository(_context);
        public ILeaderboardEntryRepository LeaderboardEntryRepository => _leaderboardEntryRepository ??= new LeaderboardEntryRepository(_context); // ADD THIS LINE BACK

        public void Save() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}