public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IActivityRepository ActivityRepository { get; }
    void Save();
}