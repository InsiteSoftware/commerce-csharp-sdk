using System.Collections.Generic;
using System.Linq;
using CommerceApiSDK.Models.ContentManagement.Converters;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    public enum WidgetType
    {
        Unknown,
        MobileCarousel,
        MobileCarouselSlide,
        MobileLinkList,
        ProductCarousel,
        MobileSearchHistory,
        MobileSpacer,
        MobileHeader,
        MobileCurrentLocation,
        MobileLocationNote,
        MobileRecentBinNote,
        MobilePreviousOrders,
    }

    public class Widget
    {
        [JsonProperty("contentKey")]
        public string Id { get; set; }

        [JsonConverter(typeof(WidgetTypeEnumConverter))]
        [JsonProperty("class")]
        public WidgetType Type { get; set; }

        [JsonIgnore]
        [JsonProperty("type")]
        public string SubType { get; set; }

        [JsonProperty(ItemConverterType = typeof(WidgetConverter), PropertyName = "childWidgets")]
        public virtual IList<Widget> ChildWidgets { get; set; }

        protected const int HashingMultiplier = 16777619;

        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ Id.GetHashCode();
                hash = (hash * HashingMultiplier) ^ Type.GetHashCode();
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, SubType) ? SubType.GetHashCode() : 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            // Is null?
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            // Is the same object?
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // Is the same type?
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Widget)obj);
        }

        public bool Equals(Widget widget)
        {
            // Is null?
            if (ReferenceEquals(null, widget))
            {
                return false;
            }

            // Is the same object?
            if (ReferenceEquals(this, widget))
            {
                return true;
            }

            bool result = Id == widget.Id
                && Type == widget.Type
                && ReferenceEquals(SubType, widget.SubType);

            if (result)
            {
                bool areChildWidgetsEqual = (ChildWidgets == null && widget.ChildWidgets == null)
                || (ChildWidgets != null && widget.ChildWidgets != null && Enumerable.SequenceEqual(ChildWidgets, widget.ChildWidgets));
                result &= areChildWidgetsEqual;
            }

            return result;
        }
    }
}
