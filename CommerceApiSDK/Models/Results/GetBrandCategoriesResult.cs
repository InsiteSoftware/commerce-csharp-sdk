namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetBrandCategoriesResult : BaseModel
    {
        public Pagination Pagination { get; set; }
        public IList<BrandCategory> BrandCategories { get; set; }
    }
}
