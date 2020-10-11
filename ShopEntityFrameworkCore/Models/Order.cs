using System;

namespace ShopEntityFrameworkCore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public DateTime DateTime { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }

        public Order(int customerId, int productId, DateTime dateTime)
        {
            CustomerId = customerId;
            ProductId = productId;
            DateTime = dateTime;
        }
    }
}