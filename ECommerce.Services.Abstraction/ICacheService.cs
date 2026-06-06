using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Abstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cacheKey);

        Task SetAsync(string cacheKey, object cacheValue, TimeSpan TimeToLive);
    }
}
