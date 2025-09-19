using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;

namespace EcoTrack.WebMvc.Repositories
{
    // UPDATED: The class is now generic for both the entity and its key.
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        // UPDATED: Made async and returns IEnumerable for better flexibility.
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // UPDATED: Made async and uses the generic TKey.
        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        // UPDATED: Made async.
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // This is synchronous because it only changes the entity's state in memory.
        // The actual database save is handled by the Unit of Work.
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        // This is also synchronous for the same reason as Update.
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}