using ECommerce.Domain.Contracts;
using ECommerce.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerce.Services.Implementation
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<string?> GetAsync(string cacheKey)
        {
            return await _cacheRepository.GetAsync(cacheKey);
        }

        public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(
                cacheValue,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            );

            await _cacheRepository.SetAsync(cacheKey, value, TimeToLive);
        }
    }
}
