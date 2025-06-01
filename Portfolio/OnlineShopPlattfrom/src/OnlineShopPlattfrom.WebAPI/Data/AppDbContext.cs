using Microsoft.EntityFrameworkCore;

using OnlineShopPlattfrom.WebAPI.Data.Entities;

namespace OnlineShopPlattfrom.WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<MultimediaProduct> MultimediaProducts { get; set; }
    
    public DbSet<WearableProduct> WearableProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MultimediaProduct>()
            .Property<decimal>(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<WearableProduct>()
            .Property<decimal>(p => p.Price)
            .HasPrecision(18, 2);
    }
}
