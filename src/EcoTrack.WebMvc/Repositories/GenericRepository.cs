using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EcoTrack.WebMvc.Data;
using EcoTrack.WebMvc.Interfaces;

namespace EcoTrack.WebMvc.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // FIX #1: Added the "virtual" keyword to allow overriding.
        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        // FIX #2: Changed return type to T? to handle cases where the item is not found.
        public virtual T? GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity) => _dbSet.Add(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
    }
}