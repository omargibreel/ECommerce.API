using ECommerce.Domain.Contracts;
using ECommerce.Domain.Models.CartModule;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ECommerce.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connection) // Injected Redis connection multiplexer for accessing the distributed cache store.
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan ttl = default)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, (ttl == default) ? TimeSpan.FromDays(7) : ttl);
            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string basketId) => await _database.KeyDeleteAsync(basketId);
        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            if (basket.IsNullOrEmpty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<CustomerBasket>(basket.ToString()!);
        }
    }
}