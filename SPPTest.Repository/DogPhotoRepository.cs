using Microsoft.EntityFrameworkCore;
using SPPTest.Domain.Models;
using SPPTest.EFDataAccess;
using System.Linq.Expressions;

namespace SPPTest.Repository
{
    public class DogPhotoRepository : IRepository<DogPhoto>
    {
        private readonly DogDbContext _dbContext;

        public DogPhotoRepository(DogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(DogPhoto entity)
        {
            await _dbContext.DogPhotos.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<DogPhoto?> GetByAsync(Expression<Func<DogPhoto, bool>> predicate)
        {
            return await _dbContext.Set<DogPhoto>().FirstOrDefaultAsync(predicate);
        }
    }

}
