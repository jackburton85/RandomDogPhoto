using SPPTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPTest.Repository
{
    public interface IRepository<T>
    {      
        Task AddAsync(T entity);
        Task<DogPhoto?> GetByBreedAsync(string breed);
    }
}
