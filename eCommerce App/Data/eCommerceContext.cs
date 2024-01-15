using eCommerce_App.Models;
using Microsoft.EntityFrameworkCore;

public class ECommerceContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }

    public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure ProductTypes table
        modelBuilder.Entity<ProductType>()
            .Property(pt => pt.Id)
            .ValueGeneratedNever(); // Identity insert is turned off

        // Configure ProductBrands table
        modelBuilder.Entity<ProductBrand>()
            .Property(pb => pb.Id)
            .ValueGeneratedNever(); // Identity insert is turned off

        // Configure relationships
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductType)
            .WithMany()
            .HasForeignKey(p => p.ProductTypeId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductBrand)
            .WithMany()
            .HasForeignKey(p => p.ProductBrandId);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)"); // Adjust the precision and scale based on your requirements
    }
}
