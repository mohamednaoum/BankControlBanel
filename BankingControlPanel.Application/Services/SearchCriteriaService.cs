using BankingControlPanel.Domain.Models;
using BankingControlPanel.Interfaces.Repositories;
using BankingControlPanel.Interfaces.Services;

namespace BankingControlPanel.Application.Services;

public class SearchCriteriaService(ISearchCriteriaRepository searchCriteriaRepository, ICacheService cacheService)
    : ISearchCriteriaService
{
    public async Task SaveSearchCriteriaAsync(string criteria, string userId)
    {
        var searchCriteria = new SearchCriteria { Criteria = criteria, UserId = userId};
        await searchCriteriaRepository.AddSearchCriteriaAsync(searchCriteria);
    }

    public async Task<IEnumerable<SearchCriteria>> GetLastSearchCriteriasAsync(int count, string userId)
    {
        var cacheKey = $"LastSearchCriterias_{userId}";
        return await cacheService.GetOrAddAsync(cacheKey, async () => await searchCriteriaRepository.GetLastSearchCriteriasAsync(count, userId));
    }
}