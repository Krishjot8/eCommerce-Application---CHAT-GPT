using eCommerce_App.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_App.Data
{
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

}