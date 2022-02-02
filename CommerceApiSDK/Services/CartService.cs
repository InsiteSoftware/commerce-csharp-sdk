using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;
using MvvmCross.Plugin.Messenger;

namespace CommerceApiSDK.Services
{
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
            isCartEmpty = true;
            token = this.messenger.Subscribe<UserSignedOutMessage>(UserSignedOutHandler);
        }

        public event PropertyChangedEventHandler IsCartEmptyPropertyChanged;
        private bool isCartEmpty;
        public bool IsCartEmpty
        {
            get => isCartEmpty;
            set
            {
                if (isCartEmpty != value)
                {
                    isCartEmpty = value;
                    IsCartEmptyPropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCartEmpty")); // Raise the event
                }
            }
        }

        public async Task<Cart> GetCurrentCart(bool getCartlines = false, bool getCostCodes = false, bool getShipping = false, bool getTax = false, bool getCarriers = false, bool getPaymentMethods = false)
        {
            try
            {
                List<string> parameters = new List<string>();
                List<string> expandParameters = new List<string>();

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

                string url = CartUri + "?" + string.Join("&", parameters);
                Cart result = await GetAsyncNoCache<Cart>(url);

                if (getCartlines)
                {
                    IsCartEmpty = result?.CartLines == null || result.CartLines.Count <= 0;
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<GetCartLinesResult> GetCartLines()
        {
            try
            {
                List<string> parameters = new List<string>();

                string url = CartLinesUri;
                GetCartLinesResult result = await GetAsyncNoCache<GetCartLinesResult>(url);

                IsCartEmpty = result?.CartLines == null || result.CartLines.Count <= 0;

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<PromotionCollectionModel> GetCurrentCartPromotions()
        {
            try
            {
                string url = PromotionsUri;
                PromotionCollectionModel result = await GetAsyncNoCache<PromotionCollectionModel>(url);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Promotion> ApplyPromotion(AddPromotion promotion)
        {
            try
            {
                string url = PromotionsUri;
                StringContent stringContent = await Task.Run(() => SerializeModel(promotion));
                Promotion result = await PostAsyncNoCache<Promotion>(url, stringContent);
                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
            try
            {
                string url = CartUri;
                StringContent stringContent = await Task.Run(() => SerializeModel(cart));
                Cart result = await PatchAsyncNoCache<Cart>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> ClearCart()
        {
            try
            {
                HttpResponseMessage clearCartResponse = await DeleteAsync(CartUri);
                bool result = clearCartResponse != null && clearCartResponse.IsSuccessStatusCode;

                if (result)
                {
                    IsCartEmpty = true;
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return false;
            }
        }

        private void UserSignedOutHandler(MvxMessage message)
        {
            IsCartEmpty = true;
        }

        public async Task<CartLineCollectionDto> AddWishListToCart(Guid wishListId)
        {
            try
            {
                return await PostAsyncNoCache<CartLineCollectionDto>("api/v1/carts/current/cartlines/wishlist/" + wishListId, null);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<CartCollectionModel> GetCarts(CartQueryParameters parameters = null)
        {
            try
            {
                string url = CartsUri;

                if (parameters != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                return await GetAsyncNoCache<CartCollectionModel>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CartsUri}/{parameters.CartId}";

                if (parameters?.Expand != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                Cart result = await GetAsyncNoCache<Cart>(url);
                if (result == null)
                {
                    throw new Exception("The cart requested cannot be found.");
                }

                if (parameters?.Expand != null && parameters.Expand.Exists(p => p.Equals("cartlines", StringComparison.OrdinalIgnoreCase)))
                {
                    IsCartEmpty = result?.CartLines == null || result.CartLines.Count <= 0;
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                string url = $"{CartsUri}/{cartId}";
                HttpResponseMessage deleteCartResponse = await DeleteAsync(url);
                return deleteCartResponse != null && deleteCartResponse.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
                return false;
            }
        }
    }
}
