using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IWishListService
    {
        Task<WishListCollectionModel> GetWishLists();

        Task<WishList> GetWishList(Guid wishListId, WishListQueryParameters parameters);

        Task<bool> DeleteWishList(Guid wishListId);

        Task<WishList> CreateWishList(CreateWishListQueryParameters parameters);

        Task<ServiceResponse<WishList>> CreateWishListWithErrorMessage(
            CreateWishListQueryParameters parameters
        );

        Task<WishList> UpdateWishList(WishList wishList);

        Task<WishListLine> AddProductToWishList(Guid wishListId, AddCartLine product);

        Task<bool> AddWishListLinesToWishList(
            Guid wishListId,
            WishListAddToCartCollection wishListLines
        );

        Task<bool> LeaveWishList(Guid wishListId);

        Task<WishListLineCollectionModel> GetWishListLines(
            Guid wishListId,
            WishListLineQueryParameters parameters
        );

        Task<bool> DeleteWishListLine(Guid wishListId, Guid wishListLineId);

        Task<bool> DeleteWishListLineCollection(
            Guid wishListId,
            IList<WishListLine> wishListLineCollection
        );

        Task<WishListLine> UpdateWishListLine(Guid wishListId, WishListLine wishListLine);

        Task ClearWishListRelatedCacheAsync(Guid wishListId);

        Task ClearGetWishListsCacheAsync();
    }
}
