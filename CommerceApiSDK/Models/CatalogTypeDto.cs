using System.Collections.Generic;
using CommerceApiSDK.Models.Parameters;

namespace CommerceApiSDK.Models
{
    public class CatalogTypeDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class GetCatalogTypeResult : BaseModel
    {
        public Pagination Pagination { get; set; }

        public IList<CatalogTypeDto> Items { get; set; }
    }

    public class CatalogTypeQueryParameters : BaseQueryParameters
    {
        public string Keyword { get; set; }
    }
}