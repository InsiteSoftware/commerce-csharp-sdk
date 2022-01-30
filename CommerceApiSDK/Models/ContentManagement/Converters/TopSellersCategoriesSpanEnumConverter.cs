namespace CommerceApiSDK.Models.ContentManagement.Converters
{
    using System;
    using CommerceApiSDK.Models.ContentManagement.Widgets;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class TopSellersCategoriesSpanEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object result;

            try
            {
                result = base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException)
            {
                result = TopSellersCategoriesSpan.Unknown;
            }

            return result;
        }
    }
}
