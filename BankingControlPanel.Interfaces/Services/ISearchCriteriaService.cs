using BankingControlPanel.Domain.Models;

namespace BankingControlPanel.Interfaces.Services;

public interface ISearchCriteriaService
{
    Task SaveSearchCriteriaAsync(string criteria);
    Task<IEnumerable<SearchCriteria>> GetLastSearchCriteriasAsync(int count);
}