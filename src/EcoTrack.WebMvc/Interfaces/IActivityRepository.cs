using System;
using System.Collections.Generic;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Interfaces
{
    public interface IActivityRepository
    {
        List<Activity> GetAll();
        Activity GetById(Guid id);
        List<Activity> GetByUserId(Guid userId);
        void Insert(Activity activity);
        void Update(Activity activity);
        void Delete(Activity activity);
    }
}