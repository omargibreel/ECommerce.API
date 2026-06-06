using ECommerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Abstraction
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string BasketId);
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO CreateOrUpdateBasket);
        Task<bool> DeleteBasketAsync(string BasketId);

    }
}
