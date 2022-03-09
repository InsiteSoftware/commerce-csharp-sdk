using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;
using Newtonsoft.Json;

namespace CommerceApiSDK.Services
{
    public class CartLineService : ServiceBase, ICartLineService
    {
        private List<AddCartLine> addToCartRequests = new List<AddCartLine>();

        public event EventHandler OnIsAddingToCartSlowChange;
        public event EventHandler OnAddToCartRequestsCountChange;

        private bool isAddingToCartSlow = false;
        public bool IsAddingToCartSlow => isAddingToCartSlow;

        public int AddToCartRequestsCount => addToCartRequests.Count;

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public CartLineService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService, ILoggerService loggerService)
            : base(clientService, networkService, trackingService, cacheService, loggerService)
        {
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
                TrackingService.TrackException(exception);
            }
            finally
            {
                addToCartRequests.Remove(cartLine);
                OnAddToCartRequestsCountChange?.Invoke(this, null);
                MarkCurrentlyAddingCartLinesFlagTоFalseIfPossible();
            }

            return result;
        }

        public void CancelAllAddCartLineTasks()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
        }

        private async void MarkCurrentlyAddingCartLinesFlagToTrueIfNeeded()
        {
            await Task.Delay(CommerceAPIConstants.AddingToCartMillisecondsDelay);

            if (addToCartRequests.Count > 0)
            {
                isAddingToCartSlow = true;
                OnIsAddingToCartSlowChange?.Invoke(this, null);
            }
        }

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
                TrackingService.TrackException(exception);
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
                TrackingService.TrackException(exception);
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
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
