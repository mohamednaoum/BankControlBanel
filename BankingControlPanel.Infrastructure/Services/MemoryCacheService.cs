using BankingControlPanel.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace BankingControlPanel.Infrastructure.Services;


public class MemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    public async Task<T> GetFromCacheAsync<T>(string cacheKey)
    {
        if (memoryCache.TryGetValue(cacheKey, out T cacheEntry))
        {
            return await Task.FromResult(cacheEntry);
        }
        
        // Return default value if not found
        return await Task.FromResult(default(T));
    }

    public void Set<T>(string cacheKey, T item)
    {
        memoryCache.Set(cacheKey, item, new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(10)
        });
    }
}