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

        event PropertyChangedEventHandler IsCartEmptyPropertyChanged;

        Task<Cart> GetCart(Guid cartId, CartQueryParameters parameters);

        Task<Cart> GetAlternateCart(AddCartModel addCartModel);

        Task<Cart> GetCurrentCart(CartQueryParameters parameters);

        Task<Cart> GetRegularCart(CartQueryParameters parameters);        

        Task<GetCartLinesResult> GetCartLines();

        Task<PromotionCollectionModel> GetCurrentCartPromotions();

        Task<PromotionCollectionModel> GetCartPromotions(Guid cartId);

        Task<Promotion> ApplyPromotion(AddPromotion promotion);

        Task<Cart> UpdateCart(Cart cart);

        Task<bool> ClearCart();

        Task<CartLineCollectionDto> AddWishListToCart(Guid wishListId);

        Task<CartCollectionModel> GetCarts(CartsQueryParameters parameters = null);

        Task<bool> DeleteCart(Guid cartId);

        /// <summary>
        /// CartLineService
        /// </summary>
        event EventHandler OnIsAddingToCartSlowChange;
        bool IsAddingToCartSlow { get; }

        event EventHandler OnAddToCartRequestsCountChange;
        int AddToCartRequestsCount { get; }

        Task<CartLine> AddCartLine(AddCartLine cartLine);

        void CancelAllAddCartLineTasks();

        Task<CartLine> UpdateCartLine(CartLine cartLine);

        Task<bool> DeleteCartLine(CartLine cartLine);

        Task<List<CartLine>> AddCartLineCollection(List<AddCartLine> cartLineCollection);
    }
}
