using System;
using CommerceApiSDK.Models.ContentManagement.Widgets;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CommerceApiSDK.Models.ContentManagement
{
    public class WidgetConverter : JsonCreationConverter<Widget>
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object basd on JObject
            Widget target = Create(objectType, jObject);

            try
            {
                // Populate the object properties
                serializer.Populate(jObject.CreateReader(), target);
            }
            catch (JsonException exception)
            {
                //ToDo replace the DefaultLogger in future commits.
                //DefaultLogger.StaticConsole(LogLevel.ERROR, $"Incorrect JSON format: {exception}");
                return null;
            }

            return target;
        }

        protected override Widget Create(Type objectType, JObject jObject)
        {
            Widget result = null;
            JToken widgetType = jObject["class"];
            if (widgetType != null)
            {
                string value = widgetType.Value<string>();


                if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobileCarousel))
                {
                    result = new CarouselWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobileLinkList))
                {
                    result = new ActionsWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.ProductCarousel))
                {
                    result = new ProductCarouselWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobileCarouselSlide))
                {
                    result = new CarouselSlideWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobileSearchHistory))
                {
                    result = new SearchHistoryWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobileHeader))
                {
                    result = new HeaderWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobileSpacer))
                {
                    result = new SpacerWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobileCurrentLocation))
                {
                    result = new CurrentLocationWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobilePreviousOrders))
                {
                    result = new PreviousOrdersWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobileLocationNote))
                {
                    result = new LocationNoteWidget();
                }
                else if (value == Enum.GetName(typeof(WidgetType), WidgetType.MobileRecentBinNote))
                {
                    result = new RecentBinNoteWidget();
                }
            }

            if (result == null)
            {
                result = new Widget();
            }

            return result;
        }
    }
}