using System;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.ContentManagement.Converters;
using CommerceApiSDK.Models.ContentManagement.Widgets;
using CommerceApiSDK.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CommerceApiSDK
{
    public class ActionConverter : JsonCreationConverter<ActionsWidget.Action>
    {
        private АctionTypeEnumConverter аctionTypeEnumConverter;

        public ActionConverter()
        {
            аctionTypeEnumConverter = new АctionTypeEnumConverter();
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            ActionsWidget.Action result;
            if (reader.TokenType == JsonToken.String)
            {
                ActionType type = (ActionType)аctionTypeEnumConverter.ReadJson(reader, typeof(ActionType), existingValue, serializer);
                result = new ActionsWidget.Action
                {
                    Type = type
                };
            }
            else
            {
                result = (ActionsWidget.Action)base.ReadJson(reader, objectType, existingValue, serializer);
            }

            return result;
        }

        protected override ActionsWidget.Action Create(Type objectType, JObject jObject)
        {
            return new ActionsWidget.Action();
        }
    }
}
