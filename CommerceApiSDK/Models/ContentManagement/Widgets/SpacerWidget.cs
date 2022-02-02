using Newtonsoft.Json;

namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    public class SpacerWidget : Widget
    {
        public int Height { get; set; }

        [JsonProperty(PropertyName = "backgroundColor")]
        public string BackgroundColor { get; set; }
    }
}
