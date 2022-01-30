namespace CommerceApiSDK.Models.Parameters
{
    using System;
    using System.Collections.Generic;
    using CommerceApiSDK.Attributes;

    public class BaseProductsQueryParameters : BaseQueryParameters
    {
        public override int? Page { get; set; } = 1;
        public override int? PageSize { get; set; } = 16;

        public List<Guid?> ProductIds { get; set; }
        public List<Guid?> BrandIds { get; set; }
        public List<Guid?> ProductLineIds { get; set; }
        public List<Guid?> TopSellersCategoryIds { get; set; }
        public List<Guid?> AttributeValueIds { get; set; }

        public List<string> PriceFilters { get; set; } = null;
        public List<string> Names { get; set; } = null;
        public List<string> ExtendedNames { get; set; } = null;

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;

        public Guid? CategoryId { get; set; }
        public string SearchWithin { get; set; }
        public string IncludeSuggestions { get; set; }
        public string IncludeAttributes { get; set; }
        public string Filter { get; set; }
        public bool ApplyPersonalization { get; set; }
    }

    public class ProductsQueryParameters : BaseProductsQueryParameters
    {
        public List<string> ErpNumbers { get; set; } = null;

        public string Query { get; set; }
        public bool ReplaceProducts { get; set; }
        public bool GetAllAttributeFacets { get; set; }
        public bool IncludeAlternateInventory { get; set; }
        public bool MakeBrandUrls { get; set; }
        public int? TopSellersMaxResults { get; set; }
    }

    public class ProductsQueryV2Parameters : BaseProductsQueryParameters
    {
        public string Search { get; set; }
        public bool IncludeProductsInSubCategories { get; set; }
        public decimal? MinimumPrice { get; set; }
        public decimal? MaximumPrice { get; set; }
        public List<Guid?> TopSellersPersonaIds { get; set; }
        public Guid? CardId { get; set; }
        public bool StockedItemsOnly { get; set; }
        public bool PreviouslyPurchasedProducts { get; set; }
    }
}
