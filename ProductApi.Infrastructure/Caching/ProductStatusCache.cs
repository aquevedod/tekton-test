using Microsoft.Extensions.Caching.Memory;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Constants;

namespace ProductApi.Infrastructure.Caching;

public class ProductStatusCache : IProductStatusCache
{
    private readonly IMemoryCache _cache;
    private const string CacheKey = "ProductStatusDictionary";

    public ProductStatusCache(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Dictionary<int, string> GetStatusDictionary()
    {
        if (_cache.TryGetValue(CacheKey, out Dictionary<int, string>? cachedStatuses))
        {
            return cachedStatuses!;
        }
        var statusMap = ProductStatus.StatusMap;

        _cache.Set(CacheKey, statusMap, TimeSpan.FromMinutes(5));
        return statusMap;
    }

    public string GetStatusName(int status)
    {
        var map = GetStatusDictionary();
        return map.TryGetValue(status, out var name) ? name : "Unknown";
    }
}
