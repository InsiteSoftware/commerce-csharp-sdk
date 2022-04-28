namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which determines network availability
    /// </summary>
    public interface INetworkService
    {
        bool IsOnline();
    }
}
