namespace CommerceApiSDK.Services.Interfaces
{
    using System;
    using System.Collections.Generic;

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

            this.EventName = eventName;
            this.Properties = new Dictionary<string, string>();
            this.WithProperty(AnalyticsEvent.EventPropertyScreenName, area);
        }

        public AnalyticsEvent WithProperty(string name, string value)
        {
            this.Properties[name] = value;
            return this;
        }

        public AnalyticsEvent WithProperty(string name, bool value)
        {
            return this.WithProperty(name, value.ToString().ToLowerInvariant());
        }
    }
}
