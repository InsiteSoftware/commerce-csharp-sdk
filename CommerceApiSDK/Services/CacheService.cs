namespace CommerceApiSDK.Services
{
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
    using CommerceApiSDK.Utils.Logger;

    public class CacheService : ICacheService
    {
        private const string OfflineCacheDatabaseName = "blobs.db";
        private const string LocalStorageDatabaseName = "storage.db";

        /// <summary>
        /// number of minutes to cache data while online.
        /// </summary>
        public static readonly int OnlineCacheMinutes = 15;

        /// <summary>
        /// number of minutes to cache data while offline.
        /// </summary>
        public static readonly int OfflineCacheMinutes = 3 * 24 * 60;

        private Lazy<IBlobCache> onlineCache;
        public IBlobCache OnlineCache => this.onlineCache.Value;

        private Lazy<IBlobCache> offlineCache;
        public IBlobCache OfflineCache => this.offlineCache.Value;

        private Lazy<IBlobCache> localStorage;
        public IBlobCache LocalStorage => this.localStorage.Value;

        private readonly IFilesystemProvider filesystemProvider;

        public CacheService(IFilesystemProvider filesystemProvider)
        {
            this.filesystemProvider = filesystemProvider;
            this.offlineCache = new Lazy<IBlobCache>(() => this.NewLocalBlobCache(OfflineCacheDatabaseName));
            this.onlineCache = new Lazy<IBlobCache>(this.NewInMemoryBlobCache);
            this.localStorage = new Lazy<IBlobCache>(() => this.NewLocalBlobCache(LocalStorageDatabaseName));
        }

        public void Shutdown()
        {
            // If the cache has not been accessed then no need to flush it
            if (this.offlineCache.IsValueCreated)
            {
                this.offlineCache.Value.Dispose();
                this.offlineCache.Value.Shutdown.Wait();
                this.offlineCache = new Lazy<IBlobCache>(() => this.NewLocalBlobCache(OfflineCacheDatabaseName));
            }

            if (this.localStorage.IsValueCreated)
            {
                this.localStorage.Value.Dispose();
                this.localStorage.Value.Shutdown.Wait();
                this.localStorage = new Lazy<IBlobCache>(() => this.NewLocalBlobCache(LocalStorageDatabaseName));
            }

            if (this.onlineCache.IsValueCreated)
            {
                this.onlineCache.Value.Dispose();
                this.onlineCache.Value.Shutdown.Wait();
                this.onlineCache = new Lazy<IBlobCache>(this.NewInMemoryBlobCache);
            }
        }

        public async Task<bool> PersistData<T>(string key, T value) where T : class
        {
            try
            {
                var keys = await this.LocalStorage.GetAllKeys();
                if (keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    await this.LocalStorage.Invalidate(key);
                }

                await this.LocalStorage.InsertObject<T>(key, value);
                Logger.LogTrace("Persisting succesfully object: {0} for key:{1}", null, value, key);
                return true;
            }
            catch
            {
                Logger.LogWarn("Error in persisting object for {0} for key{1}: ", null, value, key);
                return false;
            }
        }

        public async Task<bool> PersistData(string key, byte[] value)
        {
            try
            {
                var keys = await this.LocalStorage.GetAllKeys();
                if (keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    await this.LocalStorage.Invalidate(key);
                }

                await this.LocalStorage.Insert(key, value);
                Logger.LogTrace("Persisting succesfully object: {0} for key:{1}", null, value, key);
                return true;
            }
            catch
            {
                Logger.LogWarn("Error in persisting object for {0} for key{1}: ", null, value, key);
                return false;
            }
        }

        public async Task<T> LoadPersistedData<T>(string key) where T : class
        {
            try
            {
                var keys = await this.LocalStorage.GetAllKeys();
                if (!keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    Logger.LogWarn("Offline cache object for {0} not found", key);
                    return null;
                }

                var data = await this.LocalStorage.GetObject<T>(key);
                return data;
            }
            catch (Exception ex)
            {
                Logger.LogWarn("Error in load persisted data object for key{0}: \nError message {1}", key, ex.Message);
                return null;
            }
        }

        public async Task<byte[]> LoadPersistedData(string key)
        {
            try
            {
                var keys = await this.LocalStorage.GetAllKeys();
                if (!keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    Logger.LogWarn("Offline cache object for {0} not found", key);
                    return null;
                }

                var offlineObject = await this.LocalStorage.Get(key);
                Logger.LogTrace("Get Persisted object for {0} :{1}", null, key, offlineObject);
                return offlineObject;
            }
            catch (KeyNotFoundException)
            {
                Logger.LogWarn("Offline cache object for {0} not found", key);
                return null;
            }
        }

        public async Task RemovePersistedData(string key)
        {
            try
            {
                var keys = await this.LocalStorage.GetAllKeys();
                if (keys.Any(x => x.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    await this.LocalStorage.Invalidate(key);
                }
            }
            catch (KeyNotFoundException)
            {
                return;
            }
        }

        private IBlobCache NewLocalBlobCache(string databaseName)
        {
            //// This snippet is based on https://github.com/akavache/Akavache/blob/501b397d8c071366c3b6783aae3e98695b3d7442/src/Akavache.Sqlite3/Registrations.cs
            var cacheFolder = this.filesystemProvider.GetDefaultSecretCacheDirectory();
            this.filesystemProvider.CreateRecursive(cacheFolder).SubscribeOn(BlobCache.TaskpoolScheduler).Wait();
            var localBlobCache = new SQLitePersistentBlobCache(Path.Combine(this.filesystemProvider.GetDefaultLocalMachineCacheDirectory(), databaseName), BlobCache.TaskpoolScheduler);
            return localBlobCache;
        }

        private IBlobCache NewInMemoryBlobCache()
        {
            // This snippet is based on https://github.com/reactiveui/Akavache/blob/28f0c6dc84e6c9da5fc67e70b7b62ec661b825ee/src/Akavache.Core/BlobCache/BlobCache.cs
            var inMemoryBlobCache = new InMemoryBlobCache(Scheduler.Default);
            return inMemoryBlobCache;
        }
    }
}
