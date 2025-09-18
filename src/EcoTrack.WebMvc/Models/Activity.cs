using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcoTrack.WebMvc.Enums; // Assuming your enums are moved here

namespace EcoTrack.WebMvc.Models
{
    public abstract class Activity
    {
        [Key] // Explicitly marks this as the primary key
        public Guid ActivityId { get; set; }

        public DateTime DateTime { get; set; }
        
        public ActivityType ActivityType { get; set; }

        [Column(TypeName = "decimal(18, 6)")] // Defines the database column type for precision
        public decimal CarbonEmission { get; set; }

        // Foreign Key to User
        public Guid UserId { get; set; }
        
        // Navigation property to the User entity
        public User? User { get; set; }
    }
}