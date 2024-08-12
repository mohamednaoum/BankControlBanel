namespace BankingControlPanel.Interfaces.Services;

public interface ICacheService
{
    Task<T> GetOrAddAsync<T>(string cacheKey, Func<Task<T>> factory);
    void Remove(string cacheKey);   
}