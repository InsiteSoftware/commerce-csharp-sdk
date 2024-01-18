using CommerceApiSDK.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace CommerceApiSDK.DemoApp.Services
{
    public class CacheService : ICacheService
    {
        public int OnlineCacheMinutes => 15;

        public int OfflineCacheMinutes => 60;

        private IMemoryCache memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void ClearAllCaches()
        {
            if (this.memoryCache != null)
            {
                this.memoryCache.Dispose();
                this.memoryCache = new MemoryCache(new MemoryCacheOptions());
            }
        }

        public void Shutdown()
        {
            if (this.memoryCache != null)
            {
                this.memoryCache.Dispose();
            }
        }

        public Task<bool> PersistData<T>(string key, T value)
            where T : class
        {
            if (this.memoryCache != null)
            {
                this.memoryCache.Set<T>(key, value);

                return Task.FromResult(false);
            }

            return Task.FromResult(false);
        }

        public Task<bool> PersistData(string key, byte[] value)
        {
            if (this.memoryCache != null)
            {
                this.memoryCache.Set(key, value);

                return Task.FromResult(false);
            }

            return Task.FromResult(false);
        }

        public Task<T> LoadPersistedData<T>(string key)
            where T : class
        {
            return Task.Run(() =>
            {
                return this.memoryCache.Get<T>(key);
            });
        }

        public Task<byte[]> LoadPersistedData(string key)
        {
            return Task.Run(() =>
            {
                return this.memoryCache.Get<byte[]>(key);
            });
        }

        public Task RemovePersistedData(string key)
        {
            return Task.Run(() =>
            {
                this.memoryCache.Remove(key);
            });
        }

        public Task<bool> HasOnlineCache(string key)
        {
            return Task.FromResult(true);
        }

        public async Task<T> GetOrFetchObject<T>(
            string key,
            Func<Task<T>> fetchFunc,
            DateTimeOffset? absoluteExpiration = null
        )
        {
            var cacheEntry = await this.memoryCache.GetOrCreateAsync<T>(
                key,
                async entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromSeconds(15 * 60);
                    return await fetchFunc();
                }
            );

            return cacheEntry;
        }

        public Task Invalidate(string key)
        {
            return Task.Run(() =>
            {
                this.memoryCache.Remove(key);
            });
        }

        public Task InvalidateObjectWithKeysStartingWith<T>(string keyPrefix)
        {
            return Task.Run(() =>
            {
                this.memoryCache.Remove(keyPrefix);
            });
        }

        public Task InvalidateObject<T>(string key)
        {
            return Task.Run(() =>
            {
                this.memoryCache.Remove(key);
            });
        }

        public Task InvalidateAllObjects<T>()
        {
            return Task.Run(() =>
            {
                this.memoryCache.Dispose();
                this.memoryCache = new MemoryCache(new MemoryCacheOptions());
            });
        }

        public Task InsertObject<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            return Task.Run(() =>
            {
                return absoluteExpiration.HasValue
                    ? this.memoryCache.Set<T>(key, value, absoluteExpiration.Value)
                    : this.memoryCache.Set<T>(key, value);
            });
        }

        public Task<T> GetObject<T>(string key)
        {
            return Task.Run(() =>
            {
                return this.memoryCache.Get<T>(key);
            });
        }
    }
}
