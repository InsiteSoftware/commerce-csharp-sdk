using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetBrandCategoriesResult : BaseModel
    {
        public Pagination Pagination { get; set; }
        public IList<BrandCategory> BrandCategories { get; set; }
    }
}
