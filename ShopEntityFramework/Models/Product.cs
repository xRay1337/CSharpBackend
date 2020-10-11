using System.Collections.Generic;

namespace ShopEntityFramework.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public Product()
        {
        }

        public Product(string name, double? price, Category[] categories)
        {
            Name = name;
            Price = price;
            Categories = categories;
        }

        public override string ToString()
        {
            return $"ID: {Id.ToString().PadLeft(3, '0')};\tName: {Name}";
        }
    }
}