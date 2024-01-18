using System;
using System.Threading.Tasks;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface ICacheService
    {
        /// <summary>
        /// number of minutes to cache data while online.
        /// </summary>
        int OnlineCacheMinutes { get; }

        /// <summary>
        /// number of minutes to cache data while offline.
        /// </summary>
        int OfflineCacheMinutes { get; }

        /// <summary>
        /// Ensure that everything is closed and written and happy
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Store an object into the Local Storage.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="key">Database key for which the value is to correspond.</param>
        /// <param name="value">The object.</param>
        /// <returns>Whether the action was successful or not.</returns>
        Task<bool> PersistData<T>(string key, T value)
            where T : class;

        /// <summary>
        /// Store raw bytes into the Local Storage.
        /// </summary>
        /// <param name="key">Database key for which the value is to correspond.</param>
        /// <param name="value">The object.</param>
        /// <returns>Whether the action was successful or not.</returns>
        Task<bool> PersistData(string key, byte[] value);

        /// <summary>
        /// Retrieve an object from the Local Storage.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="key">Database key for which the object corresponds.</param>
        /// <returns>The object.</returns>
        Task<T> LoadPersistedData<T>(string key)
            where T : class;

        /// <summary>
        /// Retrieve raw bytes data from the Local Storage.
        /// </summary>
        /// <param name="key">Database key for which the data corresponds.</param>
        /// <returns>The data.</returns>
        Task<byte[]> LoadPersistedData(string key);

        /// <summary>
        /// Remove all data associated with the given database key.
        /// </summary>
        /// <param name="key">Database key.</param>
        Task RemovePersistedData(string key);

        Task<bool> HasOnlineCache(string key);

        void ClearAllCaches();

        Task<T> GetOrFetchObject<T>(
            string key,
            Func<Task<T>> fetchFunc,
            DateTimeOffset? absoluteExpiration = null
        );
        Task Invalidate(string key);
        Task InvalidateObjectWithKeysStartingWith<T>(string keyPrefix);
        Task InvalidateObject<T>(string key);
        Task InvalidateAllObjects<T>();
        Task InsertObject<T>(string key, T value, DateTimeOffset? absoluteExpiration = null);
        Task<T> GetObject<T>(string key);
    }
}
