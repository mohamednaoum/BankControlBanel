using BankingControlPanel.Domain.Models;

namespace BankingControlPanel.Interfaces.Repositories;

public interface ISearchCriteriaRepository
{
    Task AddSearchCriteriaAsync(SearchCriteria searchCriteria);
    Task<IEnumerable<SearchCriteria>> GetLastSearchCriteriasAsync(int count, string userId);
}