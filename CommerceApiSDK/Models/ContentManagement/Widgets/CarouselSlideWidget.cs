namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    using System;
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
            get => this.primaryTextColorHex;
            set
            {
                this.primaryTextColorHex = value;
                this.PrimaryTextColor = MvxHexParser.ColorFromHexString(value, true);
            }
        }

        [JsonIgnore]
        public Color PrimaryTextColor { get; set; }

        private string secondaryTextColorHex;

        [JsonProperty("secondaryTextColor")]
        public string SecondaryTextColorHex
        {
            get => this.secondaryTextColorHex;
            set
            {
                this.secondaryTextColorHex = value;
                this.SecondaryTextColor = MvxHexParser.ColorFromHexString(value, true);
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
                var hash = base.GetHashCode();

                hash = (hash * HashingMultiplier) ^ (this.ApplyDarkOverlayToImage ? 1 : 0);
                hash = (hash * HashingMultiplier) ^ this.TextJustification.GetHashCode();

                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.ImagePath) ? this.ImagePath.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.Link) ? this.Link.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.PrimaryText) ? this.PrimaryText.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.SecondaryText) ? this.SecondaryText.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.PrimaryTextColorHex) ? this.PrimaryTextColorHex.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.SecondaryTextColorHex) ? this.SecondaryTextColorHex.GetHashCode() : 0);

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            // Is null?
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            // Is the same object?
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            // Is the same type?
            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((CarouselSlideWidget)obj);
        }

        public bool Equals(CarouselSlideWidget widget)
        {
            var result = base.Equals((Widget)widget);

            if (result)
            {
                result &= this.ApplyDarkOverlayToImage == widget.ApplyDarkOverlayToImage
                    && this.TextJustification == widget.TextJustification
                    && object.Equals(this.ImagePath, widget.ImagePath)
                    && object.Equals(this.Link, widget.Link)
                    && object.Equals(this.PrimaryText, widget.PrimaryText)
                    && object.Equals(this.SecondaryText, widget.SecondaryText)
                    && object.Equals(this.PrimaryTextColorHex, widget.PrimaryTextColorHex)
                    && object.Equals(this.SecondaryTextColorHex, widget.SecondaryTextColorHex);
            }

            return result;
        }
    }
}