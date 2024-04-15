using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using SPPTest.Services.DogApIServices;
using SPPTest.Shared.Models;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

public class DogApiServiceTests
{
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly DogApiService _dogApiService;
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;

    public DogApiServiceTests()
    {
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        _mockConfiguration = new Mock<IConfiguration>();
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        _mockConfiguration.Setup(x => x["AppSettings:DogApiBaseUrl"]).Returns("https://dog.ceo/api/breed/");

        _dogApiService = new DogApiService(_mockHttpClientFactory.Object, _mockConfiguration.Object);
    }

    [Fact]
    public async Task GetDataAsync_ThrowsInvalidOperationException_WhenBreedIsEmpty()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() => _dogApiService.GetDataAsync(string.Empty));
    }

    [Fact]
    public async Task GetDataAsync_ReturnsDogData_WhenBreedIsValid()
    {
        // Arrange
        var breed = "hound";
        var dogData = new DogData { Message = "Test message", Status = "success" };

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(dogData), Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act
        var result = await _dogApiService.GetDataAsync(breed);

        // Assert
        Assert.Equal(dogData, result);
    }
}