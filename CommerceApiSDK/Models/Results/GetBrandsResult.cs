namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetBrandsResult : BaseModel
    {
        public Pagination Pagination { get; set; }
        public IList<Brand> Brands { get; set; }
    }
}