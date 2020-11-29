using System;
using System.Collections.Generic;

namespace ShopEntityFramework.Models
{
    public class CategoryProduct
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual Category Category { get; set; }

        public CategoryProduct(Category category, Product product)
        {
            CreateDate = DateTime.Now;
            CategoryId = category.Id;
            ProductId = product.Id;
        }
    }
}