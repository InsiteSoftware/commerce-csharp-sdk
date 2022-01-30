namespace CommerceApiSDK.Models.Parameters
{
    using System.Collections.Generic;
    using CommerceApiSDK.Attributes;

    public class BrandsQueryParameters : BaseQueryParameters
    {
        public string StartsWith { get; set; }
        public string Manufacturer { get; set; }
        public override int? PageSize { get => 500; set => base.PageSize = value; }
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }

    public class BrandQueryParameters : BaseQueryParameters
    {
        [QueryParameter(queryType: QueryListParameterType.CommaSeparated)]
        public List<string> Expand { get; set; } = null;
    }
}
