namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetBrandProductLinesResult : BaseModel
    {
        public Pagination Pagination { get; set; }
        public IList<BrandProductLine> ProductLines { get; set; }
    }
}
