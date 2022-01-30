namespace CommerceApiSDK.Services
{
    using System;
    using CommerceApiSDK.Services.Interfaces;

    public abstract class NetworkServiceBase : INetworkService
    {
        private DateTime lastOnlineCheck;
        private bool? onlineState;

        protected abstract bool PlatformIsOnline();

        public bool IsOnline()
        {
            if (this.onlineState.HasValue && DateTime.Now - this.lastOnlineCheck < TimeSpan.FromSeconds(2))
            {
                return this.onlineState.Value;
            }

            this.onlineState = this.PlatformIsOnline();
            this.lastOnlineCheck = DateTime.Now;
            return this.onlineState.Value;
        }
    }
}
