using AutoMapper;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.ViewModels;

namespace EcoTrack.WebMvc.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Base mapping from the domain model to the base ViewModel
            CreateMap<Activity, ActivityViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : "Unknown"))
                .Include<TravelActivity, TravelActivityViewModel>()
                .Include<FoodActivity, FoodActivityViewModel>()
                .Include<ElectricityActivity, ElectricityActivityViewModel>()
                .Include<ApplianceActivity, ApplianceActivityViewModel>()
                .Include<WasteActivity, WasteActivityViewModel>();

            // Specific mappings for each derived type
            CreateMap<TravelActivity, TravelActivityViewModel>();
            CreateMap<FoodActivity, FoodActivityViewModel>();
            CreateMap<ElectricityActivity, ElectricityActivityViewModel>();
            CreateMap<ApplianceActivity, ApplianceActivityViewModel>();
            CreateMap<WasteActivity, WasteActivityViewModel>();
        }
    }
}