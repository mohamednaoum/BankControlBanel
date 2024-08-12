using BankingControlPanel.Domain.Models;
using BankingControlPanel.Infrastructure.Data;
using BankingControlPanel.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Infrastructure.Repositories;

public class SearchCriteriaRepository : ISearchCriteriaRepository
{
    private readonly ApplicationDbContext _context;

    public SearchCriteriaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddSearchCriteriaAsync(SearchCriteria searchCriteria)
    {
        await _context.SearchCriterias.AddAsync(searchCriteria);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SearchCriteria>> GetLastSearchCriteriasAsync(int count)
    {
        return await _context.SearchCriterias
            .OrderByDescending(sc => sc.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}