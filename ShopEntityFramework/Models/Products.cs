using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopEntityFramework.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public double? Price { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public Product()
        {
        }

        public Product(int сategoryId, string name, double? price = null)
        {
            CategoryId = сategoryId;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"ID: {Id.ToString().PadLeft(3, '0')};\tName: {Name}";
        }
    }
}