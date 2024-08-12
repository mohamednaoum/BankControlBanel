using BankingControlPanel.Domain.Models;
using BankingControlPanel.Infrastructure.Data;
using BankingControlPanel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Infrastructure.UnitTests.RepositoryUnitTests;

public class SearchCriteriaRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly SearchCriteriaRepository _repository;

    public SearchCriteriaRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);
        _repository = new SearchCriteriaRepository(_context);
    }

    [Fact]
    public async Task AddSearchCriteriaAsync_ShouldAddSearchCriteriaToDatabase()
    {
        // Arrange
        var searchCriteria = new SearchCriteria { Criteria = "Test Criteria" };

        // Act
        await _repository.AddSearchCriteriaAsync(searchCriteria);

        // Assert
        Assert.Equal(1, _context.SearchCriterias.Count());
        Assert.Equal(searchCriteria.Criteria, _context.SearchCriterias.Single().Criteria);
    }

    [Fact]
    public async Task GetLastSearchCriteriasAsync_ShouldReturnSearchCriterias()
    {
        // Arrange
        _context.SearchCriterias.Add(new SearchCriteria { Criteria = "Criteria 1" });
        _context.SearchCriterias.Add(new SearchCriteria { Criteria = "Criteria 2" });
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetLastSearchCriteriasAsync(2);

        // Assert
        Assert.Equal(2, result.Count());
    }
}