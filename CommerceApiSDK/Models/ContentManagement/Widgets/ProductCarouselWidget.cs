namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using CommerceApiSDK.Models.ContentManagement.Converters;
    using Newtonsoft.Json;

    public enum ProductCarouselType
    {
        Unknown,
        FeaturedCategory,
        RecentlyViewed,
        TopSellers,
        [EnumMember(Value = "crossSells")]
        WebCrossSells,
    }

    public enum TopSellersCategoriesSpan
    {
        Unknown,
        AllCategories,
        SelectCategories,
    }

    public class ProductCarouselWidget : Widget
    {
        [JsonConverter(typeof(ProductCarouselTypeEnumConverter))]
        public ProductCarouselType CarouselType { get; set; }

        public string Title { get; set; }

        public int NumberOfProductsToDisplay { get; set; }

        public bool DisplayPartNumbers { get; set; }

        public bool DisplayPrice { get; set; }

        [JsonConverter(typeof(TopSellersCategoriesSpanEnumConverter))]
        public TopSellersCategoriesSpan DisplayTopSellersFrom { get; set; }

        private string selectedCategoryIdsString;
        [JsonProperty("selectedCategoryIds")]
        public string SelectedCategoryIdsString
        {
            get => this.selectedCategoryIdsString;
            set
            {
                this.selectedCategoryIdsString = value;
                this.SelectedCategoryIds = this.selectedCategoryIdsString?.Split(',').ToList().Select(s => new Guid(s)).ToList();
            }
        }

        [JsonIgnore]
        public List<Guid> SelectedCategoryIds { get; set; }
        [JsonIgnore]
        public bool ShouldForceLoadData { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = base.GetHashCode();
                hash = (hash * HashingMultiplier) ^ (this.SelectedCategoryIdsString?.GetHashCode() ?? 0);
                hash = (hash * HashingMultiplier) ^ this.NumberOfProductsToDisplay.GetHashCode();
                hash = (hash * HashingMultiplier) ^ (this.Title?.GetHashCode() ?? 0);
                hash = (hash * HashingMultiplier) ^ this.CarouselType.GetHashCode();
                hash = (hash * HashingMultiplier) ^ this.DisplayPartNumbers.GetHashCode();
                hash = (hash * HashingMultiplier) ^ this.DisplayPrice.GetHashCode();
                hash = (hash * HashingMultiplier) ^ this.DisplayTopSellersFrom.GetHashCode();
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

            return this.Equals((ProductCarouselWidget)obj);
        }

        public bool Equals(ProductCarouselWidget obj)
        {
            var result = base.Equals((Widget)obj);

            if (result)
            {
                result &= this.SelectedCategoryIdsString == obj.SelectedCategoryIdsString;
                result &= this.NumberOfProductsToDisplay == obj.NumberOfProductsToDisplay;
                result &= this.Title == obj.Title;
                result &= this.CarouselType == obj.CarouselType;
                result &= this.DisplayPartNumbers == obj.DisplayPartNumbers;
                result &= this.DisplayPrice == obj.DisplayPrice;
                result &= this.DisplayTopSellersFrom == obj.DisplayTopSellersFrom;
            }

            return result;
        }
    }
}
