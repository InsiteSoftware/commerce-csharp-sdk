using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Enums;
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
}