namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using CommerceApiSDK.Models;
    using CommerceApiSDK.Services.Interfaces;

    using Newtonsoft.Json;

    public class CartLineService : ServiceBase, ICartLineService
    {
        private const string CartLineUrl = "api/v1/carts/current/cartlines";
        private const int AddingToCartMillisecondsDelay = 5000;
        private List<AddCartLine> addToCartRequests = new List<AddCartLine>();

        public event EventHandler OnIsAddingToCartSlowChange;
        public event EventHandler OnAddToCartRequestsCountChange;

        private bool isAddingToCartSlow = false;
        public bool IsAddingToCartSlow
        {
            get
            {
                return this.isAddingToCartSlow;
            }
        }

        public int AddToCartRequestsCount
        {
            get
            {
                return this.addToCartRequests.Count;
            }
        }

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public CartLineService(IClientService clientService, INetworkService networkService, ITrackingService trackingService, ICacheService cacheService)
            : base(clientService, networkService, trackingService, cacheService)
        {
        }

        public async Task<CartLine> AddCartLine(AddCartLine cartLine)
        {
            CartLine result = null;
            try
            {
                this.addToCartRequests.Add(cartLine);
                this.OnAddToCartRequestsCountChange?.Invoke(this, null);
                this.MarkCurrentlyAddingCartLinesFlagToTrueIfNeeded();

                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(cartLine));
                var cancellationToken = this.cancellationTokenSource.Token;

                result = await this.PostAsyncNoCache<CartLine>(CartLineUrl, stringContent, null, cancellationToken);
            }
            catch (Exception exception) when (!(exception is OperationCanceledException))
            {
                this.TrackingService.TrackException(exception);
            }
            finally
            {
                this.addToCartRequests.Remove(cartLine);
                this.OnAddToCartRequestsCountChange?.Invoke(this, null);
                this.MarkCurrentlyAddingCartLinesFlagTоFalseIfPossible();
            }

            return result;
        }

        public void CancelAllAddCartLineTasks()
        {
            this.cancellationTokenSource?.Cancel();
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        private async void MarkCurrentlyAddingCartLinesFlagToTrueIfNeeded()
        {
            await Task.Delay(AddingToCartMillisecondsDelay);

            if (this.addToCartRequests.Count > 0)
            {
                this.isAddingToCartSlow = true;
                this.OnIsAddingToCartSlowChange?.Invoke(this, null);
            }
        }

        private void MarkCurrentlyAddingCartLinesFlagTоFalseIfPossible()
        {
            if (this.addToCartRequests.Count == 0)
            {
                this.isAddingToCartSlow = false;
                this.OnIsAddingToCartSlowChange?.Invoke(this, null);
            }
        }

        public async Task<CartLine> UpdateCartLine(CartLine cartLine)
        {
            try
            {
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(cartLine));
                return await this.PatchAsyncNoCache<CartLine>($"{CartLineUrl}/{cartLine.Id}", stringContent);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<bool> DeleteCartLine(CartLine cartLine)
        {
            try
            {
                var result = await this.DeleteAsync($"{CartLineUrl}/{cartLine.Id}");
                return result.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return false;
            }
        }

        public async Task<List<CartLine>> AddCartLineCollection(List<AddCartLine> cartLineCollection)
        {
            try
            {
                var serializationSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                var stringContent = await Task.Run(() => ServiceBase.SerializeModel(new { cartLines = cartLineCollection }, serializationSettings));
                var result = await this.PostAsyncNoCache<CartLineList>(CartLineUrl + "/batch", stringContent);
                return result?.CartLines?.ToList();
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}
