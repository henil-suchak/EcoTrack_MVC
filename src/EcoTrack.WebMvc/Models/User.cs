using System.ComponentModel.DataAnnotations;

namespace EcoTrack.WebMvc.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 120)]
        public int Age { get; set; }
        
        [Required]
        [StringLength(150)]
        [EmailAddress] // Helps ensure the string is a valid email format
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(150)]
        public string Location { get; set; } = string.Empty;

        public string? LifestylePreferences { get; set; }
        public string? ConnectedAccounts { get; set; }

        // Foreign Key for Family
        public Guid? FamilyId { get; set; } // CORRECTED to Guid?
        
        // Navigation property to Family
        public Family? Family { get; set; }

        // Navigation properties for related data
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<Suggestion> Suggestions { get; set; } = new List<Suggestion>();
        public ICollection<Badge> Badges { get; set; } = new List<Badge>();
    }
}