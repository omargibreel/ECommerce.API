using ECommerce.Domain.Models.CartModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan ttl=default); // TTL : Time To Live
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
