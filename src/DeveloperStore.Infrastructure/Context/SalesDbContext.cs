using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infrastructure.Context;

public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

    public DbSet<Sale> Sales => Set<Sale>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Number).IsRequired();
            entity.Property(s => s.Discount).HasColumnType("decimal(18,2)");
            entity.Property(s => s.Total).HasColumnType("decimal(18,2)");
            entity.Property(s => s.Status).HasConversion<string>();
            entity.HasMany(s => s.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasKey(nameof(SaleItem.ProductId));
            entity.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
            entity.Property(i => i.Discount).HasColumnType("decimal(18,2)");
        });
    }
}
