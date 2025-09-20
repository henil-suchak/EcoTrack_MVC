using System;
using Microsoft.EntityFrameworkCore;
using EcoTrack.WebMvc.Models;
using EcoTrack.WebMvc.Enums;

namespace EcoTrack.WebMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // --- DbSets ---
        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }
        public DbSet<EmissionFactor> EmissionFactors { get; set; }
        public DbSet<Activity> Activities { get; set; }

        // --- Model Configuration ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- USER RELATIONSHIPS ---
            modelBuilder.Entity<User>()
                .HasMany(u => u.Activities)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            // Add other user relationships...

            // --- FAMILY RELATIONSHIP ---
            modelBuilder.Entity<Family>()
                .HasMany(f => f.Members)
                .WithOne(u => u.Family)
                .HasForeignKey(u => u.FamilyId);

            // --- ACTIVITY INHERITANCE CONFIGURATION (TPH) ---
            modelBuilder.Entity<Activity>()
                .HasDiscriminator<ActivityType>("ActivityType")
                .HasValue<TravelActivity>(ActivityType.Travel)
                .HasValue<FoodActivity>(ActivityType.Food)
                .HasValue<ElectricityActivity>(ActivityType.Electricity)
                .HasValue<ApplianceActivity>(ActivityType.Appliance)
                .HasValue<WasteActivity>(ActivityType.Waste);

            // --- ACTIVITY SUBTYPE TO EMISSIONFACTOR RELATIONSHIPS ---
            modelBuilder.Entity<TravelActivity>()
                .HasOne(ta => ta.EmissionFactor)
                .WithMany()
                .HasForeignKey(ta => ta.EmissionFactorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FoodActivity>()
                .HasOne(fa => fa.EmissionFactor)
                .WithMany()
                .HasForeignKey(fa => fa.EmissionFactorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ... other activity to emission factor relationships

            // --- DATA SEEDING FOR EMISSION FACTORS ---
            modelBuilder.Entity<EmissionFactor>().HasData(
                new EmissionFactor
                {
                    EmissionFactorId = Guid.Parse("00000000-0000-0000-0000-000000000001"), // Fixed Guid
                    ActivityType = "Travel",
                    SubType = "Car",
                    Value = 0.21m, // Example value: kg CO2 per km
                    SourceReference = "Default"
                },
                new EmissionFactor
                {
                    EmissionFactorId = Guid.Parse("00000000-0000-0000-0000-000000000002"), // Fixed Guid
                    ActivityType = "Food",
                    SubType = "Beef",
                    Value = 27.0m, // Example value: kg CO2 per kg
                    SourceReference = "Default"
                },
                new EmissionFactor
                {
                    EmissionFactorId = Guid.Parse("00000000-0000-0000-0000-000000000003"), // Fixed Guid
                    ActivityType = "Food",
                    SubType = "Chicken",
                    Value = 6.9m, // Example value: kg CO2 per kg
                    SourceReference = "Default"
                }
            );
        } // <-- The OnModelCreating method ends here
    }
}