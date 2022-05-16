using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using CommerceApiSDK.Models.ContentManagement.Converters;
using CommerceApiSDK.Models.Enums;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
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
            get => selectedCategoryIdsString;
            set
            {
                selectedCategoryIdsString = value;
                SelectedCategoryIds = selectedCategoryIdsString?
                    .Split(',')
                    .ToList()
                    .Select(s => new Guid(s))
                    .ToList();
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
                int hash = base.GetHashCode();
                hash = (hash * HashingMultiplier) ^ (SelectedCategoryIdsString?.GetHashCode() ?? 0);
                hash = (hash * HashingMultiplier) ^ NumberOfProductsToDisplay.GetHashCode();
                hash = (hash * HashingMultiplier) ^ (Title?.GetHashCode() ?? 0);
                hash = (hash * HashingMultiplier) ^ CarouselType.GetHashCode();
                hash = (hash * HashingMultiplier) ^ DisplayPartNumbers.GetHashCode();
                hash = (hash * HashingMultiplier) ^ DisplayPrice.GetHashCode();
                hash = (hash * HashingMultiplier) ^ DisplayTopSellersFrom.GetHashCode();
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

            return Equals((ProductCarouselWidget)obj);
        }

        public bool Equals(ProductCarouselWidget obj)
        {
            bool result = Equals((Widget)obj);

            if (result)
            {
                result &= SelectedCategoryIdsString == obj.SelectedCategoryIdsString;
                result &= NumberOfProductsToDisplay == obj.NumberOfProductsToDisplay;
                result &= Title == obj.Title;
                result &= CarouselType == obj.CarouselType;
                result &= DisplayPartNumbers == obj.DisplayPartNumbers;
                result &= DisplayPrice == obj.DisplayPrice;
                result &= DisplayTopSellersFrom == obj.DisplayTopSellersFrom;
            }

            return result;
        }
    }
}
