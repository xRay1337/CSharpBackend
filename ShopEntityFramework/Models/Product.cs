using System.Collections.Generic;

namespace ShopEntityFramework.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

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