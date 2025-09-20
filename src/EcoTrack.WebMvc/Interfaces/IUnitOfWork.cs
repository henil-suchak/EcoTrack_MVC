namespace EcoTrack.WebMvc.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IActivityRepository ActivityRepository { get; }
        IBadgeRepository BadgeRepository { get; }
        ISuggestionRepository SuggestionRepository { get; }
        ILeaderboardEntryRepository LeaderboardEntryRepository { get; }

        IEmissionFactorRepository EmissionFactorRepository { get; }
        IFamilyRepository FamilyRepository { get; }

        Task<int> CompleteAsync();
        // void Save();
    }
}