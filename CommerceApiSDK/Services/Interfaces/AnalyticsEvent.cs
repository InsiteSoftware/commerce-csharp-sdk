using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Services.Interfaces
{
    public class AnalyticsEvent
    {
        public static readonly string EventPropertyScreenName = "screen_name";

        public string EventName { get; }

        public Dictionary<string, string> Properties { get; }

        public AnalyticsEvent(string eventName, string area)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            if (string.IsNullOrEmpty(area))
            {
                throw new ArgumentNullException(nameof(area));
            }

            EventName = eventName;
            Properties = new Dictionary<string, string>();
            WithProperty(EventPropertyScreenName, area);
        }

        public AnalyticsEvent WithProperty(string name, string value)
        {
            Properties[name] = value;
            return this;
        }

        public AnalyticsEvent WithProperty(string name, bool value)
        {
            return WithProperty(name, value.ToString().ToLowerInvariant());
        }
    }
}
