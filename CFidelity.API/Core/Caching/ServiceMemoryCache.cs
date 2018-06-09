using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace CFidelity.API.Core.Caching
{
    public class ServiceMemoryCache : IServiceMemoryCache
    {
        private IMemoryCache _memoryCache;

        public ServiceMemoryCache()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public void Add(string key, object objectCache)
        {
            Add(key, CacheLifetime.Moderate, objectCache);
        }

        public void Add(string key, CacheLifetime lifeTime, object objectCache)
        {
            _memoryCache.Set(key, objectCache,
                new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(Convert.ToDouble(lifeTime))));
        }

        public bool Contains(string key)
        {
            object cached;

            if (_memoryCache.TryGetValue(key, out cached))
                return true;

            return false;
        }

        public object Find(string key)
        {
            return _memoryCache.Get(key);
        }

        public T Find<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public T Find<T>(string key, Func<T> findFunction)
        {
            return Find<T>(key, CacheLifetime.Moderate, findFunction);
        }

        public async Task<T> Find<T>(string key, Func<Task<T>> findFunction)
        {
            return await Find<T>(key, CacheLifetime.Moderate, findFunction);
        }

        public T Find<T>(string key, CacheLifetime lifeTime, Func<T> findFunction)
        {
            if (Contains(key))
            {
                return Find<T>(key);
            }
            else
            {

                var result = findFunction();

                if (result != null)
                    Add(key, lifeTime, result);

                return result;
            }
        }

        public async Task<T> Find<T>(string key, CacheLifetime lifeTime, Func<Task<T>> findFunction)
        {
            if (Contains(key))
            {
                return Find<T>(key);
            }
            else
            {

                var result = findFunction();

                if (result != null)
                    Add(key, lifeTime, result);

                return await result;
            }
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}