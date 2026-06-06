using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Models.CartModule
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
