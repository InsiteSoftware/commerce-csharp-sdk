using System;
using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class CategoryFacet
    {
        public Guid CategoryId { get; set; }

        public Guid WebsiteId { get; set; }

        public string ShortDescription { get; set; }

        public int Count { get; set; }

        public bool Selected { get; set; }

        public IList<CategoryFacet> SubCategoryDtos { get; set; }
    }
}
