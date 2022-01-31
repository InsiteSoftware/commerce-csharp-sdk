namespace CommerceApiSDK.Models.ContentManagement.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using CommerceApiSDK.Models.ContentManagement.Converters;
    using CommerceApiSDK.Models.ContentManagement.Widgets;
    using MvvmCross.Plugin.Color;
    using Newtonsoft.Json;

    public enum PageType
    {
        Unknown,
        MobileAccount,
        MobileShop,
        MobileSearch,
    }

    public class PageInformation
    {
        public string Title { get; set; }

        public Guid Id { get; set; }

        public Guid NodeId { get; set; }

        public Guid ParentId { get; set; }

        public Guid WebsiteId { get; set; }

        public string Name { get; set; }

        public string VariantName { get; set; }

        public int SortOrder { get; set; }

        [JsonProperty(ItemConverterType = typeof(PageTypeConverter))]
        public PageType Type { get; set; }

        public List<PageWidget> Widgets { get; set; }

        [JsonProperty("generalFields")]
        public PageSettings PageSettings { get; set; }
    }

    public class PageContentManagement : BaseModel
    {
        [JsonProperty("page")]
        public PageInformation PageInformation { get; set; }

        public List<Widget> Widgets { get; set; }

        public int StatusCode { get; set; }

        public string RedirectTo { get; set; }

        [JsonProperty("translatableFields")]
        public Localization TextLocalized { get; set; }
    }

    public class PageSettings
    {
        public bool HideHeader { get; set; }
        public bool HideFooter { get; set; }
        public bool HideFromSearchEngines { get; set; }
        public bool HideFromSiteSearch { get; set; }
        public bool HideBreadcrumbs { get; set; }
        public bool ExcludeFromNavigation { get; set; }
        public bool ExcludeFromSignInRequired { get; set; }
        public string VariantName { get; set; }
        public string HorizontalRule { get; set; }
        public List<string> Tags { get; set; }
    }

    public class PageWidget
    {
        public string Id { get; set; }

        public Guid ParentId { get; set; }

        [JsonProperty(ItemConverterType = typeof(PageWidgetTypeConverter))]
        public WidgetType Type { get; set; }

        public string Zone { get; set; }

        [JsonProperty("generalFields")]
        public PageWidgetFields PageWidgetFields { get; set; }

        [JsonProperty("translatableFields")]
        public Localization TextLocalized { get; set; }
    }

    public class PageWidgetFields
    {
        public int? PreviousSearches { get; set; }

        public int? TimerSpeed { get; set; }

        public int? AnimationSpeed { get; set; }

        public bool ShowImage { get; set; }

        public bool ShowTitle { get; set; }

        public bool ShowPrice { get; set; }

        public bool ShowPartNumbers { get; set; }

        [JsonConverter(typeof(ProductCarouselTypeEnumConverter))]
        public ProductCarouselType CarouselType { get; set; }

        public List<Guid> SelectedCategoryIds { get; private set; }

        [JsonConverter(typeof(TopSellersCategoriesSpanEnumConverter))]
        public TopSellersCategoriesSpan DisplayProductsFrom { get; set; }

        [JsonProperty(ItemConverterType = typeof(ActionsLayoutEnumConverter))]
        public ActionsLayout Layout { get; set; }

        public List<PageLink> Links { get; set; }

        public List<PageSlide> Slides { get; set; }
    }

    public class PageLink
    {
        [JsonProperty(ItemConverterType = typeof(ActionConverter), PropertyName = "fields")]
        public ActionsWidget.Action Action { get; set; }
    }

    public class PageSlide
    {
        [JsonProperty("fields")]
        public Slide Slide { get; set; }
    }

    public class Slide
    {
        public string Image { get; set; }

        public string Link { get; set; }

        public string Background { get; set; }

        public string Heading { get; set; }

        public string Subheading { get; set; }

        public bool ApplyDarkOverlayToImage { get; set; }

        private string backgroundColor;
        public string BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                BackgroundTextColor = MvxHexParser.ColorFromHexString(value, true);
            }
        }

        [JsonIgnore]
        public Color BackgroundTextColor { get; set; }

        [JsonIgnore]
        public Color PrimaryTextColor { get; set; }

        private string headingColor;
        public string HeadingColor
        {
            get => headingColor;
            set
            {
                headingColor = value;
                PrimaryTextColor = MvxHexParser.ColorFromHexString(value, true);
            }
        }

        private string subheadingColor;
        public string SubheadingColor
        {
            get => subheadingColor;
            set
            {
                subheadingColor = value;
                SecondaryTextColor = MvxHexParser.ColorFromHexString(value, true);
            }
        }

        [JsonIgnore]
        public Color SecondaryTextColor { get; set; }

        [JsonConverter(typeof(TextJustificationEnumConverter))]
        public TextJustification TextAlignment { get; set; }
    }

    public class Localization
    {
        public dynamic Title { get; set; }
    }
}