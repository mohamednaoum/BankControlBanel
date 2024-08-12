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

    public async Task<IEnumerable<SearchCriteria>> GetLastSearchCriteriasAsync(int count, string userId)
    {
        return await _context.SearchCriterias.Where(x=>x.UserId== userId)
            .OrderByDescending(sc => sc.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}