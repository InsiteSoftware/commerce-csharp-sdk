using System;

namespace CommerceApiSDK.Models
{
    public class AddCartModel
    {
        public Guid? BillToId { get; set; }

        public Guid? ShipToId { get; set; }

        public string Notes { get; set; }

        public Guid? VmiLocationId { get; set; }
    }
}
