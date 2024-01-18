using System;
using System.Collections.Generic;
using CommerceApiSDK.Models;

namespace CommerceApiSDK.Models
{
    public class Warehouse : Availability
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string ContactName { get; set; }

        public Guid? CountryId { get; set; }

        public DateTimeOffset? DeactivateOn { get; set; }

        public string Description { get; set; }

        public string Phone { get; set; }

        public string PostalCode { get; set; }

        public string ShipSite { get; set; }

        public string State { get; set; }

        public bool IsDefault { get; set; }

        public IList<Warehouse> AlternateWarehouses { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Hours { get; set; }

        public double Distance { get; set; }

        public bool AllowPickup { get; set; }

        public Guid? PickupShipViaId { get; set; }
    }
}
