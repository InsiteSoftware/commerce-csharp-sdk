namespace CommerceApiSDK.Models.ContentManagement.Widgets
{
    using Newtonsoft.Json;

    public class SearchHistoryWidget : Widget
    {
        public string Title { get; set; }

        [JsonProperty("numberOfPreviousSearches")]
        public int ItemsCount { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = base.GetHashCode();

                hash = (hash * HashingMultiplier) ^ this.ItemsCount.GetHashCode();
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

            return this.Equals((SearchHistoryWidget)obj);
        }

        public bool Equals(SearchHistoryWidget obj)
        {
            var result = base.Equals((Widget)obj);
            if (result)
            {
                result = this.ItemsCount == obj.ItemsCount;
            }

            return result;
        }
    }
}
