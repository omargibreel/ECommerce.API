using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.DTOs.BasketDTOs
{
    public class BasketDTO
    {
        public string Id { get; set; } = null!;
        public ICollection<BasketItemDTO> Items { get; set; } = [];
    }
}
