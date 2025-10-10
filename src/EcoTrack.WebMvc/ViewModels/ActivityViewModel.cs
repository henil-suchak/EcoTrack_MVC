using System;

namespace EcoTrack.WebMvc.ViewModels
{
    // The abstract base ViewModel
    public abstract class ActivityViewModel
    {
        public Guid ActivityId { get; set; }
        public string ActivityType { get; set; } = string.Empty;
        public decimal CarbonEmission { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; } = string.Empty;


        public abstract string GetDescription();
    }


    public class TravelActivityViewModel : ActivityViewModel
    {
        public string Mode { get; set; } = string.Empty;
        public decimal Distance { get; set; }
        public override string GetDescription() => $"Traveled {Distance:F2} km by {Mode}.";
    }

    public class FoodActivityViewModel : ActivityViewModel
    {
        public string FoodType { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public override string GetDescription() => $"Ate {Quantity:F2} kg of {FoodType}.";
    }

    public class ElectricityActivityViewModel : ActivityViewModel
    {
        public decimal Consumption { get; set; }
        public override string GetDescription() => $"Used {Consumption:F2} kWh of electricity.";
    }

    public class ApplianceActivityViewModel : ActivityViewModel
    {
        public string ApplianceType { get; set; } = string.Empty;
        public decimal UsageTime { get; set; }
        public override string GetDescription() => $"Used {ApplianceType} for {UsageTime:F2} hours.";
    }
    
    public class WasteActivityViewModel : ActivityViewModel
    {
        public string WasteType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public override string GetDescription() => $"Produced {Amount:F2} kg of {WasteType} waste.";
    }
}