namespace CommerceApiSDK.Models.ContentManagement.Converters
{
    using System;
    using CommerceApiSDK.Models.ContentManagement.Widgets;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class АctionTypeEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object result = null;

            try
            {
                result = base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException)
            {
                if (reader?.Value is string enumValue && enumValue.Trim().ToLower() == "quick_order")
                {
                    result = ActionType.QuickOrder;
                }
                else
                {
                    result = ActionType.Unknown;
                }
            }

            return result;
        }
    }
}
