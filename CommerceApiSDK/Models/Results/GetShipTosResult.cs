using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetShipTosResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<ShipTo> ShipTos { get; set; }
    }
}
