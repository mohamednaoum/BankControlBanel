using BankingControlPanel.Domain.Models;
using BankingControlPanel.Interfaces.Repositories;
using BankingControlPanel.Interfaces.Services;

namespace BankingControlPanel.Application.Services;

public class SearchCriteriaService(ISearchCriteriaRepository searchCriteriaRepository, ICacheService cacheService)
    : ISearchCriteriaService
{
    public async Task SaveSearchCriteriaAsync(string criteria, string userId)
    {
        var searchCriteria = new SearchCriteria { Criteria = criteria, UserId = userId };
        await searchCriteriaRepository.AddSearchCriteriaAsync(searchCriteria);
    }

    public async Task<IEnumerable<SearchCriteria>> GetLastSearchCriteriasAsync(int count, string userId)
    {
        var cacheKey = $"LastSearchCriterias_{userId}";

        var cachedCriteria = await cacheService.GetFromCacheAsync<IEnumerable<SearchCriteria>>(cacheKey);
    
        if (cachedCriteria != null)
        {
            return cachedCriteria;
        }

        var lastSearchCriterias = await searchCriteriaRepository.GetLastSearchCriteriasAsync(count, userId);

        cacheService.Set(cacheKey, lastSearchCriterias);

        return lastSearchCriterias;
    }

}