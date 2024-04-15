using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SPPTest.Domain.Models;
using SPPTest.Shared.Utilities;

namespace SPPTest.Services.Models
{
    public class CacheService<T,TData> : IApiService<T, string>, IApiAddDataService<T,TData>
    {
        
        public async Task AddDataAsync(TData data)
        {
            Console.WriteLine();
            if (data is null)
            {
                throw new ArgumentNullException("Data is null. Null values are not allowed to be stored in cache");
            }
            if (data is not Cache<T>)
            {
                throw new ArgumentException("Invalid data Type: " + typeof(T).Name);
            }

            await CacheHelper<T>.AddDataAsync(data as Cache<T>);
        }
        

        public async Task<T> GetDataAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("Key is null or empty. Key is required to get data from cache.");
            }
            var result = await CacheHelper<T>.GetCachedDataAsync(key);
            return result;
        }
    }
}
