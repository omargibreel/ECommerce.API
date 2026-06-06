using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Models.CartModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } = null!;
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
