﻿using System;
using System.Collections.Generic;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class BaseProductQueryParameters : BaseQueryParameters
    {
        public string IncludeAttributes { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string Expand { get; set; }
    }

    public class ProductQueryParameters : BaseProductQueryParameters
    {
        public Guid? CategoryId { get; set; }
        public bool? ReplaceProducts { get; set; }
        public string UnitOfMeasure { get; set; }
        public double? QtyOrdered { get; set; }
        public bool? AddToRecentlyViewed { get; set; }
        public bool? ApplyPersonalization { get; set; }
        public int? AlsoPurchasedMaxResults { get; set; }
        public bool? IncludeAlternateInventory { get; set; }
        public List<Guid> Configuration { get; set; }
    }

    public class ProductQueryV2Parameters : BaseProductQueryParameters
    {
        public Guid? ProductId { get; set; }
        public bool? AddToRecentlyViewed { get; set; }
        public bool? ApplyPersonalization { get; set; }
    }

    public class AlsoPurchasedParameters : BaseProductQueryParameters
    {
        public Guid? ProductId { get; set; }
    }

    public class RelatedProductParameters : BaseProductQueryParameters
    {
        public Guid? ProductId { get; set; }
        public string Relationship { get; set; }
    }

    public class VariantChildrenParameters : BaseProductQueryParameters
    {
        public Guid? ProductId { get; set; }
        public Guid? CategoryId { get; set; }
    }

    public class VariantChildrenDetailParameters : BaseProductQueryParameters
    {
        public Guid? ProductId { get; set; }
        public Guid? VariantChildId { get; set; }
    }

    public class ProductPriceQueryParameters : BaseQueryParameters
    {
        [QueryParameter(QueryOptions.DoNotQuery)]
        public decimal quantity { get; set; }

        [QueryParameter(QueryOptions.DoNotQuery)]
        public string unitOfMeasure { get; set; }

        [QueryParameter(QueryOptions.DoNotQuery)]
        public List<Guid> configuration { get; set; } = null;
    }
}