using BankingControlPanel.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace BankingControlPanel.Infrastructure.Services;


public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T> GetOrAddAsync<T>(string cacheKey, Func<Task<T>> factory)
    {
        if (!_memoryCache.TryGetValue(cacheKey, out T cacheEntry))
        {
            cacheEntry = await factory();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10)); // Adjust expiration as needed

            _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
        }
        return cacheEntry;
    }

    public void Remove(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
    }
}