using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IWishListLineService
    {
        Task<WishListLineCollectionModel> GetWishListLines(Guid wishListId, WishListLineQueryParameters parameters);

        Task<bool> DeleteWishListLine(Guid wishListId, Guid wishListLineId);

        Task<bool> DeleteWishListLineCollection(Guid wishListId, IList<WishListLine> wishListLineCollection);

        Task<WishListLine> UpdateWishListLine(Guid wishListId, WishListLine wishListLine);
    }
}