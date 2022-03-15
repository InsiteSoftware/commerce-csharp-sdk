using System.Collections.Generic;

namespace CommerceApiSDK.Models.Results
{
    public class GetProductCollectionResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<Product> Products { get; set; }

        public IList<CategoryFacetDto> CategoryFacets { get; set; }

        public IList<AttributeType> AttributeTypeFacets { get; set; }

        public IList<BrandFacetDto> BrandFacets { get; set; }

        public IList<ProductLineFacetDto> ProductLineFacets { get; set; }

        public IList<SuggestionDto> DidYouMeanSuggestions { get; set; }

        public bool ExactMatch { get; set; }

        public bool NotAllProductsFound { get; set; }

        public bool NotAllProductsAllowed { get; set; }

        public string OriginalQuery { get; set; }

        public string CorrectedQuery { get; set; }

        public object SearchTermRedirectUrl { get; set; }

        // for V2:
        public PriceRange PriceRange { get; set; }
    }
}