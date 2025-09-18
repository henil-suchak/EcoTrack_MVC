using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTrack.WebMvc.Entities
{
    public class EmissionFactor
    {
        // This is the Primary Key for the database table.
        public int EmissionFactorId { get; set; }

        // Attributes like [Required] and [StringLength] help define the database schema.
        // For example, StringLength(100) often translates to NVARCHAR(100) in SQL.
        [Required]
        [StringLength(100)]
        public string ActivityType { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string SubType { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Region { get; set; }

        // The [Column] attribute specifies the exact data type in the database to ensure precision.
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Value { get; set; } // kg CO₂/unit

        [Required]
        [StringLength(250)]
        public string SourceReference { get; set; } = string.Empty;
    }
}