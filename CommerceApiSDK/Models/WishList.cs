using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    public class WishList : BaseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool CanAddAllToCart { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedByDisplayName { get; set; }

        public int WishListLinesCount { get; set; }

        public int WishListSharesCount { get; set; }

        public bool IsSharedList { get; set; }

        public string SharedByDisplayName { get; set; }

        public Pagination Pagination { get; set; }

        public IList<WishListLine> WishListLineCollection { get; set; }

        [JsonProperty(PropertyName = "AllowEdit")]
        public bool AllowEditingBySharedWithUsers { get; set; }

        public string ShareOption { get; set; }
    }
}

