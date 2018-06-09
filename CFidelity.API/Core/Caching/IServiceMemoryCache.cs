using System;
using System.Threading.Tasks;

namespace CFidelity.API.Core.Caching
{
    public interface IServiceMemoryCache
    {
        void Add(string key, object objectCache);

        void Add(string key, CacheLifetime lifeTime, object objectCache);

        void Remove(string key);

        object Find(string key);

        T Find<T>(string key);

        T Find<T>(string key, Func<T> findFunction);

        Task<T> Find<T>(string key, Func<Task<T>> findFunction);

        T Find<T>(string key, CacheLifetime lifeTime, Func<T> findFunction);

        Task<T> Find<T>(string key, CacheLifetime lifeTime, Func<Task<T>> findFunction);

        bool Contains(string key);
    }
}
