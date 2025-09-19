using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoTrack.WebMvc.Interfaces
{

    // This is the new flexible base interface.
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        // CHANGED: Returns Task<IEnumerable<TEntity>> for async operation and flexibility.
        Task<IEnumerable<TEntity>> GetAllAsync();

        // CHANGED: Returns Task<TEntity?> and uses the generic TKey.
        Task<TEntity?> GetByIdAsync(TKey id);

        // CHANGED: Renamed and changed to be async.
        Task AddAsync(TEntity entity);

        // CHANGED: This is now just a marker, the actual save is in the Unit of Work.
        void Update(TEntity entity);

        // CHANGED: This is now just a marker.
        void Delete(TEntity entity);
    }
}