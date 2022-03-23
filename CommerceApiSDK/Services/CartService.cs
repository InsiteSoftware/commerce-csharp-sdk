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

namespace CommerceApiSDK.Services
{
    public class CartService : ServiceBase, ICartService
    {
        private IDisposable token;

        public CartService(
           IOptiAPIBaseServiceProvider optiAPIBaseServiceProvider)
            : base(optiAPIBaseServiceProvider)
        {
            isCartEmpty = true;
            token = _optiAPIBaseServiceProvider.GetMessengerService().Subscribe<UserSignedOutOptiMessage>(UserSignedOutHandler);
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

                string url = CommerceAPIConstants.CartUri + "?" + string.Join("&", parameters);
                Cart result = await GetAsyncNoCache<Cart>(url);

                if (getCartlines)
                {
                    IsCartEmpty = result?.CartLines == null || result.CartLines.Count <= 0;
                }

                return result;
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<GetCartLinesResult> GetCartLines()
        {
            try
            {
                List<string> parameters = new List<string>();

                string url = CommerceAPIConstants.CartLinesUri;
                GetCartLinesResult result = await GetAsyncNoCache<GetCartLinesResult>(url);

                IsCartEmpty = result?.CartLines == null || result.CartLines.Count <= 0;

                return result;
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<PromotionCollectionModel> GetCurrentCartPromotions()
        {
            try
            {
                string url = CommerceAPIConstants.PromotionsUri;
                PromotionCollectionModel result = await GetAsyncNoCache<PromotionCollectionModel>(url);

                return result;
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<Promotion> ApplyPromotion(AddPromotion promotion)
        {
            try
            {
                string url = CommerceAPIConstants.PromotionsUri;
                StringContent stringContent = await Task.Run(() => SerializeModel(promotion));
                Promotion result = await PostAsyncNoCache<Promotion>(url, stringContent);
                return result;
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
            try
            {
                string url = CommerceAPIConstants.CartUri;
                StringContent stringContent = await Task.Run(() => SerializeModel(cart));
                Cart result = await PatchAsyncNoCache<Cart>(url, stringContent);

                return result;
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<bool> ClearCart()
        {
            try
            {
                HttpResponseMessage clearCartResponse = await DeleteAsync(CommerceAPIConstants.CartUri);
                bool result = clearCartResponse != null && clearCartResponse.IsSuccessStatusCode;

                if (result)
                {
                    IsCartEmpty = true;
                }

                return result;
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return false;
            }
        }

        private void UserSignedOutHandler(OptiMessage message)
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
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<CartCollectionModel> GetCarts(CartQueryParameters parameters = null)
        {
            try
            {
                string url = CommerceAPIConstants.CartsUri;

                if (parameters != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                return await GetAsyncNoCache<CartCollectionModel>(url);
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
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

                string url = $"{CommerceAPIConstants.CartsUri}/{parameters.CartId}";

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
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
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
                string url = $"{CommerceAPIConstants.CartsUri}/{cartId}";
                HttpResponseMessage deleteCartResponse = await DeleteAsync(url);
                return deleteCartResponse != null && deleteCartResponse.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                _optiAPIBaseServiceProvider.GetTrackingService().TrackException(exception);
                return false;
            }
        }
    }
}
