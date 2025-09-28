using System;
using System.Threading.Tasks;
using EcoTrack.WebMvc.DTO;
using EcoTrack.WebMvc.Enums;
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

        public async Task<Activity> LogActivityAsync(LogActivityDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            // Create the specific activity object based on the DTO
            Activity newActivity;
            switch (dto.ActivityType)
            {
                case ActivityType.Travel:
                    newActivity = new TravelActivity { Mode = dto.TravelMode!, Distance = dto.Distance };
                    break;
                case ActivityType.Food:
                    newActivity = new FoodActivity { FoodType = dto.FoodType!, Quantity = dto.Quantity };
                    break;
                case ActivityType.Electricity:
                    newActivity = new ElectricityActivity { Consumption = dto.ElectricityConsumption, SourceType = "Bill" };
                    break;
                case ActivityType.Appliance:
                    newActivity = new ApplianceActivity { ApplianceType = dto.ApplianceType!, UsageTime = dto.UsageTime, PowerRating = dto.PowerRating };
                    break;
                case ActivityType.Waste:
                    newActivity = new WasteActivity { WasteType = dto.WasteType!, Amount = dto.Amount };
                    break;
                default:
                    throw new NotSupportedException("Selected activity type is not supported yet.");
            }

            newActivity.UserId = dto.UserId;
            newActivity.ActivityType = dto.ActivityType;
            
            string subType = GetSubTypeForActivity(newActivity).ToLower();
            var factor = await _unitOfWork.EmissionFactorRepository.GetFactorAsync(
                newActivity.ActivityType.ToString(), 
                subType);

            if (factor == null)
            {
                throw new InvalidOperationException($"Could not find a valid emission factor for {newActivity.ActivityType} with subtype {subType}.");
            }

            // Use a switch statement to safely assign the EmissionFactorId
            switch (newActivity)
            {
                case TravelActivity ta: ta.EmissionFactorId = factor.EmissionFactorId; break;
                case FoodActivity fa: fa.EmissionFactorId = factor.EmissionFactorId; break;
                case ElectricityActivity ea: ea.EmissionFactorId = factor.EmissionFactorId; break;
                case ApplianceActivity aa: aa.EmissionFactorId = factor.EmissionFactorId; break;
                case WasteActivity wa: wa.EmissionFactorId = factor.EmissionFactorId; break;
            }

            newActivity.CarbonEmission = CalculateEmission(newActivity, factor.Value);
            newActivity.DateTime = DateTime.UtcNow;

            await _unitOfWork.ActivityRepository.AddAsync(newActivity);
            await _unitOfWork.CompleteAsync();
            
            return newActivity;
        }

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