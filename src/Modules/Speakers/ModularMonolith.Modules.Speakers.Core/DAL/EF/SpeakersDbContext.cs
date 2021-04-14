using Microsoft.EntityFrameworkCore;
using ModularMonolith.Modules.Speakers.Core.Entities;

namespace ModularMonolith.Modules.Speakers.Core.DAL.EF
{
    public class SpeakersDbContext : DbContext
    {
        public DbSet<Speaker> Speakers { get; set; }

        public SpeakersDbContext(DbContextOptions<SpeakersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("speakers");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}