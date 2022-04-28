using System;
using System.Runtime.Serialization;

namespace CommerceApiSDK.Models.Enums
{
    public enum ProductCarouselType
    {
        Unknown,
        FeaturedCategory,
        RecentlyViewed,
        TopSellers,
        [EnumMember(Value = "crossSells")]
        WebCrossSells,
    }
}
