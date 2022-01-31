namespace CommerceApiSDK.Models.ContentManagement.Converters
{
    using System;
    using CommerceApiSDK.Models.ContentManagement.Widgets;
    using Newtonsoft.Json;

    public class PageWidgetTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(WidgetType).IsAssignableFrom(objectType);
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            WidgetType result;
            string enumString = (string)reader.Value;
            switch (enumString.ToLower())
            {
                case "mobile/slideshow":
                case "mobilecarousel":
                    result = WidgetType.MobileCarousel;
                    break;
                case "mobilecarouselslide":
                    result = WidgetType.MobileCarouselSlide;
                    break;
                case "mobile/linklist":
                case "mobilelinklist":
                    result = WidgetType.MobileLinkList;
                    break;
                case "mobile/productcarousel":
                case "productcarousel":
                    result = WidgetType.ProductCarousel;
                    break;
                case "mobile/searchhistory":
                case "mobilesearchhistory":
                    result = WidgetType.MobileSearchHistory;
                    break;
                default:
                    result = WidgetType.Unknown;
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}