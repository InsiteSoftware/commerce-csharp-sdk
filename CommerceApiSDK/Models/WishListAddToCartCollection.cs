using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class WishListAddToCartCollection : BaseModel
    {
        public IList<AddCartLine> WishListLines { get; set; }
    }
}

