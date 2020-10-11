using Microsoft.EntityFrameworkCore;
using ShopEntityFrameworkCore.Models;

namespace ShopEntityFrameworkCore
{
    public class ShopContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=Zlobin\SQLEXPRESS;Initial Catalog=ZlobinShopEFCore;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Customer>().Property(c => c.LastName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.MiddleName).HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.PhoneNumber).HasMaxLength(11).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.Email).HasMaxLength(255).IsRequired();

            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<ProductCategory>(b =>
            {
                b.HasOne(pc => pc.Product)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(pc => pc.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(pc => pc.Category)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(pc => pc.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}