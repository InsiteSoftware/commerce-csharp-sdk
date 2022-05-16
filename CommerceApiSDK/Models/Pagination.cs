using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class Pagination
    {
        /// <summary>Gets or sets the current page.</summary>
        [Obsolete]
        public int CurrentPage { get; set; }

        /// <summary>Gets or sets the page.</summary>
        public int Page { get; set; }

        /// <summary>Gets or sets the size of the page.</summary>
        public int PageSize { get; set; }

        /// <summary>Gets or sets the default size of the page.</summary>
        public int DefaultPageSize { get; set; }

        /// <summary>Gets or sets the total item count.</summary>
        public int TotalItemCount { get; set; }

        /// <summary>Gets or sets the number of pages.</summary>
        public int NumberOfPages { get; set; }

        /// <summary>Gets or sets the page size options.</summary>
        public IList<int> PageSizeOptions { get; set; }

        /// <summary>Gets or sets the sort options.</summary>
        public IList<SortOption> SortOptions { get; set; }

        /// <summary>Gets or sets the type of the sort.</summary>
        public string SortType { get; set; }

        /// <summary>full url to rest endpoint to retrieve next page or null if no next page</summary>
        public string NextPageUri { get; set; }

        /// <summary>full url to rest endpoint to retrieve previous page or null if no previous page</summary>
        public string PrevPageUri { get; set; }
    }
}
