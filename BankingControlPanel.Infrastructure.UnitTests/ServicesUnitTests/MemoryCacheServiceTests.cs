using BankingControlPanel.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace BankingControlPanel.Infrastructure.UnitTests.ServicesUnitTests;

public class MemoryCacheServiceTests
{
    private readonly MemoryCacheService _cacheService;
    private readonly Mock<IMemoryCache> _mockMemoryCache;

    public MemoryCacheServiceTests()
    {
        _mockMemoryCache = new Mock<IMemoryCache>();
        _cacheService = new MemoryCacheService(_mockMemoryCache.Object);
    }

    [Fact]
    public async Task GetOrAddAsync_ShouldReturnCachedValue()
    {
        // Arrange
        var cacheKey = "testKey";
        object expectedValue = "cachedValue";
        _mockMemoryCache.Setup(x => x.TryGetValue(cacheKey, out expectedValue)).Returns(true);

        // Act
        var result = await _cacheService.GetOrAddAsync(cacheKey, () => Task.FromResult("newValue"));

        // Assert
        Assert.Equal(expectedValue, result);
    }

    [Fact]
    public async Task GetOrAddAsync_ShouldAddValueToCache()
    {
        // Arrange
        var cacheKey = "testKey";
        var newValue = "newValue";
        object cachedValue = null; // Local variable for the out parameter

        _mockMemoryCache.Setup(x => x.TryGetValue(cacheKey, out cachedValue)).Returns(false);
    
        // Setup for CreateEntry to return a mock of ICacheEntry
        var mockCacheEntry = new Mock<ICacheEntry>();
        _mockMemoryCache.Setup(x => x.CreateEntry(cacheKey)).Returns(mockCacheEntry.Object);

        // Act
        var result = await _cacheService.GetOrAddAsync(cacheKey, () => Task.FromResult(newValue));

        // Assert
        Assert.Equal(newValue, result);
    }

    [Fact]
    public void Remove_ShouldRemoveItemFromCache()
    {
        // Arrange
        var cacheKey = "testKey";

        // Act
        _cacheService.Remove(cacheKey);

        // Assert
        _mockMemoryCache.Verify(x => x.Remove(cacheKey), Times.Once);
    }
}