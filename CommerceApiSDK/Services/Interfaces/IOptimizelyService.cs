using System;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IOptimizelyService
    {
        void Init(string host, string clientId, string clientSecret, bool isCachingEnabled);
    }
}
