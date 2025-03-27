﻿using Microsoft.EntityFrameworkCore;
using OnlineShopPlattfrom.WebAPI.Data.Entities;

namespace OnlineShopPlattfrom.WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property<decimal>(p => p.Price)
            .HasPrecision(18, 2);
    }
}
