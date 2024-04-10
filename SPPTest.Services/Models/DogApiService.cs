using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SPPTest.Domain.Models;
using SPPTest.EFDataAccess;
using SPPTest.Shared.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace SPPTest.Services.DogApIServices
{
    public class DogApiService : IApiService
    {
        public async Task<T> GetDataAsync<T, TData>(TData key) where TData : class
        {
            var breed = key as string;
            if (!string.IsNullOrEmpty(breed))
            {
                var names = breed.Split(" ");
                if (names.Length > 1)
                {
                    breed = $"{names[1]}/{names[0]}";
                }
                var dogApiClient = new DogApiClient(new HttpClient());
                var dogData = await dogApiClient.GetRandomPhotoForBreedAsync(breed);
                if (dogData == null)
                {
                    Console.WriteLine($"Data for breed '{breed}' not found in api");
                    return default;
                }

                Console.WriteLine($"Data for breed '{breed}' found in api");
                return (T)(object)dogData;
            }
       
            throw new InvalidOperationException("Unsupported type T");
        }
                 
    }
}
