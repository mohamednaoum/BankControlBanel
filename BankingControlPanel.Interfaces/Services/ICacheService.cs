namespace BankingControlPanel.Interfaces.Services;

public interface ICacheService
{
    Task<T> GetFromCacheAsync<T>(string cacheKey);
    void Set<T>(string cacheKey, T item);
}