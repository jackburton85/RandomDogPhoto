using SPPTest.Domain.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DogPhotoConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
           
            Console.WriteLine("Enter a breed name (e.g., Tibetan Mastiff) and press Enter to view the photo URL.");
            Console.WriteLine("Press Escape (Esc) to quit.");

            while (true)
            {
                Console.Write("Enter breed name: ");
                var userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Please enter a valid breed name.");
                    continue;
                }

                if (userInput.Equals("Escape", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                }

                try
                {
                    using var httpClient = new HttpClient();
                    var response = await httpClient.GetAsync($"https://localhost:7218/api/DogApi/breed/{userInput}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var dogPhoto = Newtonsoft.Json.JsonConvert.DeserializeObject<DogPhoto>(content);
                        Console.WriteLine($"Breed: {dogPhoto.Breed}");
                        Console.WriteLine($"Photo URL: {dogPhoto.PhotoUrl}");
                    }
                    else
                    {
                        Console.WriteLine($"Error fetching data for breed '{userInput}': {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
   
}
