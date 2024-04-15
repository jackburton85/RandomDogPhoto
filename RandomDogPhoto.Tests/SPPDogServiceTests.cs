using Moq;
using SPPTest.Domain.Models;
using SPPTest.Repository;
using SPPTest.Services.Models;

public class SPPDogServiceTests
{
    private readonly Mock<IRepository<DogPhoto>> _mockRepository;
    private readonly SPPDogService _sppDogService;

    public SPPDogServiceTests()
    {
        _mockRepository = new Mock<IRepository<DogPhoto>>();
        _sppDogService = new SPPDogService(_mockRepository.Object);
    }

    [Fact]
    public async Task AddDataAsync_ThrowsArgumentNullException_WhenDataIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sppDogService.AddDataAsync(null));
    }

    [Fact]
    public async Task AddDataAsync_AddsData_WhenDataIsValid()
    {
        var dogPhoto = new DogPhoto() { Breed = "hound", PhotoUrl = "TestPhoto.png" };
        await _sppDogService.AddDataAsync(dogPhoto);
        _mockRepository.Verify(r => r.AddAsync(dogPhoto), Times.Once);
    }
    [Fact]
    public async Task AddDataAsync_AddsData_WhenBreedHasInvalidCharacters()
    {
        var dogPhoto = new DogPhoto() { Breed = "ho*badDAtaund", PhotoUrl = "TestPhoto.png" };
        await Assert.ThrowsAsync<ArgumentException>(() =>  _sppDogService.AddDataAsync(dogPhoto));      
    }


    [Fact]
    public async Task GetDataAsync_ThrowsArgumentNullException_WhenDataIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sppDogService.GetDataAsync(null));
    }

    [Fact]
    public async Task GetDataAsync_ReturnsData_WhenDataIsValid()
    {
        var breed = "hound";
        var dogPhoto = new DogPhoto() { Breed = breed, PhotoUrl = "TestPhoto.png" };
        _mockRepository.Setup(r => r.GetByAsync(x => x.Breed == breed)).ReturnsAsync(dogPhoto);

        var result = await _sppDogService.GetDataAsync(breed);

        Assert.Equal(dogPhoto, result);
    }
}