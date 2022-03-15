using System.Threading.Tasks;
using Akavache;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface ICacheService
    {
        /// <summary>
        /// The short in-memory cache for online requests
        /// </summary>
        IBlobCache OnlineCache { get; }

        /// <summary>
        /// The long on-device persistent cache for offline requests
        /// </summary>
        IBlobCache OfflineCache { get; }

        /// <summary>
        /// The on-device database for storing configurations, settings, preferences, etc.
        /// </summary>
        IBlobCache LocalStorage { get; }

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
        Task<bool> PersistData<T>(string key, T value) where T : class;

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
        Task<T> LoadPersistedData<T>(string key) where T : class;

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
    }
}