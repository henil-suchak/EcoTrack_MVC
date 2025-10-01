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
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Badges)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Suggestions)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            
            // --- ACTIVITY INHERITANCE CONFIGURATION (TPH) ---
            modelBuilder.Entity<Activity>()
                .HasDiscriminator<ActivityType>("ActivityType")
                .HasValue<TravelActivity>(ActivityType.Travel)
                .HasValue<FoodActivity>(ActivityType.Food)
                .HasValue<ElectricityActivity>(ActivityType.Electricity)
                .HasValue<ApplianceActivity>(ActivityType.Appliance)
                .HasValue<WasteActivity>(ActivityType.Waste);

            // --- ACTIVITY SUBTYPE TO EMISSIONFACTOR RELATIONSHIPS ---
            modelBuilder.Entity<TravelActivity>().HasOne(ta => ta.EmissionFactor).WithMany().HasForeignKey(ta => ta.EmissionFactorId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FoodActivity>().HasOne(fa => fa.EmissionFactor).WithMany().HasForeignKey(fa => fa.EmissionFactorId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ElectricityActivity>().HasOne(ea => ea.EmissionFactor).WithMany().HasForeignKey(ea => ea.EmissionFactorId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ApplianceActivity>().HasOne(aa => aa.EmissionFactor).WithMany().HasForeignKey(aa => aa.EmissionFactorId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WasteActivity>().HasOne(wa => wa.EmissionFactor).WithMany().HasForeignKey(wa => wa.EmissionFactorId).OnDelete(DeleteBehavior.Restrict);

            // --- DATA SEEDING FOR EMISSION FACTORS ---
            modelBuilder.Entity<EmissionFactor>().HasData(
                new EmissionFactor { EmissionFactorId = Guid.Parse("0a0a0a0a-0000-0000-0000-000000000001"), ActivityType = "Travel", SubType = "Car", Value = 0.21m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0a0a0a0a-0000-0000-0000-000000000002"), ActivityType = "Travel", SubType = "Bus", Value = 0.10m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0a0a0a0a-0000-0000-0000-000000000003"), ActivityType = "Travel", SubType = "Train", Value = 0.04m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0b0b0b0b-0000-0000-0000-000000000001"), ActivityType = "Food", SubType = "Beef", Value = 27.0m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0b0b0b0b-0000-0000-0000-000000000002"), ActivityType = "Food", SubType = "Chicken", Value = 6.9m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0b0b0b0b-0000-0000-0000-000000000003"), ActivityType = "Food", SubType = "Vegetables", Value = 2.0m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0c0c0c0c-0000-0000-0000-000000000001"), ActivityType = "Electricity", SubType = "Grid", Value = 0.45m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0d0d0d0d-0000-0000-0000-000000000001"), ActivityType = "Appliance", SubType = "Fridge", Value = 0.45m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0d0d0d0d-0000-0000-0000-000000000002"), ActivityType = "Appliance", SubType = "AC", Value = 0.45m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0e0e0e0e-0000-0000-0000-000000000001"), ActivityType = "Waste", SubType = "Landfill", Value = 0.60m, SourceReference = "Default" },
                new EmissionFactor { EmissionFactorId = Guid.Parse("0e0e0e0e-0000-0000-0000-000000000002"), ActivityType = "Waste", SubType = "Recyclable", Value = 0.10m, SourceReference = "Default" }
            );
        }
    }
}