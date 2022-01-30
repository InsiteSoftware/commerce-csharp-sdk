namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;
    using CommerceApiSDK.Services.Messages;
    using MvvmCross.Plugin.Messenger;

    public class CartService : ServiceBase, ICartService
    {
        private const string CartUri = "/api/v1/carts/current";
        private const string CartLinesUri = "/api/v1/carts/current/cartlines";
        private const string PromotionsUri = "/api/v1/carts/current/promotions";
        private const string CartsUri = "/api/v1/carts";

        private MvxSubscriptionToken token;
        private readonly IMvxMessenger messenger;

        public CartService(
            IClientService clientService,
            IMvxMessenger messenger,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
            this.messenger = messenger;
            this.isCartEmpty = true;
            this.token = this.messenger.Subscribe<UserSignedOutMessage>(this.UserSignedOutHandler);
        }

        public event PropertyChangedEventHandler IsCartEmptyPropertyChanged;
        private bool isCartEmpty;
        public bool IsCartEmpty
        {
            get => this.isCartEmpty;
            set
            {
                if (this.isCartEmpty != value)
                {
                    this.isCartEmpty = value;
                    this.IsCartEmptyPropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCartEmpty")); // Raise the event
                }
            }
        }

        public async Task<Cart> GetCurrentCart(bool getCartlines = false, bool getCostCodes = false, bool getShipping = false, bool getTax = false, bool getCarriers = false, bool getPaymentMethods = false)
        {
            try
            {
                var parameters = new List<string>();
                var expandParameters = new List<string>();

                if (getCartlines)
                {
                    expandParameters.Add("cartlines");
                }

                if (getCostCodes)
                {
                    expandParameters.Add("costcodes");
                }

                if (getShipping)
                {
                    expandParameters.Add("shipping");
                }

                if (getTax)
                {
                    expandParameters.Add("tax");
                }

                if (getCarriers)
                {
                    expandParameters.Add("carriers");
                }

                if (getPaymentMethods)
                {
                    expandParameters.Add("paymentoptions");
                }

                if (expandParameters.Count > 0)
                {
                    parameters.Add("expand=" + string.Join(",", expandParameters));
                }

                var url = CartUri + "?" + string.Join("&", parameters);
                var result = await this.GetAsyncNoCache<Cart>(url);

                if (getCartlines)
                {
                    this.IsCartEmpty = result?.CartLines == null || result.CartLines.Count <= 0;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetCartLinesResult> GetCartLines()
        {
            try
            {
                var parameters = new List<string>();

                var url = CartLinesUri;
                var result = await this.GetAsyncNoCache<GetCartLinesResult>(url);

                this.IsCartEmpty = result?.CartLines == null || result.CartLines.Count <= 0;

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<PromotionCollectionModel> GetCurrentCartPromotions()
        {
            try
            {
                var url = PromotionsUri;
                var result = await this.GetAsyncNoCache<PromotionCollectionModel>(url);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Promotion> ApplyPromotion(AddPromotion promotion)
        {
            try
            {
                var url = PromotionsUri;
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(promotion));
                var result = await this.PostAsyncNoCache<Promotion>(url, stringContent);
                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
            try
            {
                var url = CartUri;
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(cart));
                var result = await this.PatchAsyncNoCache<Cart>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> ClearCart()
        {
            try
            {
                var clearCartResponse = await this.DeleteAsync(CartUri);
                var result = clearCartResponse != null && clearCartResponse.IsSuccessStatusCode;

                if (result)
                {
                    this.IsCartEmpty = true;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return false;
            }
        }

        private void UserSignedOutHandler(MvxMessage message)
        {
            this.IsCartEmpty = true;
        }

        public async Task<CartLineCollectionDto> AddWishListToCart(Guid wishListId)
        {
            try
            {
                return await this.PostAsyncNoCache<CartLineCollectionDto>("api/v1/carts/current/cartlines/wishlist/" + wishListId, null);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<CartCollectionModel> GetCarts(CartQueryParameters parameters = null)
        {
            try
            {
                var url = CartsUri;

                if (parameters != null)
                {
                    var queryString = parameters.ToQueryString();
                    url += queryString;
                }

                return await this.GetAsyncNoCache<CartCollectionModel>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Cart> GetCart(CartDetailQueryParameters parameters)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                if (parameters.CartId.Equals(Guid.Empty))
                {
                    throw new ArgumentException($"{nameof(parameters.CartId)} is empty");
                }

                var url = $"{CartsUri}/{parameters.CartId}";

                if (parameters?.Expand != null)
                {
                    var queryString = parameters.ToQueryString();
                    url += queryString;
                }

                var result = await this.GetAsyncNoCache<Cart>(url);
                if (result == null)
                {
                    throw new Exception("The cart requested cannot be found.");
                }

                if (parameters?.Expand != null && parameters.Expand.Exists(p => p.Equals("cartlines", StringComparison.OrdinalIgnoreCase)))
                {
                    this.IsCartEmpty = result?.CartLines == null || result.CartLines.Count <= 0;
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> DeleteCart(Guid cartId)
        {
            if (cartId.Equals(Guid.Empty))
            {
                throw new ArgumentException($"{nameof(cartId)} is empty");
            }

            try
            {
                var url = $"{CartsUri}/{cartId}";
                var deleteCartResponse = await this.DeleteAsync(url);
                return deleteCartResponse != null && deleteCartResponse.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return false;
            }
        }
    }
}
