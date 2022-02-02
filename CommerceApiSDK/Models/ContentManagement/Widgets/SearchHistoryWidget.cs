using Newtonsoft.Json;

namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    public class SearchHistoryWidget : Widget
    {
        public string Title { get; set; }

        [JsonProperty("numberOfPreviousSearches")]
        public int ItemsCount { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = base.GetHashCode();

                hash = (hash * HashingMultiplier) ^ ItemsCount.GetHashCode();
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

            return Equals((SearchHistoryWidget)obj);
        }

        public bool Equals(SearchHistoryWidget obj)
        {
            bool result = Equals((Widget)obj);
            if (result)
            {
                result = ItemsCount == obj.ItemsCount;
            }

            return result;
        }
    }
}
