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

            CreateMap<User, UserProfileViewModel>();
            CreateMap<User, UserEditViewModel>();


            CreateMap<DashboardDto, DashboardViewModel>()
                .ForMember(dest => dest.TotalCarbonEmitted, opt => opt.MapFrom(src => 
                    src.UserProfile != null ? src.UserProfile.Activities.Sum(a => a.CarbonEmission) : 0));


            CreateMap<Badge, BadgeViewModel>()
                .ForMember(dest => dest.DateEarnedDisplay, opt => opt.MapFrom(src => src.DateEarned.ToString("MMMM dd, yyyy")));


            CreateMap<Suggestion, SuggestionViewModel>();


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