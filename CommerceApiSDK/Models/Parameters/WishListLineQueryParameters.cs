using CommerceApiSDK.Attributes;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services.Attributes;

namespace CommerceApiSDK.Models.Parameters
{
    public class WishListLineQueryParameters : BaseQueryParameters
    {
        public string Query { get; set; }

        public int? DefaultPageSize { get; set; }

        public string ChangedSharedListLinesQuantities { get; set; }

        [QueryParameter(QueryOptions.DoNotEncode)]
        public override string Sort { get; set; } = SortOrderAttribute.GetSortOrderValue(WishListLineSortOrder.CustomSort);
    }
}
