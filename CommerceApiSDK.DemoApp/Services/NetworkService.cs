using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.DemoApp.Services
{
    public class NetworkService : INetworkService
    {
        public bool IsOnline()
        {
            return true;
        }
    }
}
