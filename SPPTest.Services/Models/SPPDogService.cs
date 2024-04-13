using SPPTest.Domain.Models;
using SPPTest.Repository;

namespace SPPTest.Services.Models
{
    public class SPPDogService : IApiService
    {
        private readonly IRepository<DogPhoto> _dogPhotoRepository;

        public SPPDogService(IRepository<DogPhoto> dogPhotoRepository)
        {
            _dogPhotoRepository = dogPhotoRepository;
        }

        public async Task<T> AddDataAsync<T, TData>(string photoUrl, string breed, int age) where TData : class
        {
            var dog = new DogPhoto
            {
                Breed = breed,
                PhotoUrl = photoUrl
            };

            await _dogPhotoRepository.AddAsync(dog);
           
            Console.WriteLine($"Data for breed '{breed}' added to db");
            return (T)(object)dog;
        }

        public async Task<T> GetDataAsync<T, TData>(TData data) where TData : class
        {
            var breed = data as string;
            if (string.IsNullOrEmpty(breed))
            {
                return default;
            }
            var dog = await _dogPhotoRepository.GetByAsync(x => x.Breed == breed);
            if (dog == null)
            {
                Console.WriteLine($"Data for breed '{breed}' not found in db");
                return default;
            }
            Console.WriteLine($"Data for breed '{breed}' found in db");

            return (T)(object)dog;
        }

    }
}
