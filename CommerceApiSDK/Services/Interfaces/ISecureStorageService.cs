namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which can persist sensitive data securely on the device
    /// </summary>
    public interface ISecureStorageService
    {
        string Load(string key);
        bool Save(string key, string value);
        bool Remove(string key);
        bool ClearAll();
    }
}
