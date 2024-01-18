using System;
using System.Collections.Generic;
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

        int CartItemCount { get; set; }

        event PropertyChangedEventHandler IsCartEmptyPropertyChanged;

        event PropertyChangedEventHandler IsCartCountPropertyChanged;

        Task<ServiceResponse<Cart>> GetCart(Guid cartId, CartQueryParameters parameters);

        /// <summary>
        /// This will create an Alternate Cart as opposed to regular cart, by setting AlternateCart cookie in CookieCollection
        /// Any subsequent call to GetCurrentCart will give AlternateCart
        /// To get back to regular cart call GetRegularCart
        /// Or Remove cookie by calling RemoveAlternateCartCookie from IClientService
        /// </summary>
        Task<ServiceResponse<Cart>> CreateAlternateCart(AddCartModel addCartModel);

        Task<ServiceResponse<Cart>> GetCurrentCart(CartQueryParameters parameters);

        Task<ServiceResponse<Cart>> GetRegularCart(CartQueryParameters parameters);

        Task<ServiceResponse<GetCartLinesResult>> GetCartLines();

        Task<ServiceResponse<PromotionCollectionModel>> GetCurrentCartPromotions();

        Task<ServiceResponse<PromotionCollectionModel>> GetCartPromotions(Guid cartId);

        Task<ServiceResponse<Promotion>> ApplyPromotion(AddPromotion promotion);

        Task<ServiceResponse<Cart>> UpdateCart(Cart cart);

        Task<bool> ClearCart();

        Task<ServiceResponse<CartLineCollectionDto>> AddWishListToCart(Guid wishListId);

        Task<ServiceResponse<CartCollectionModel>> GetCarts(CartsQueryParameters parameters = null);

        Task<bool> DeleteCart(Guid cartId);

        Task<ServiceResponse<Cart>> ApproveCart(Cart cart);

        /// <summary>
        /// CartLineService
        /// </summary>
        event EventHandler OnIsAddingToCartSlowChange;
        bool IsAddingToCartSlow { get; }

        event EventHandler OnAddToCartRequestsCountChange;
        int AddToCartRequestsCount { get; }

        Task<ServiceResponse<CartLine>> AddCartLine(AddCartLine cartLine);

        void CancelAllAddCartLineTasks();

        Task<ServiceResponse<CartLine>> UpdateCartLine(CartLine cartLine);

        Task<bool> DeleteCartLine(CartLine cartLine);

        Task<ServiceResponse<List<CartLine>>> AddCartLineCollection(
            List<AddCartLine> cartLineCollection
        );
    }
}
