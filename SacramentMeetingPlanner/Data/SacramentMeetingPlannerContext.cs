using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Data
{
    public class SacramentMeetingPlannerContext : DbContext
    {
        public SacramentMeetingPlannerContext(DbContextOptions<SacramentMeetingPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<Planner> Planner { get; set; } = default!;
        public DbSet<SpeakerTopic> SpeakerTopic { get; set; } = default!; // Include SpeakerTopic DbSet

        // Override OnModelCreating to define relationships and additional configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpeakerTopic>()
                .HasOne<Planner>()                      // Define relationship: SpeakerTopic belongs to Planner
                .WithMany(p => p.SpeakersTopics)        // Planner has many SpeakerTopics
                .HasForeignKey(st => st.PlannerId);     // Foreign key is PlannerId

            base.OnModelCreating(modelBuilder);         // Call base method
        }
    }
}
