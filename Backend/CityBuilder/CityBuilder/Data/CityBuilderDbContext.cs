using CityBuilder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityBuilder.Data
{
    public class CityBuilderDbContext : DbContext
    {
        public CityBuilderDbContext(DbContextOptions<CityBuilderDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Road> Roads { get; set; }

        public DbSet<CityRoadNetwork> CityRoadNetworks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>().ToTable("City");
            builder.Entity<Road>().ToTable("Road");
            builder.Entity<CityRoadNetwork>().ToTable("CityRoadNetwork");

            base.OnModelCreating(builder);
        }
    }
}
