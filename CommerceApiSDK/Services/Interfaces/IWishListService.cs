using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services.Attributes;
using Newtonsoft.Json;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface IWishListService
    {
        Task<WishListCollectionModel> GetWishLists(int pageNumber = 1, int pageSize = 16, WishListSortOrder sortOrder = WishListSortOrder.ModifiedOnDescending, string searchText = null);

        Task<WishList> GetWishList(Guid wishListId);

        Task<bool> DeleteWishList(Guid wishListId);

        Task<WishList> CreateWishList(string wishListName, string description = "");

        Task<ServiceResponse<WishList>> CreateWishListWithErrorMessage(string wishListName, string description = "");

        Task<WishList> UpdateWishList(WishList wishList);

        Task<WishListLine> AddProductToWishList(Guid wishListId, AddCartLine product);

        Task<bool> AddWishListLinesToWishList(Guid wishListId, WishListAddToCartCollection wishListLines);

        Task<bool> LeaveWishList(Guid wishListId);
    }

    public class WishListCollectionModel : BaseModel
    {
        public IList<WishList> WishListCollection { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class WishList : BaseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool CanAddAllToCart { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedByDisplayName { get; set; }

        public int WishListLinesCount { get; set; }

        public int WishListSharesCount { get; set; }

        public bool IsSharedList { get; set; }

        public string SharedByDisplayName { get; set; }

        public Pagination Pagination { get; set; }

        public IList<WishListLine> WishListLineCollection { get; set; }

        [JsonProperty(PropertyName = "AllowEdit")]
        public bool AllowEditingBySharedWithUsers { get; set; }

        public string ShareOption { get; set; }
    }

    public class WishListLineCollectionModel
    {
        public IList<WishListLine> WishListLines { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class WishListLine : BaseModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string SmallImagePath { get; set; }

        public string ManufacturerItem { get; set; }

        public string CustomerName { get; set; }

        public string ShortDescription { get; set; }

        public decimal QtyOrdered { get; set; }

        public string ERPNumber { get; set; }

        public ProductPriceDto Pricing { get; set; }

        public bool IsActive { get; set; }

        public bool CanEnterQuantity { get; set; }

        public bool CanAddToCart { get; set; }

        public AvailabilityDto Availability { get; set; }

        public string UnitOfMeasure { get; set; }

        public IList<ProductUnitOfMeasure> ProductUnitOfMeasures { get; set; }

        public string Notes { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDiscontinued { get; set; }

        public bool TrackInventory { get; set; }

        public int SortOrder { get; set; }

        public Brand Brand { get; set; }

        public bool QuoteRequired { get; set; }
    }

    public class WishListAddToCartCollection : BaseModel
    {
        public IList<AddCartLine> WishListLines { get; set; }
    }
}