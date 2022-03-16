using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Services.Attributes;

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