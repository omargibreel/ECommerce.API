using ECommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Persistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database; // IDatabase for Make Instance of Redis Database to perform cache operations like Get and Set.

        public CacheRepository(IConnectionMultiplexer connection) // IConnectionMultiplexer for catch objects Connected to Redis server and get database instance from it to perform cache operations.
        {
            _database = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string cacheKey)
        {
            var cacheValue = await _database.StringGetAsync(cacheKey);

            return cacheValue.IsNullOrEmpty ? null : cacheValue.ToString();
        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive)
        {
            await _database.StringSetAsync(cacheKey, cacheValue, timeToLive);
        }
    }
}
