using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetCartLinesResult : BaseModel
    {
        public IList<CartLine> CartLines { get; set; }
    }
}
