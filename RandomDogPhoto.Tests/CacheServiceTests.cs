using Xunit;
using Moq;
using SPPTest.Services.Models;
using System.Threading.Tasks;
using SPPTest.Domain.Models;

public class CacheServiceTests
{
    private readonly CacheService<DogPhoto, Cache<DogPhoto>> _cacheService;
    private readonly CacheService<string, Cache<string>> _stringCacheService;
    public CacheServiceTests()
    {
        _cacheService = new CacheService<DogPhoto, Cache<DogPhoto>>();
        _stringCacheService = new CacheService<string, Cache<string>>();
    }

    [Fact]
    public async Task AddDataAsync_ThrowsArgumentNullException_WhenDataIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _cacheService.AddDataAsync(null));
    }
 
    [Fact]
    public async Task AddDataAsync_DoesNotThrow_WhenDataIsCache_String()
    {
        var value = "Store in Cache";
        var key = "cacheTest";
        var cache = new Cache<string>() { Key = key, Value = value };
        await _stringCacheService.AddDataAsync(cache);
    }

    [Fact]
    public async Task AddDataAsync_DoesNotThrow_WhenDataIsCache_DogData()
    {
        var dogPhoto = new DogPhoto() { Breed = "hound", PhotoUrl="TestPhoto.png" };
        var cache = new Cache<DogPhoto>() { Key= "breed", Value= dogPhoto };
        await _cacheService.AddDataAsync(cache);
    }

    [Fact]
    public async Task GetDataAsync_ReturnsNull_WhenDataIsNull()
    {   
        await Assert.ThrowsAsync<ArgumentNullException>(() => _cacheService.GetDataAsync(null));
    }

        [Fact]
    public async Task GetDataAsync_ReturnsExpectedResult_WhenDataIsNotNull_String()

    {
        var value = "Store in Cache";
        var key = "cacheTest";
        var cache = new Cache<string>() { Key = key, Value = value };
        await _stringCacheService.AddDataAsync(cache);
        var result = await _stringCacheService.GetDataAsync(key);
        Assert.Equal(value, result);
    }

    [Fact]
    public async Task GetDataAsync_ReturnsExpectedResult_WhenDataIsNotNull_DogData()

    {

        var dogPhoto = new DogPhoto() { Breed = "hound", PhotoUrl = "TestPhoto.png" };
        var cache = new Cache<DogPhoto>() { Key = "breed", Value = dogPhoto };
        await _cacheService.AddDataAsync(cache);

        // Assuming CacheHelper.GetCachedDataAsync returns the same string when called with a string
       
        var result = await _cacheService.GetDataAsync(cache.Key);
        Assert.Equal(dogPhoto, result);
    }
}