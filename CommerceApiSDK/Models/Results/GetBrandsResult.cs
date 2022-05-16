using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetBrandsResult : BaseModel
    {
        public Pagination Pagination { get; set; }
        public IList<Brand> Brands { get; set; }
    }
}
