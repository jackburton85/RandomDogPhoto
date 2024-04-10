using SPPTest.Domain.Models;
using SPPTest.Shared.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SPPTest.Services.Models
{
    public class CacheService : IApiService
    {
        public async Task<T> GetDataAsync<T, TData>(TData data) where TData : class
        {
            var dogPhoto = await CacheHelper<DogPhoto>.GetCachedDataAsync(data as string);
            return (T)(object)dogPhoto;
        }

        public async Task<T> AddDataAsync<T, TData>(string key, DogPhoto data) where TData : class
        {
            await CacheHelper<DogPhoto>.AddDataAsync(key, data);
            return (T)(object)data;
        }
    }
}
