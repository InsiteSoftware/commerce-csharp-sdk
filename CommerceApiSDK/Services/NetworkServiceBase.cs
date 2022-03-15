using System;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public abstract class NetworkServiceBase : INetworkService
    {
        private DateTime lastOnlineCheck;
        private bool? onlineState;

        protected abstract bool PlatformIsOnline();

        public bool IsOnline()
        {
            if (onlineState.HasValue && DateTime.Now - lastOnlineCheck < TimeSpan.FromSeconds(2))
            {
                return onlineState.Value;
            }

            onlineState = PlatformIsOnline();
            lastOnlineCheck = DateTime.Now;
            return onlineState.Value;
        }
    }
}
