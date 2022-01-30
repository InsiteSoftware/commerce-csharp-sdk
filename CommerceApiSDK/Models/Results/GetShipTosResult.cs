namespace CommerceApiSDK.Models.Results
{
    using System.Collections.Generic;

    public class GetShipTosResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<ShipTo> ShipTos { get; set; }
    }
}
