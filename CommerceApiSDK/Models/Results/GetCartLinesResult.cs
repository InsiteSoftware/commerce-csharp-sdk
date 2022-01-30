namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetCartLinesResult : BaseModel
    {
        public IList<CartLine> CartLines { get; set; }
    }
}
