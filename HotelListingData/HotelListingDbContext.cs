using HotelListingAPIData.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPIData
{
    public class HotelListingDbContext : IdentityDbContext<User>
    {
        public HotelListingDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new CountriesConfiguration());
            modelBuilder.ApplyConfiguration(new HotelsConfiguration());
        }
    }
}
