using System.Collections.Generic;

namespace ShopEntityFrameworkCore.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public Category()
        {
        }

        public Category(string name)
        {
            Name = name;
        }
    }
}