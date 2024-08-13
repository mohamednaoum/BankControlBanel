using System;
using System.Threading.Tasks;
using BankingControlPanel.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace BankingControlPanel.Application.UnitTests
{
    public class MemoryCacheServiceTests
    {
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly MemoryCacheService _cacheService;
        private readonly Mock<ICacheEntry> _mockCacheEntry;

        public MemoryCacheServiceTests()
        {
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockCacheEntry = new Mock<ICacheEntry>();
            _cacheService = new MemoryCacheService(_mockMemoryCache.Object);
        }

        [Fact]
        public async Task GetFromCacheAsync_ShouldReturnCachedItem_WhenItemExists()
        {
            // Arrange
            var cacheKey = "testKey";
            var cachedItem = "cachedValue";
            object retrievedValue = cachedItem; // Cast to object to match the out parameter type

            _mockMemoryCache
                .Setup(mc => mc.TryGetValue(cacheKey, out retrievedValue))
                .Returns(true);

            // Act
            var result = await _cacheService.GetFromCacheAsync<string>(cacheKey);

            // Assert
            Assert.Equal(cachedItem, result);
        }

        [Fact]
        public async Task GetFromCacheAsync_ShouldReturnDefault_WhenItemDoesNotExist()
        {
            // Arrange
            var cacheKey = "testKey";
            object retrievedValue = null;

            _mockMemoryCache
                .Setup(mc => mc.TryGetValue(cacheKey, out retrievedValue))
                .Returns(false);

            // Act
            var result = await _cacheService.GetFromCacheAsync<string>(cacheKey);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Set_ShouldAddItemToCache()
        {
            // Arrange
            var cacheKey = "testKey";
            var itemToCache = "cacheValue";
            
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cacheService = new MemoryCacheService(memoryCache);

            // Act
            cacheService.Set(cacheKey, itemToCache);

            // Assert
            // Retrieve item from cache
            var cachedItem = memoryCache.Get<string>(cacheKey);

            // Assert that the item is correctly cached
            Assert.NotNull(cachedItem);
            Assert.Equal(itemToCache, cachedItem);
        }




    }
}
