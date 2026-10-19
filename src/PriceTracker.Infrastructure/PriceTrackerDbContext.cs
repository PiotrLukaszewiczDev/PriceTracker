using Microsoft.EntityFrameworkCore;
using PriceTracker.Domain;

namespace PriceTracker.Infrastructure
{
    public class PriceTrackerDbContext : DbContext
    {
        public PriceTrackerDbContext(DbContextOptions<PriceTrackerDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<PriceSnapshot> PriceSnapshots { get; set; }
    }
}
