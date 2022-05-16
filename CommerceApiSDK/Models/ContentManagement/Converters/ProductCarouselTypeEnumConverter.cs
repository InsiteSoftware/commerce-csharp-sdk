using System;
using CommerceApiSDK.Models.ContentManagement.Widgets;
using CommerceApiSDK.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CommerceApiSDK.Models.ContentManagement.Converters
{
    public class ProductCarouselTypeEnumConverter : StringEnumConverter
    {
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            object result;

            try
            {
                result = base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException)
            {
                result = ProductCarouselType.Unknown;
            }

            return result;
        }
    }
}
