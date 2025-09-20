using AutoMapper;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.ViewModels;

namespace EcoTrack.WebMvc.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // This tells AutoMapper how to convert a ViewModel to a Model.
            // We will need to expand this later.
            CreateMap<LogActivityViewModel, TravelActivity>();
            CreateMap<LogActivityViewModel, FoodActivity>();
            // ... Mappings for other activity types
        }
    }
}