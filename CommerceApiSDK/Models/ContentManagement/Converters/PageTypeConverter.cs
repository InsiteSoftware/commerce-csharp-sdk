﻿using System;
using CommerceApiSDK.Models.ContentManagement.Pages;
using CommerceApiSDK.Models.Enums;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models.ContentManagement.Converters
{
    public class PageTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(PageType).IsAssignableFrom(objectType);
        }

        public override bool CanWrite => false;

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            PageType result;
            string enumString = (string)reader.Value;
            switch (enumString.ToLower())
            {
                case "mobile/account":
                case "mobileaccount":
                    result = PageType.MobileAccount;
                    break;
                case "mobile/shop":
                case "mobileshop":
                    result = PageType.MobileShop;
                    break;
                case "mobile/search":
                case "mobilesearch":
                    result = PageType.MobileSearch;
                    break;
                default:
                    result = PageType.Unknown;
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
