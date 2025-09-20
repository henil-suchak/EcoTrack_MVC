using System;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.Enums; // Ensure you have this using for ActivityType

namespace EcoTrack.WebMvc.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Activity> LogActivityAsync(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            // 1. Get the correct 'subType' for the factor lookup (e.g., "Car", "Beef").
            string subType = GetSubTypeForActivity(activity);

            // 2. Use the repository to find the emission factor.
            var factor = await _unitOfWork.EmissionFactorRepository.GetFactorAsync(
                activity.ActivityType.ToString(), 
                subType);

            if (factor == null)
            {
                throw new InvalidOperationException($"Could not find a valid emission factor for {activity.ActivityType} with subtype {subType}.");
            }

            // 3. Perform the calculation based on the specific activity type.
            activity.CarbonEmission = CalculateEmission(activity, factor.Value);
            activity.DateTime = DateTime.UtcNow;

            // 4. Use the repository to save the final object.
            await _unitOfWork.ActivityRepository.AddAsync(activity);
            await _unitOfWork.CompleteAsync();

            return activity;
        }

        // Helper method to get the specific subtype from an activity
        private string GetSubTypeForActivity(Activity activity)
        {
            return activity switch
            {
                TravelActivity ta => ta.Mode,
                FoodActivity fa => fa.FoodType,
                ElectricityActivity => "Grid", // Or based on a region property
                ApplianceActivity aa => aa.ApplianceType,
                WasteActivity wa => wa.WasteType,
                _ => throw new NotSupportedException("Activity type not supported.")
            };
        }

        // Helper method to perform the correct calculation
        private decimal CalculateEmission(Activity activity, decimal factorValue)
        {
            return activity switch
            {
                TravelActivity travel => travel.Distance * factorValue,
                FoodActivity food => food.Quantity * factorValue,
                ElectricityActivity electricity => electricity.Consumption * factorValue,
                ApplianceActivity appliance => (appliance.PowerRating / 1000) * appliance.UsageTime * factorValue,
                WasteActivity waste => waste.Amount * factorValue,
                _ => throw new NotSupportedException("Activity type not supported.")
            };
        }
    }
}