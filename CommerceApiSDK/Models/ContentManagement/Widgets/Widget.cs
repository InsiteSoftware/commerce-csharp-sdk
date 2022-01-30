namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    using System.Collections.Generic;
    using System.Linq;
    using CommerceApiSDK.Models.ContentManagement.Converters;
    using Newtonsoft.Json;

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
                hash = (hash * HashingMultiplier) ^ this.Id.GetHashCode();
                hash = (hash * HashingMultiplier) ^ this.Type.GetHashCode();
                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.SubType) ? this.SubType.GetHashCode() : 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            // Is null?
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            // Is the same object?
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            // Is the same type?
            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Widget)obj);
        }

        public bool Equals(Widget widget)
        {
            // Is null?
            if (object.ReferenceEquals(null, widget))
            {
                return false;
            }

            // Is the same object?
            if (object.ReferenceEquals(this, widget))
            {
                return true;
            }

            var result = this.Id == widget.Id
                && this.Type == widget.Type
                && object.ReferenceEquals(this.SubType, widget.SubType);

            if (result)
            {
                var areChildWidgetsEqual = (this.ChildWidgets == null && widget.ChildWidgets == null)
                || (this.ChildWidgets != null && widget.ChildWidgets != null && Enumerable.SequenceEqual(this.ChildWidgets, widget.ChildWidgets));
                result &= areChildWidgetsEqual;
            }

            return result;
        }
    }
}
