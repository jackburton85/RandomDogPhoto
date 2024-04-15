using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPPTest.Domain.Models;
using SPPTest.EFDataAccess;
using SPPTest.Services;
using SPPTest.Services.DogApIServices;
using SPPTest.Services.Models;
using SPPTest.Shared.Models;
using SPPTest.Shared.Utilities;
using System.Formats.Asn1;

namespace SPPTest.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogApiController : ControllerBase
    {
        private readonly SPPDogService _sppDogService;
        private readonly DogApiService _dogApiService;
        private readonly CacheService<DogPhoto, Cache<DogPhoto>> _cacheService;

        public DogApiController(SPPDogService sppDogService, DogApiService dogApiService, CacheService<DogPhoto, Cache<DogPhoto>> cacheService)
        {
            _dogApiService = dogApiService;
            _sppDogService = sppDogService;
            _cacheService = cacheService;
        }

        [HttpGet("breed/{breed}")]
        public async Task<IActionResult> GetDog(string breed)
        {
            if (string.IsNullOrEmpty(breed) || !ValidationHelper.IsValidBreedFormat(breed))
            {
                return BadRequest("This api method will only accept valid breed names");
            }
            DogPhoto dogPhoto = await _cacheService.GetDataAsync(breed);

            if (dogPhoto == null)
            {
                dogPhoto = await _sppDogService.GetDataAsync(breed);

                if (dogPhoto == null)
                {
                    DogData dogData = await _dogApiService.GetDataAsync(breed);
                    if (dogData == null)
                    {
                        return NotFound();
                    }
                    dogPhoto = new DogPhoto
                    {
                        Breed = breed,
                        PhotoUrl = dogData.Message
                    };
                    await _sppDogService.AddDataAsync(dogPhoto);
                }
                Cache<DogPhoto> dogPhotoCache = new Cache<DogPhoto>
                {
                    Key = dogPhoto.Breed,
                    Value = dogPhoto
                };

                await _cacheService.AddDataAsync(dogPhotoCache);
            }


            return Ok(dogPhoto);
        }
    }


}
