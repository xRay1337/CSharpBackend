using System.Collections.Generic;

namespace ShopEntityFrameworkCore.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public Product()
        {
        }

        public Product(string name, double? price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"ID: {Id.ToString().PadLeft(3, '0')};\tName: {Name}";
        }
    }
}