using ECommerce.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMins;

        private static readonly string[] NonCacheableParams =
        {
            "search"
        };

        public RedisCacheAttribute(int durationInMins = 5)
        {
            _durationInMins = durationInMins;
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            //Get CacheService from DI Container
            //Can not inject ICacheService in constructor because Attributes are instantiated at compile time and metadata level, before your Dependency Injection (DI) container starts

            // check if the request contains any non-cacheable query parameters, if it does, skip caching and execute the endpoint directly
            if (context.HttpContext.Request.Query.Keys.Any(
     key => NonCacheableParams.Contains(
         key,
         StringComparer.OrdinalIgnoreCase)))
            {
                await next();
                return;
            }

            var cacheService =
                context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            //Create CacheKey Based On RequestPath & QueryParams
            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            //Check if Data Exists in Cache
            var cacheValue = await cacheService.GetAsync(cacheKey);

            //If Exists,Return Cached Data and skip executing endpoint
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };

                return;
            }
            //If Not exists ,Execute endpoint and store result in Cache if response from endpoint is 200 Ok

            var executedContext = await next.Invoke(); // this is a instance of the current executing endpoint after execution, it contains the result of the endpoint execution

            if (executedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(
                    cacheKey,
                    result.Value!,
                    TimeSpan.FromMinutes(_durationInMins)
                );
            }
        }

        // api/Products
        // api/Products?brandId=2
        // api/Products?typeId=1
        // api/Products?brandId=2&typeId=1
        // api/Products?typeId=1 &brandId=2
        private string CreateCacheKey(HttpRequest request)
        {

            //StringBuilder key = new StringBuilder();

            //key.Append(request.Path); // api/Products|brandId-2|typeId-1

            //foreach (var item in request.Query.OrderBy(X => X.Key))
            //{
            //    key.Append($"|{item.Key}-{item.Value}");
            //}

            //return key.ToString();

            var query = request.Query
            .OrderBy(x => x.Key)
            .Select(x => $"{x.Key}={x.Value}");

            var queryString = string.Join("&", query);

            return $"{request.Path}?{queryString}";
        }
    }
}
