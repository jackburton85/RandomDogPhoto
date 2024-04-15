using Microsoft.Extensions.Configuration;
using SPPTest.Shared.Models;

namespace SPPTest.Services.DogApIServices
{
    public class DogApiService : IApiService<DogData, string>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DogApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["AppSettings:DogApiBaseUrl"] ?? String.Empty);
        }
        public async Task<DogData> GetDataAsync(string breed)
        {              
            if (!string.IsNullOrEmpty(breed))
            {
                var names = breed.Split(" ");
                if (names.Length > 1)
                {
                    breed = $"{names[1]}/{names[0]}";
                }
                var dogApiClient = new ApiClient<string, DogData>(_httpClient);

                var dogData = await dogApiClient.GetDataAsync($"{breed.ToLower() + _configuration["AppSettings:DogApiRandomImagePath"]}");
                if (dogData == null)
                {
                    Console.WriteLine($"Data for breed '{breed}' not found in api");
                    return default;
                }

                Console.WriteLine($"Data for breed '{breed}' found in api");
                return dogData;
            }

            throw new InvalidOperationException("Unsupported type T");
        }

    }
}
