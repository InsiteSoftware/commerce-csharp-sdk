﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using CommerceApiSDK.Services.Messages;
using Newtonsoft.Json;

namespace CommerceApiSDK.Services
{
    public class CartService : ServiceBase, ICartService
    {
        private List<AddCartLine> addToCartRequests = new List<AddCartLine>();

        public event EventHandler OnIsAddingToCartSlowChange;
        public event EventHandler OnAddToCartRequestsCountChange;

        private bool isAddingToCartSlow = false;
        public bool IsAddingToCartSlow => isAddingToCartSlow;

        public int AddToCartRequestsCount => addToCartRequests.Count;

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public CartService(
           ICommerceAPIServiceProvider commerceAPIServiceProvider)
            : base(commerceAPIServiceProvider)
        {
            isCartEmpty = true;
            _commerceAPIServiceProvider.GetMessengerService().Subscribe<UserSignedOutOptiMessage>(UserSignedOutHandler);
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

        public async Task<Cart> GetCurrentCart(CartQueryParameters parameters)
        {
            try
            {
                string url = CommerceAPIConstants.CartUri;

                url += parameters.ToQueryString();
                Cart result = await GetAsyncNoCache<Cart>(url);

                IsCartEmpty = result?.CartLines == null || result.CartLines.Count <= 0;

                return result;
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<CartCollectionModel> GetCarts(CartsQueryParameters parameters = null)
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<Cart> GetCart(CartQueryParameters parameters)
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
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
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return false;
            }
        }

        public async Task<CartLine> AddCartLine(AddCartLine cartLine)
        {
            CartLine result = null;
            try
            {
                addToCartRequests.Add(cartLine);
                OnAddToCartRequestsCountChange?.Invoke(this, null);
                MarkCurrentlyAddingCartLinesFlagToTrueIfNeeded();

                StringContent stringContent = await Task.Run(() => SerializeModel(cartLine));
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                result = await PostAsyncNoCache<CartLine>(CommerceAPIConstants.CartLineUrl, stringContent, null, cancellationToken);
            }
            catch (Exception exception) when (!(exception is OperationCanceledException))
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
            }
            finally
            {
                addToCartRequests.Remove(cartLine);
                OnAddToCartRequestsCountChange?.Invoke(this, null);
                MarkCurrentlyAddingCartLinesFlagTоFalseIfPossible();
            }

            return result;
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        public void CancelAllAddCartLineTasks()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        private async void MarkCurrentlyAddingCartLinesFlagToTrueIfNeeded()
        {
            await Task.Delay(CommerceAPIConstants.AddingToCartMillisecondsDelay);

            if (addToCartRequests.Count > 0)
            {
                isAddingToCartSlow = true;
                OnIsAddingToCartSlowChange?.Invoke(this, null);
            }
        }

        [Obsolete("Caution: Will be removed in a future release.")]
        private void MarkCurrentlyAddingCartLinesFlagTоFalseIfPossible()
        {
            if (addToCartRequests.Count == 0)
            {
                isAddingToCartSlow = false;
                OnIsAddingToCartSlowChange?.Invoke(this, null);
            }
        }

        public async Task<CartLine> UpdateCartLine(CartLine cartLine)
        {
            try
            {
                StringContent stringContent = await Task.Run(() => SerializeModel(cartLine));
                return await PatchAsyncNoCache<CartLine>($"{CommerceAPIConstants.CartLineUrl}/{cartLine.Id}", stringContent);
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }

        public async Task<bool> DeleteCartLine(CartLine cartLine)
        {
            try
            {
                HttpResponseMessage result = await DeleteAsync($"{CommerceAPIConstants.CartLineUrl}/{cartLine.Id}");
                return result.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return false;
            }
        }

        public async Task<List<CartLine>> AddCartLineCollection(List<AddCartLine> cartLineCollection)
        {
            try
            {
                JsonSerializerSettings serializationSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                StringContent stringContent = await Task.Run(() => SerializeModel(new { cartLines = cartLineCollection }, serializationSettings));
                CartLineList result = await PostAsyncNoCache<CartLineList>(CommerceAPIConstants.CartLineUrl + "/batch", stringContent);
                return result?.CartLines?.ToList();
            }
            catch (Exception exception)
            {
                _commerceAPIServiceProvider.GetTrackingService().TrackException(exception);
                return null;
            }
        }
    }
}
