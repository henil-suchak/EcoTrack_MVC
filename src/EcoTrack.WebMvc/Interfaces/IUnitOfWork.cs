namespace EcoTrack.WebMvc.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IActivityRepository ActivityRepository { get; }
        IBadgeRepository BadgeRepository { get; }
        ISuggestionRepository SuggestionRepository { get; }
        ILeaderboardEntryRepository LeaderboardEntryRepository { get; } // ADD THIS LINE BACK

        void Save();
    }
}