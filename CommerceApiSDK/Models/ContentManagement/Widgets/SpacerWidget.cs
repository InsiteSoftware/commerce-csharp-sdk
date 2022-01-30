namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    using Newtonsoft.Json;

    public class SpacerWidget : Widget
    {
        public int Height { get; set; }

        [JsonProperty(PropertyName = "backgroundColor")]
        public string BackgroundColor { get; set; }
    }
}
