using System.Collections.Generic;

namespace ShopEntityFramework.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();

        public Category(string name)
        {
            Name = name;
        }
    }
}