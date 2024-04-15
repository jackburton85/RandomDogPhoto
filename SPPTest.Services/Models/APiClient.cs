using Newtonsoft.Json;

namespace SPPTest.Services.DogApIServices
{

    public class ApiClient<TData, TResult> : IApiClient<TData, TResult>
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));       
        }

        public async Task<TResult> GetDataAsync(TData data)
        {
            try
            {
                var response = await _httpClient.GetAsync(data.ToString());
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResult>(content);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return default;
            }
        }
    }
}
