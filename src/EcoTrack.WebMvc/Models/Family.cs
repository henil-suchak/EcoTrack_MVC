using System.ComponentModel.DataAnnotations;

namespace EcoTrack.WebMvc.Entities
{
    public class Family
    {
        [Key]
        public Guid FamilyId { get; set; }

        [Required]
        [StringLength(100)]
        public string FamilyName { get; set; } = string.Empty;

        // Navigation property for users in this family
        public ICollection<User> Members { get; set; } = new List<User>();

        // Optional field for shared resource tracking
        public string? SharedConsumption { get; set; } // e.g., JSON data
    }
}