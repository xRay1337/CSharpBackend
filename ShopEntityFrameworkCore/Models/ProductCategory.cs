using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace ShopEntityFrameworkCore.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        ProductCategory()
        {
        }

        public ProductCategory(EntityEntry<Product> product, EntityEntry<Category> category)
        {
            ProductId = product.Entity.Id;
            Product = product.Entity;
            CategoryId = category.Entity.Id;
            Category = category.Entity;
        }
    }
}