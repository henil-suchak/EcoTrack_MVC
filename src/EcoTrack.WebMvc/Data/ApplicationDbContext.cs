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

            modelBuilder.Entity<User>()
                .HasMany(u => u.Badges)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Suggestions)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);
            
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

            modelBuilder.Entity<ElectricityActivity>()
                .HasOne(ea => ea.EmissionFactor)
                .WithMany()
                .HasForeignKey(ea => ea.EmissionFactorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplianceActivity>()
                .HasOne(aa => aa.EmissionFactor)
                .WithMany()
                .HasForeignKey(aa => aa.EmissionFactorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WasteActivity>()
                .HasOne(wa => wa.EmissionFactor)
                .WithMany()
                .HasForeignKey(wa => wa.EmissionFactorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}