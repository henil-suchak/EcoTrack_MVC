using System;
using System.Collections.Generic;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        // FIX: Changed return type to T? to match the implementation.
        T? GetById(Guid id); 
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}