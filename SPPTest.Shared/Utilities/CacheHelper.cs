using SPPTest.Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SPPTest.Shared.Utilities
{
    public static class CacheHelper<T>
    {
        private static readonly ConcurrentDictionary<string, T> _cache = new ConcurrentDictionary<string, T>();

        public static async Task<T?> GetCachedDataAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("Key is null or empty. Key is required to get data from cache.");
            }
            if (_cache.TryGetValue(key, out var cachedData))
            {
                Console.WriteLine($"Data for key '{key}' found in cache.");
            }
            else
            {
                Console.WriteLine($"Data for key '{key}' not found in cache.");
            }

            return cachedData;
        }

        public static async Task AddDataAsync(Cache<T> cache)
        {
            _cache[cache.Key] = cache.Value;
            Console.WriteLine($"Data added to cache with key '{cache.Key}'.");
        }
    }
}
