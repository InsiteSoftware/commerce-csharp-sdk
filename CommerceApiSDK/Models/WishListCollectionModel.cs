using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class WishListCollectionModel : BaseModel
    {
        public IList<WishList> WishListCollection { get; set; }

        public Pagination Pagination { get; set; }
    }
}

