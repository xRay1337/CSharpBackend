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

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Category>()
        //    //    .HasMany(c => c.Products)
        //    //    .WithMany(p => p.Categories)
        //    //    .Map(m =>
        //    //    {
        //    //        m.MapLeftKey("CategoryId");
        //    //        m.MapRightKey("ProductId");
        //    //    });

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}