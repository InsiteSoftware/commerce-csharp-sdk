using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class ProductQueryParameters : BaseQueryParameters
    {
        public Guid? ProductId { get; set; }

        public Guid? CategoryId { get; set; }

        public bool? ReplaceProducts { get; set; }

        public string UnitOfMeasure { get; set; }

        public double? QtyOrdered { get; set; }

        public bool? AddToRecentlyViewed { get; set; }

        public bool? ApplyPersonalization { get; set; }

        public int? AlsoPurchasedMaxResults { get; set; }

        public bool? IncludeAlternateInventory { get; set; }

        public string IncludeAttributes { get; set; }

        /// <summary>
        /// Options: alsopurchased, warehouses
        /// </summary>
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string Expand { get; set; }

        public List<Guid> Configuration { get; set; }
    }
}
