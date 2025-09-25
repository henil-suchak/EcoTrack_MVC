using System;
using EcoTrack.WebMvc.Enums;

namespace EcoTrack.WebMvc.DTO
{
    public class LogActivityDto
    {
        public Guid UserId { get; set; }
        public ActivityType ActivityType { get; set; }

        // Travel
        public string? TravelMode { get; set; }
        public decimal Distance { get; set; }

        // Food
        public string? FoodType { get; set; }
        public decimal Quantity { get; set; }

        // Electricity
        public decimal ElectricityConsumption { get; set; }
        
        // Appliance
        public string? ApplianceType { get; set; }
        public decimal UsageTime { get; set; }
        public decimal PowerRating { get; set; }
        
        // Waste
        public string? WasteType { get; set; }
        public decimal Amount { get; set; }
    }
}