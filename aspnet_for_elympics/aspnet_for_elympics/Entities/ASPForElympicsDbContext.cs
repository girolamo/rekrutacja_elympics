using Aspnet_for_elympics.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aspnet_for_elympics.Controllers
{
    public class ASPForElympicsDbContext : DbContext
    {
        public ASPForElympicsDbContext(DbContextOptions<ASPForElympicsDbContext> options) : base(options) { }

        public DbSet<Number> Numbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Number>().Property(p => p.CreationTime).IsRequired();
        }
    }
}
