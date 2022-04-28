using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service for event tracking and error logging.
    /// </summary>
    public interface ITrackingService
    {
        ISessionService SessionService { get; }

        void Initialize();

        void TrackEvent(AnalyticsEvent analyticsEvent);

        void TrackException(Exception exception, Dictionary<string, string> properties = null);

        void ForceCrash();

        void SetUserID(string userId);
    }
}
