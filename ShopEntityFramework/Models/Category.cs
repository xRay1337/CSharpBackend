using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopEntityFramework.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Category(string name)
        {
            Name = name;
        }
    }
}