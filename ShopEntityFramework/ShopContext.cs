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

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Customer>().Property(c => c.LastName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.MiddleName).HasMaxLength(50).IsOptional();
            modelBuilder.Entity<Customer>().Property(c => c.PhoneNumber).HasMaxLength(11).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.Email).HasMaxLength(255).IsRequired();

            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany(c => c.Categories)
                .WithMany(p => p.Products)
                .Map(m =>
                {
                    m.MapLeftKey("ProductId");
                    m.MapRightKey("CategoryId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}