using AutoMapper;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.ViewModels;

namespace EcoTrack.WebMvc.Services // Or whatever your namespace is
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- User Mappings ---
            CreateMap<User, UserProfileViewModel>();
            CreateMap<UserRegistrationViewModel, User>();

            // --- Activity Mappings ---
            CreateMap<Activity, ActivityViewModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => GetActivityDescription(src)));

            // --- Suggestion Mapping ---
            CreateMap<Suggestion, SuggestionViewModel>()
                .ForMember(dest => dest.SavingAmountDisplay, opt => opt.MapFrom(src => $"{src.SavingAmount:F1} kg CO₂"))
                .ForMember(dest => dest.DateIssuedDisplay, opt => opt.MapFrom(src => src.DateTimeIssued.ToShortDateString()));

            // --- Badge Mapping ---
            CreateMap<Badge, BadgeViewModel>()
                .ForMember(dest => dest.DateEarnedDisplay, opt => opt.MapFrom(src => src.DateEarned.ToString("MMMM dd, yyyy")));
        }

        // Helper method to create a user-friendly description for activities
        private static string GetActivityDescription(Activity activity)
        {
            return activity switch
            {
                TravelActivity travel => $"{travel.Mode} trip for {travel.Distance:F1} km",
                FoodActivity food => $"{food.Quantity:F1} kg of {food.FoodType}",
                ElectricityActivity elec => $"{elec.Consumption:F1} kWh of electricity used",
                _ => "An activity was logged."
            };
        }
    }
}