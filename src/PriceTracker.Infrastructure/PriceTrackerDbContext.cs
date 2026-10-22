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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PriceSnapshot>(entity =>
            {
                entity.Property(s => s.Price).HasPrecision(18, 2);
                entity.Property(s => s.CrossedOutPrice).HasPrecision(18, 2);
                entity.Property(s => s.DeclaredLowestPrice).HasPrecision(18, 2);
                entity.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey(s => s.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name).HasMaxLength(300);
                entity.Property(p => p.Url).HasMaxLength(850);
            });
        }
    }
}
