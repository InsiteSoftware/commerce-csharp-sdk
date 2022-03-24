using System;
using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Models.Parameters
{
    public class ProductQueryV2Parameters : BaseQueryParameters
    {
        public Guid? ProductId { get; set; }

        public bool? AddToRecentlyViewed { get; set; }

        public bool? ApplyPersonalization { get; set; }

        public string IncludeAttributes { get; set; }

        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public string Expand { get; set; }
    }
}

