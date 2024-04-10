using Newtonsoft.Json;
using SPPTest.Services.Models;
using SPPTest.Shared.Models;

namespace SPPTest.Services.DogApIServices
{

    public class DogApiClient
    {
        private readonly HttpClient _httpClient;

        public DogApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://dog.ceo/api/");
        }

        public async Task<DogData> GetRandomPhotoForBreedAsync(string breed)
        {
            try
            {
                var response = await _httpClient.GetAsync($"breed/{breed.ToLower()}/images/random");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var dogData = JsonConvert.DeserializeObject<DogData>(content);
                return dogData;
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Error fetching dog data: {ex.Message}");
                return null;
            }
        }
    }
}
