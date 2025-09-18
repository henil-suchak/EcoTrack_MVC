using Microsoft.EntityFrameworkCore;
using EcoTrack.WebMvc.Models;

namespace EcoTrack.WebMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add a DbSet for each entity you want a table for in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }
        public DbSet<EmissionFactor> EmissionFactors { get; set; }
        
        // Activity-related DbSets
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ApplianceActivity> ApplianceActivities { get; set; }
        public DbSet<ElectricityActivity> ElectricityActivities { get; set; }
        public DbSet<FoodActivity> FoodActivities { get; set; }
        public DbSet<TravelActivity> TravelActivities { get; set; }
        public DbSet<WasteActivity> WasteActivities { get; set; }
    }
}