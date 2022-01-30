namespace CommerceApiSDK.Test.TestHelpers
{
    using CommerceApiSDK.Services.Attributes;

    public enum TestEnumSortOrder
    {
        [SortOrder("Best Match", "Best Match \u2713", "1")]
        BestMatch,

        [SortOrder("Name", "Name \u2193", "3")]
        NameDescending,

        [SortOrder("Name", "Name \u2191", "2")]
        NameAscending,

        [SortOrder("Price", "Price \u2193", "5")]
        PriceDescending,

        [SortOrder("Price", "Price \u2191", "4")]
        PriceAscending
    }
}