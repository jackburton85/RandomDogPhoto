using Microsoft.EntityFrameworkCore;
using SPPTest.Domain.Models;
using SPPTest.EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPTest.Repository
{
    public class DogPhotoRepository : IRepository<DogPhoto>
    {
        private readonly DogDbContext _dbContext;

        public DogPhotoRepository(DogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        Task IRepository<DogPhoto>.AddAsync(DogPhoto entity)
        {
            _dbContext.DogPhotos.Add(entity);
            return _dbContext.SaveChangesAsync();
        }
        public async Task<DogPhoto?> GetByBreedAsync(string breed)
        {
            var dogPhoto = await _dbContext.DogPhotos.Where(x => x.Breed.ToLower() == breed.ToLower()).FirstOrDefaultAsync();
            return dogPhoto;
        }
    }
       
}
