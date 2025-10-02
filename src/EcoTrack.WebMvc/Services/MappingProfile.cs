using AutoMapper;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.ViewModels;
using EcoTrack.WebMvc.DTO;
using System.Linq;

namespace EcoTrack.WebMvc.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- User & Dashboard Mappings ---
            CreateMap<User, UserProfileViewModel>();
            CreateMap<User, UserEditViewModel>();

            // Merged and corrected mapping for the dashboard
            CreateMap<DashboardDto, DashboardViewModel>()
                .ForMember(dest => dest.TotalCarbonEmitted, opt => opt.MapFrom(src => 
                    src.UserProfile != null ? src.UserProfile.Activities.Sum(a => a.CarbonEmission) : 0));

            // --- Badge Mapping ---
            CreateMap<Badge, BadgeViewModel>()
                .ForMember(dest => dest.DateEarnedDisplay, opt => opt.MapFrom(src => src.DateEarned.ToString("MMMM dd, yyyy")));

            // --- Suggestion Mapping ---
            CreateMap<Suggestion, SuggestionViewModel>();

            // --- Activity Inheritance Mapping ---
            CreateMap<Activity, ActivityViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : "Unknown"))
                .IncludeAllDerived();

            CreateMap<TravelActivity, TravelActivityViewModel>();
            CreateMap<FoodActivity, FoodActivityViewModel>();
            CreateMap<ElectricityActivity, ElectricityActivityViewModel>();
            CreateMap<ApplianceActivity, ApplianceActivityViewModel>();
            CreateMap<WasteActivity, WasteActivityViewModel>();
        }
    }
}