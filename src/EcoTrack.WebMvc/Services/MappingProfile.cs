using AutoMapper;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.ViewModels;
using EcoTrack.WebMvc.DTO;
namespace EcoTrack.WebMvc.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Base mapping from the domain model to the base ViewModel.
            // .IncludeAllDerived() automatically finds and applies the specific mappings below.
            CreateMap<Activity, ActivityViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : "Unknown"))
                .IncludeAllDerived();

            CreateMap<User, UserEditViewModel>();
            CreateMap<User, UserProfileViewModel>();
            CreateMap<DashboardDto, DashboardViewModel>();

            // Specific mappings for each derived type
            CreateMap<TravelActivity, TravelActivityViewModel>();
            CreateMap<FoodActivity, FoodActivityViewModel>();
            CreateMap<ElectricityActivity, ElectricityActivityViewModel>();
            CreateMap<ApplianceActivity, ApplianceActivityViewModel>();
            CreateMap<WasteActivity, WasteActivityViewModel>();

            // --- You will add other mappings here, like for Suggestions ---
            CreateMap<Suggestion, SuggestionViewModel>(); // Example from previous discussion

            // Add this new mapping for your DTO to your ViewModel
            CreateMap<DashboardDto, DashboardViewModel>()
                .ForMember(dest => dest.TotalCarbonEmitted, opt => opt.MapFrom(src =>
                    src.UserProfile != null ? src.UserProfile.Activities.Sum(a => a.CarbonEmission) : 0));
        }
    }
}