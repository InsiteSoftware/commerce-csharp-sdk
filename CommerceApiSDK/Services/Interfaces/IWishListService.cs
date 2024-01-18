using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IWishListService
    {
        Task<ServiceResponse<WishListCollectionModel>> GetWishLists(
            WishListsQueryParameters parameters
        );

        Task<ServiceResponse<WishList>> GetWishList(
            Guid wishListId,
            WishListQueryParameters parameters
        );

        Task<bool> DeleteWishList(Guid wishListId);

        Task<ServiceResponse<WishList>> CreateWishList(CreateWishListQueryParameters parameters);

        Task<ServiceResponse<WishList>> CreateWishListWithErrorMessage(
            CreateWishListQueryParameters parameters
        );

        Task<ServiceResponse<WishList>> UpdateWishList(WishList wishList);

        Task<ServiceResponse<WishListLine>> AddProductToWishList(
            Guid wishListId,
            AddCartLine product
        );

        Task<bool> AddWishListLinesToWishList(
            Guid wishListId,
            WishListAddToCartCollection wishListLines
        );

        Task<bool> LeaveWishList(Guid wishListId);

        Task<ServiceResponse<WishListLineCollectionModel>> GetWishListLines(
            Guid wishListId,
            WishListLineQueryParameters parameters
        );

        Task<bool> DeleteWishListLine(Guid wishListId, Guid wishListLineId);

        Task<bool> DeleteWishListLineCollection(
            Guid wishListId,
            IList<WishListLine> wishListLineCollection
        );

        Task<ServiceResponse<WishListLine>> UpdateWishListLine(
            Guid wishListId,
            WishListLine wishListLine
        );

        Task ClearWishListRelatedCacheAsync(Guid wishListId);

        Task ClearGetWishListsCacheAsync();
    }
}
