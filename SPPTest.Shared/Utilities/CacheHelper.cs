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

        public static async Task AddDataAsync(string key, T data)
        {
            
            _cache[key] = data;          
            Console.WriteLine($"Data added to cache with key '{key}'.");
        }
    }
}
