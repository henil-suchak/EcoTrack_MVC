using System;
using System.Threading.Tasks;
using EcoTrack.WebMvc.Interfaces;
using EcoTrack.WebMvc.Models;

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

            string subType = GetSubTypeForActivity(activity);
            var factor = await _unitOfWork.EmissionFactorRepository.GetFactorAsync(
                activity.ActivityType.ToString(), 
                subType);

            if (factor == null)
            {
                throw new InvalidOperationException($"Could not find a valid emission factor for {activity.ActivityType} with subtype {subType}.");
            }
            
            // --- THIS IS THE FIX ---
            // Before calculating, assign the found factor's ID to the activity's foreign key.
            // We use a switch to access the property on the correct derived type.
            switch (activity)
            {
                case TravelActivity ta:
                    ta.EmissionFactorId = factor.EmissionFactorId;
                    break;
                case FoodActivity fa:
                    fa.EmissionFactorId = factor.EmissionFactorId;
                    break;
                case ElectricityActivity ea:
                    ea.EmissionFactorId = factor.EmissionFactorId;
                    break;
                case ApplianceActivity aa:
                    aa.EmissionFactorId = factor.EmissionFactorId;
                    break;
                case WasteActivity wa:
                    wa.EmissionFactorId = factor.EmissionFactorId;
                    break;
            }

            activity.CarbonEmission = CalculateEmission(activity, factor.Value);
            activity.DateTime = DateTime.UtcNow;

            await _unitOfWork.ActivityRepository.AddAsync(activity);
            await _unitOfWork.CompleteAsync();

            return activity;
        }

        // ... GetSubTypeForActivity and CalculateEmission methods are the same ...
        private string GetSubTypeForActivity(Activity activity)
        {
            return activity switch
            {
                TravelActivity ta => ta.Mode,
                FoodActivity fa => fa.FoodType,
                ElectricityActivity => "Grid",
                ApplianceActivity aa => aa.ApplianceType,
                WasteActivity wa => wa.WasteType,
                _ => throw new NotSupportedException("Activity type not supported.")
            };
        }

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