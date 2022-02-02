using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Services.Attributes;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IWishListLineService
    {
        Task<WishListLineCollectionModel> GetWishListLines(Guid wishListId, int pageNumber = 1, int pageSize = 16, WishListLineSortOrder sortOrder = WishListLineSortOrder.CustomSort, string searchQuery = null);

        Task<bool> DeleteWishListLine(Guid wishListId, Guid wishListLineId);

        Task<bool> DeleteWishListLineCollection(Guid wishListId, IList<WishListLine> wishListLineCollection);

        Task<WishListLine> UpdateWishListLine(Guid wishListId, WishListLine wishListLine);
    }

    public enum WishListLineSortOrder
    {
        [SortOrder("Custom Sort", "Custom Sort \u2713", "sortorder")]
        CustomSort,

        [SortOrder("Date Added", "Date Added \u2713", "createdon+desc")]
        DateAdded,

        [SortOrder("Product Name", "Product Name \u2193", "product.shortdescription+desc")]
        ProductNameDescending,

        [SortOrder("Product Name", "Product Name \u2191", "product.shortdescription")]
        ProductNameAscending,
    }
}