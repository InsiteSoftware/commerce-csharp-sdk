using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Akavache.Sqlite3;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Services
{
    public class CacheService : ICacheService
    {
        /// <summary>
        /// number of minutes to cache data while online.
        /// </summary>
        public static readonly int OnlineCacheMinutes = 15;

        /// <summary>
        /// number of minutes to cache data while offline.
        /// </summary>
        public static readonly int OfflineCacheMinutes = 3 * 24 * 60;

        private Lazy<IBlobCache> onlineCache;
        public IBlobCache OnlineCache => onlineCache.Value;

        private Lazy<IBlobCache> offlineCache;
        public IBlobCache OfflineCache => offlineCache.Value;

        private Lazy<IBlobCache> localStorage;
        public IBlobCache LocalStorage => localStorage.Value;

        private readonly IFilesystemProvider filesystemProvider;
        private readonly ILoggerService loggerService;

        public CacheService(IFilesystemProvider filesystemProvider, ILoggerService loggerService)
        {
            this.filesystemProvider = filesystemProvider;
            this.loggerService = loggerService;

            offlineCache = new Lazy<IBlobCache>(() => NewLocalBlobCache(CommerceAPIConstants.OfflineCacheDatabaseName));
            onlineCache = new Lazy<IBlobCache>(NewInMemoryBlobCache);
            localStorage = new Lazy<IBlobCache>(() => NewLocalBlobCache(CommerceAPIConstants.LocalStorageDatabaseName));
        }

        public void Shutdown()
        {
            // If the cache has not been accessed then no need to flush it
            if (offlineCache.IsValueCreated)
            {
                offlineCache.Value.Dispose();
                offlineCache.Value.Shutdown.Wait();
                offlineCache = new Lazy<IBlobCache>(() => NewLocalBlobCache(CommerceAPIConstants.OfflineCacheDatabaseName));
            }

            if (localStorage.IsValueCreated)
            {
                localStorage.Value.Dispose();
                localStorage.Value.Shutdown.Wait();
                localStorage = new Lazy<IBlobCache>(() => NewLocalBlobCache(CommerceAPIConstants.LocalStorageDatabaseName));
            }

            if (onlineCache.IsValueCreated)
            {
                onlineCache.Value.Dispose();
                onlineCache.Value.Shutdown.Wait();
                onlineCache = new Lazy<IBlobCache>(NewInMemoryBlobCache);
            }
        }

        public async Task<bool> PersistData<T>(string key, T value) where T : class
        {
            try
            {
                IEnumerable<string> keys = await LocalStorage.GetAllKeys();
                if (keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    await LocalStorage.Invalidate(key);
                }

                await LocalStorage.InsertObject(key, value);
                this.loggerService.LogConsole(LogLevel.INFO, "Persisting succesfully object: {0} for key:{1}", null, value, key);
                return true;
            }
            catch
            {
                this.loggerService.LogConsole(LogLevel.WARN, "Error in persisting object for {0} for key{1}: ", null, value, key);
                return false;
            }
        }

        public async Task<bool> PersistData(string key, byte[] value)
        {
            try
            {
                IEnumerable<string> keys = await LocalStorage.GetAllKeys();
                if (keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    await LocalStorage.Invalidate(key);
                }

                await LocalStorage.Insert(key, value);
                this.loggerService.LogConsole(LogLevel.INFO, "Persisting succesfully object: {0} for key:{1}", null, value, key);
                return true;
            }
            catch
            {
                this.loggerService.LogConsole(LogLevel.WARN, "Error in persisting object for {0} for key{1}: ", null, value, key);
                return false;
            }
        }

        public async Task<T> LoadPersistedData<T>(string key) where T : class
        {
            try
            {
                IEnumerable<string> keys = await LocalStorage.GetAllKeys();
                if (!keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    this.loggerService.LogConsole(LogLevel.WARN, "Offline cache object for {0} not found", key);
                    return null;
                }

                var data = await LocalStorage.GetObject<T>(key);
                return data;
            }
            catch (Exception ex)
            {
                this.loggerService.LogConsole(LogLevel.WARN, "Error in load persisted data object for key{0}: \nError message {1}", key, ex.Message);
                return null;
            }
        }

        public async Task<byte[]> LoadPersistedData(string key)
        {
            try
            {
                IEnumerable<string> keys = await LocalStorage.GetAllKeys();
                if (!keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    this.loggerService.LogConsole(LogLevel.WARN, "Offline cache object for {0} not found", key);
                    return null;
                }

                byte[] offlineObject = await LocalStorage.Get(key);
                this.loggerService.LogConsole(LogLevel.INFO, "Get Persisted object for {0} :{1}", null, key, offlineObject);
                return offlineObject;
            }
            catch (KeyNotFoundException)
            {
                this.loggerService.LogConsole(LogLevel.WARN, "Offline cache object for {0} not found", key);
                return null;
            }
        }

        public async Task RemovePersistedData(string key)
        {
            try
            {
                IEnumerable<string> keys = await LocalStorage.GetAllKeys();
                if (keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    await LocalStorage.Invalidate(key);
                }
            }
            catch (KeyNotFoundException)
            {
                return;
            }
        }

        public async Task<bool> HasOnlineCache(string key)
        {
            IEnumerable<string> keys = await OnlineCache.GetAllKeys();
            return keys.Contains(key);
        }

        public void ClearAllCaches()
        {
            OfflineCache.InvalidateAll();
            OnlineCache.InvalidateAll();
            LocalStorage.InvalidateAll();
        }

        private IBlobCache NewLocalBlobCache(string databaseName)
        {
            //// This snippet is based on https://github.com/akavache/Akavache/blob/501b397d8c071366c3b6783aae3e98695b3d7442/src/Akavache.Sqlite3/Registrations.cs
            string cacheFolder = filesystemProvider.GetDefaultSecretCacheDirectory();
            filesystemProvider.CreateRecursive(cacheFolder).SubscribeOn(BlobCache.TaskpoolScheduler).Wait();
            SQLitePersistentBlobCache localBlobCache = new SQLitePersistentBlobCache(Path.Combine(filesystemProvider.GetDefaultLocalMachineCacheDirectory(), databaseName), BlobCache.TaskpoolScheduler);
            return localBlobCache;
        }

        private IBlobCache NewInMemoryBlobCache()
        {
            // This snippet is based on https://github.com/reactiveui/Akavache/blob/28f0c6dc84e6c9da5fc67e70b7b62ec661b825ee/src/Akavache.Core/BlobCache/BlobCache.cs
            InMemoryBlobCache inMemoryBlobCache = new InMemoryBlobCache(Scheduler.Default);
            return inMemoryBlobCache;
        }
    }
}
