using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class WishListLineCollectionModel
    {
        public IList<WishListLine> WishListLines { get; set; }

        public Pagination Pagination { get; set; }
    }
}

