using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;

namespace ProductApi.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.ProductId);
            entity.Property(p => p.ProductId).ValueGeneratedOnAdd();
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Status).IsRequired();
            entity.Property(p => p.Stock).IsRequired();
            entity.Property(p => p.Description).HasMaxLength(300);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
        });
    }
}
