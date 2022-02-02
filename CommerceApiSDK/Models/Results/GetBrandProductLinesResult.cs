using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetBrandProductLinesResult : BaseModel
    {
        public Pagination Pagination { get; set; }
        public IList<BrandProductLine> ProductLines { get; set; }
    }
}
