using System;
using System.Collections.Generic;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.DemoApp.Services
{
    public class TrackingService : ITrackingService
    {
        public ISessionService SessionService { get; }

        public void Initialize() { }

        public void TrackEvent(AnalyticsEvent analyticsEvent) { }

        public void TrackException(
            Exception exception,
            Dictionary<string, string> properties = null
        ) { }

        public void ForceCrash() { }

        public void SetUserID(string userId) { }
    }
}
