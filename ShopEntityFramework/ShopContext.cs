using ShopEntityFramework.Models;
using System.Data.Entity;

namespace ShopEntityFramework
{
    public class ShopContext : DbContext
    {
        static ShopContext()
        {
            Database.SetInitializer(new ShopContextInitializer());
        }

        public ShopContext() : base("name=ShopModel")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Customer>().Property(c => c.LastName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.MiddleName).HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.PhoneNumber).HasMaxLength(11).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.Email).HasMaxLength(255).IsRequired();

            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Order>().Property(p => p.OrderDate).HasColumnType("datetime2");
            //modelBuilder.Entity<Order>().Property(p => p.OrderDate).HasColumnType("datetimeoffset"); // Этот тип данных почему-то не хочет создавать

            modelBuilder.Entity<Product>()
                .HasMany(p => p.CategoryProducts)
                .WithRequired(cp => cp.Product)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithRequired(o => o.Product)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.CategoryProducts)
                .WithRequired(cp => cp.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithRequired(o => o.Customer)
                .HasForeignKey(p => p.CustomerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}