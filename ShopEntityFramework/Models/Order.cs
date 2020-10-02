﻿using System;
using System.Collections.Generic;

namespace ShopEntityFramework.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public DateTime Dt { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }

        public Order(int customerId, int productId, DateTime dt)
        {
            CustomerId = customerId;
            ProductId = productId;
            Dt = dt;
        }
    }
}