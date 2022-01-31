namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    using System.Drawing;
    using CommerceApiSDK.Models.ContentManagement.Converters;
    using MvvmCross.Plugin.Color;
    using Newtonsoft.Json;

    public enum TextJustification
    {
        Left,
        Center,
        Right,
    }

    public class CarouselSlideWidget : Widget
    {
        [JsonProperty("imageUrl")]
        public string ImagePath { get; set; }

        public string Link { get; set; }

        public string PrimaryText { get; set; }

        public string SecondaryText { get; set; }

        public bool ApplyDarkOverlayToImage { get; set; }

        private string primaryTextColorHex;

        [JsonProperty("primaryTextColor")]
        public string PrimaryTextColorHex
        {
            get => primaryTextColorHex;
            set
            {
                primaryTextColorHex = value;
                PrimaryTextColor = MvxHexParser.ColorFromHexString(value, true);
            }
        }

        [JsonIgnore]
        public Color PrimaryTextColor { get; set; }

        private string secondaryTextColorHex;

        [JsonProperty("secondaryTextColor")]
        public string SecondaryTextColorHex
        {
            get => secondaryTextColorHex;
            set
            {
                secondaryTextColorHex = value;
                SecondaryTextColor = MvxHexParser.ColorFromHexString(value, true);
            }
        }

        [JsonIgnore]
        public Color SecondaryTextColor { get; set; }

        [JsonConverter(typeof(TextJustificationEnumConverter))]
        public TextJustification TextJustification { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = base.GetHashCode();

                hash = (hash * HashingMultiplier) ^ (ApplyDarkOverlayToImage ? 1 : 0);
                hash = (hash * HashingMultiplier) ^ TextJustification.GetHashCode();

                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, ImagePath) ? ImagePath.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Link) ? Link.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, PrimaryText) ? PrimaryText.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, SecondaryText) ? SecondaryText.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, PrimaryTextColorHex) ? PrimaryTextColorHex.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, SecondaryTextColorHex) ? SecondaryTextColorHex.GetHashCode() : 0);

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            // Is null?
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            // Is the same object?
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // Is the same type?
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((CarouselSlideWidget)obj);
        }

        public bool Equals(CarouselSlideWidget widget)
        {
            bool result = Equals((Widget)widget);

            if (result)
            {
                result &= ApplyDarkOverlayToImage == widget.ApplyDarkOverlayToImage
                    && TextJustification == widget.TextJustification
                    && Equals(ImagePath, widget.ImagePath)
                    && Equals(Link, widget.Link)
                    && Equals(PrimaryText, widget.PrimaryText)
                    && Equals(SecondaryText, widget.SecondaryText)
                    && Equals(PrimaryTextColorHex, widget.PrimaryTextColorHex)
                    && Equals(SecondaryTextColorHex, widget.SecondaryTextColorHex);
            }

            return result;
        }
    }
}