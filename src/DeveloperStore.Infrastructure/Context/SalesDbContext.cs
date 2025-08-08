using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace DeveloperStore.Infrastructure.Context;

public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Branch> Branches => Set<Branch>();

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
            entity.Ignore(s => s.BranchName);

            entity.HasOne(s => s.Branch)
                  .WithMany()
                  .HasForeignKey(s => s.BranchId)
                  .OnDelete(DeleteBehavior.Cascade);
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

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Name).HasColumnName("name").IsRequired();
            entity.Property(p => p.Price).HasColumnName("price").HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("branches");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Name).HasColumnName("name").IsRequired();
        });
    }
}
