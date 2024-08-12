using BankingControlPanel.Domain.Models;
using BankingControlPanel.Interfaces.Repositories;
using BankingControlPanel.Interfaces.Services;

namespace BankingControlPanel.Application.Services;

public class SearchCriteriaService : ISearchCriteriaService
{
    private readonly ISearchCriteriaRepository _searchCriteriaRepository;
    private readonly ICacheService _cacheService;

    public SearchCriteriaService(ISearchCriteriaRepository searchCriteriaRepository, ICacheService cacheService)
    {
        _searchCriteriaRepository = searchCriteriaRepository;
        _cacheService = cacheService;
    }

    public async Task SaveSearchCriteriaAsync(string criteria)
    {
        var searchCriteria = new SearchCriteria { Criteria = criteria };
        await _searchCriteriaRepository.AddSearchCriteriaAsync(searchCriteria);
    }

    public async Task<IEnumerable<SearchCriteria>> GetLastSearchCriteriasAsync(int count)
    {
        var cacheKey = $"LastSearchCriterias_{count}";
        return await _cacheService.GetOrAddAsync(cacheKey, async () =>
        {
            return await _searchCriteriaRepository.GetLastSearchCriteriasAsync(count);
        });
    }
}