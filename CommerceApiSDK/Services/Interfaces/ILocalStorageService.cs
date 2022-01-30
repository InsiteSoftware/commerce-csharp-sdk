namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which manages persitant local storage
    /// </summary>
    public interface ILocalStorageService
    {
        /// <summary>
        /// Load Persisted value from device
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Value</returns>
        string Load(string key);

        /// <summary>
        /// Load Persisted value from device. Return default value of KeyValue is null
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        string Load(string key, string defaultValue);

        int LoadInt(string key);

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>Success</returns>
        void Save(string key, string value);

        void Save(string key, int value);

        /// <summary>
        /// ClearAll
        /// </summary>
        /// <returns>Success</returns>
        bool ClearAll();

        /// <summary>
        /// Remove
        /// </summary>
        /// <returns>Success</returns>
        bool Remove(string key);
    }
}