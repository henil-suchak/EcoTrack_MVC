using System;
using System.ComponentModel.DataAnnotations;

namespace EcoTrack.WebMvc.ViewModels
{
    public class DashboardStatsViewModel
    {
        public int NumberOfActivities { get; set; }
        public decimal TotalCarbonEmitted { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow.Date.AddDays(-7);

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.UtcNow.Date;
    }
}