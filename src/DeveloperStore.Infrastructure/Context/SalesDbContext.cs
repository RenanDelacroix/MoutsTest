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
            entity.ToTable("sales");

            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).HasColumnName("id");
            entity.Property(s => s.Number).HasColumnName("number").IsRequired();
            entity.Property(s => s.CustomerId).HasColumnName("customerid");
            entity.Property(s => s.BranchId).HasColumnName("branchid");
            entity.Property(s => s.CreatedAt).HasColumnName("createdat");
            entity.Property(s => s.Status).HasColumnName("status").HasConversion<string>();
            entity.Property(s => s.Discount).HasColumnName("discount").HasColumnType("decimal(18,2)");
            entity.Ignore(s => s.Total);
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.ToTable("saleitem");

            entity.HasKey(i => i.Id);
            entity.Property(i => i.Id).HasColumnName("id");
            entity.Property(i => i.ProductId).HasColumnName("productid");
            entity.Property(i => i.SaleId).HasColumnName("saleid");
            entity.Property(i => i.Quantity).HasColumnName("quantity");
            entity.Property(i => i.UnitPrice).HasColumnName("unitprice").HasColumnType("decimal(18,2)");
            entity.Property(i => i.Discount).HasColumnName("discount").HasColumnType("decimal(18,2)");

            entity.HasOne<Sale>()
                  .WithMany(s => s.Items)
                  .HasForeignKey(i => i.SaleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
