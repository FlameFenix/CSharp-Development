using Cars_Market.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarDetails> CarDetails { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Seller> Sellers { get; set; }
    }
}