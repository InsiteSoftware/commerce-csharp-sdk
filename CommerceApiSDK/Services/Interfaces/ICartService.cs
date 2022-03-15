using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;

namespace CommerceApiSDK.Services.Interfaces
{
    /// <summary>
    /// A service which manages the shopping cart
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Indicate last known IsCartEmpty state
        /// </summary>
        bool IsCartEmpty { get; set; }

        event PropertyChangedEventHandler IsCartEmptyPropertyChanged;

        Task<Cart> GetCurrentCart(bool getCartlines = false, bool getCostCodes = false, bool getShipping = false, bool getTax = false, bool getCarriers = false, bool getPaymentMethods = false);

        Task<GetCartLinesResult> GetCartLines();

        Task<PromotionCollectionModel> GetCurrentCartPromotions();

        Task<Promotion> ApplyPromotion(AddPromotion promotion);

        Task<Cart> UpdateCart(Cart cart);

        Task<bool> ClearCart();

        Task<CartLineCollectionDto> AddWishListToCart(Guid wishListId);

        Task<CartCollectionModel> GetCarts(CartQueryParameters parameters = null);

        Task<Cart> GetCart(CartDetailQueryParameters parameters);
        Task<bool> DeleteCart(Guid cartId);
    }
}
