using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class WebsiteCrosssells : BaseModel
    {
        public IList<Product> Products { get; set; }
    }
}
