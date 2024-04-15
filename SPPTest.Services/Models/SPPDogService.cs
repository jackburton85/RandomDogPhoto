using SPPTest.Domain.Models;
using SPPTest.Repository;
using SPPTest.Shared.Utilities;

namespace SPPTest.Services.Models
{
    public class SPPDogService : IApiService<DogPhoto, string>, IApiAddDataService<DogPhoto, DogPhoto>
    {
        private readonly IRepository<DogPhoto> _dogPhotoRepository;

        public SPPDogService(IRepository<DogPhoto> dogPhotoRepository)
        {
            _dogPhotoRepository = dogPhotoRepository;
        }

        public async Task AddDataAsync(DogPhoto dogPhoto)
        {
            if (dogPhoto is null)
            {
                throw new ArgumentNullException("Data is null. Null values are not allowed");
            }            
            if (!ValidationHelper.IsValidBreedFormat(dogPhoto.Breed))
            {
                throw new ArgumentException("Invalid data: Breed contains invalid data: " + dogPhoto.Breed);
            }
            await _dogPhotoRepository.AddAsync(dogPhoto);
            Console.WriteLine($"Data for breed '{dogPhoto}' added to db");
        }

        public async Task<DogPhoto> GetDataAsync(string breed)
        {
            if (breed is null)
            {
                throw new ArgumentNullException("Data is null. Null values are not allowed");
            }
            if (!ValidationHelper.IsValidBreedFormat(breed))
            {
                throw new ArgumentException("Invalid data: Breed contains invalid data: " + breed);
            }
          
            var dog = await _dogPhotoRepository.GetByAsync(x => x.Breed == breed);
            if (dog == null)
            {
                Console.WriteLine($"Data for breed '{breed}' not found in db");
                return default;
            }
            Console.WriteLine($"Data for breed '{breed}' found in db");

            return dog;
        }

    }
}
