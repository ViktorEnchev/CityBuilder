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

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User() { Id = 1,Username = "Demo", Password = "Demo"}
            );

            base.OnModelCreating(builder);
        }
    }
}
