namespace CommerceApiSDK.Models
{
    public class SortOption
    {
        /// <summary>Gets or sets the sort string to display in UI.</summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the sort types (SortOrderType constants) in order to show in UI - lowest is default. pass this value to SortBy as a
        /// string.
        /// </summary>
        public string SortType { get; set; }
    }
}

