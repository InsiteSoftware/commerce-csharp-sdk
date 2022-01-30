namespace CommerceApiSDK.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Models.Parameters;
    using CommerceApiSDK.Models.Results;
    using CommerceApiSDK.Services.Interfaces;

    public class VmiLocationsService : ServiceBase, IVmiLocationsService
    {
        private const string VMILocationsUrl = "/api/v1/vmilocations";
        private const string OrdersUrl = "/api/v1/orders";
        private const string ProductsUrl = "/api/v1/products";

        private readonly IGooglePlacesService googlePlacesService;

        public VmiLocationsService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            IGooglePlacesService googlePlacesService)
            : base(
                  clientService,
                  networkService,
                  trackingService,
                  cacheService)
        {
            this.googlePlacesService = googlePlacesService;
        }

        #region VMI Locations

        public async Task<LatLong> GetPlaceFromAddresss(Address address)
        {
            var fullAddress = $"{address?.Address1} {address?.City} {(address?.State == null ? string.Empty : address?.State.Name)} {address?.PostalCode}";
            try
            {
                var result = await this.googlePlacesService.GetPlace(fullAddress);
                if (result != null)
                {
                    return new LatLong()
                    {
                        Latitude = result.Latitude,
                        Longitude = result.Longitude
                    };
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
            }

            return null;
        }

        public async Task<GetVmiLocationResult> GetVmiLocations(VmiLocationQueryParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                var url = $"{VMILocationsUrl}{parameters.ToQueryString()}";
                return await this.GetAsyncNoCache<GetVmiLocationResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<VmiLocationModel> GetVmiLocation(VmiLocationDetailParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                var url = $"{VMILocationsUrl}/{parameters.VmiLocationId}";

                if (parameters?.Expand != null)
                {
                    var queryString = parameters.ToQueryString();
                    url += queryString;
                }

                var result = await this.GetAsyncNoCache<VmiLocationModel>(url);

                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<VmiLocationModel>> SaveVmiLocation(VmiLocationModel model)
        {
            if (model == null || model.Id == null)
            {
                throw new ArgumentException($"{nameof(model)} is empty");
            }

            try
            {
                var stringContent = await Task.Run(() => SerializeModel(model));
                if (model.Id.Equals(Guid.Empty))
                {
                    return await this.PostAsyncNoCacheWithErrorMessage<VmiLocationModel>(VMILocationsUrl, stringContent);
                }
                else
                {
                    var editUrl = $"{VMILocationsUrl}/{model.Id}";
                    return await this.PatchAsyncNoCacheWithErrorMessage<VmiLocationModel>(editUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<VmiLocationModel>> DeleteVmiLocation(Guid vmiLocationId)
        {
            if (vmiLocationId == null)
            {
                throw new ArgumentException($"{nameof(vmiLocationId)} is empty");
            }

            try
            {
                var deleteUrl = $"{VMILocationsUrl}/{vmiLocationId}";
                return await this.DeleteAsyncWithErrorMessage<VmiLocationModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        #endregion

        #region VMI Bin

        public async Task<GetVmiBinResult> GetVmiBins(VmiBinQueryParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                var url = $"{VMILocationsUrl}/{parameters.VmiLocationId}/vmibins{parameters.ToQueryString()}";
                return await this.GetAsyncNoCache<GetVmiBinResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<VmiBinModel> GetVmiBin(VmiBinDetailParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                var url = $"{VMILocationsUrl}/{parameters.VmiLocationId}/vmibins/{parameters.VmiBinId}";

                var result = await this.GetAsyncNoCache<VmiBinModel>(url);
                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<VmiBinModel>> SaveVmiBin(Guid vmiLocationId, VmiBinModel model)
        {
            if (vmiLocationId.Equals(Guid.Empty) || model == null || model.Id == null)
            {
                throw new ArgumentException($"{nameof(vmiLocationId)} or {nameof(model)} is empty");
            }

            try
            {
                var url = $"{VMILocationsUrl}/{vmiLocationId}/vmibins";
                var stringContent = await Task.Run(() => SerializeModel(model));
                if (model.Id.Equals(Guid.Empty))
                {
                    return await this.PostAsyncNoCacheWithErrorMessage<VmiBinModel>(url, stringContent);
                }
                else
                {
                    var editUrl = $"{url}/{model.Id}";
                    return await this.PatchAsyncNoCacheWithErrorMessage<VmiBinModel>(editUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<VmiBinModel>> DeleteVmiBin(Guid vmiLocationId, Guid vmiBinId)
        {
            if (vmiLocationId == null || vmiBinId == null)
            {
                throw new ArgumentException($"{nameof(vmiLocationId)} or {nameof(vmiBinId)} is empty");
            }

            try
            {
                var deleteUrl = $"{VMILocationsUrl}/{vmiLocationId}/vmibins/{vmiBinId}";
                return await this.DeleteAsyncWithErrorMessage<VmiBinModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        #endregion

        #region VMI Count

        public async Task<GetVmiCountResult> GetBinCounts(VmiCountQueryParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                var url = $"{VMILocationsUrl}/{parameters.VmiLocationId}/vmibins/{parameters.VmiBinId}/bincounts{parameters.ToQueryString()}";
                return await this.GetAsyncNoCache<GetVmiCountResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<VmiCountModel> GetBinCount(VmiCountDetailParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                var url = $"{VMILocationsUrl}/{parameters.VmiLocationId}/vmibins/{parameters.VmiBinId}/bincounts/{parameters.VmiCountId}";
                var result = await this.GetAsyncNoCache<VmiCountModel>(url);
                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<VmiCountModel>> SaveBinCount(Guid vmiLocationId, Guid vmiBinId, VmiCountModel model)
        {
            if (vmiLocationId.Equals(Guid.Empty) || vmiBinId.Equals(Guid.Empty) || model == null)
            {
                throw new ArgumentException($"{nameof(vmiLocationId)} or {nameof(vmiBinId)} or {nameof(model)} is empty");
            }

            try
            {
                var url = $"{VMILocationsUrl}/{vmiLocationId}/vmibins/{vmiBinId}/bincounts";
                var stringContent = await Task.Run(() => SerializeModel(model));

                if (model.Id.Equals(Guid.Empty))
                {
                    return await this.PostAsyncNoCacheWithErrorMessage<VmiCountModel>(url, stringContent);
                }
                else
                {
                    var editUrl = $"{url}/{model.Id}";
                    return await this.PatchAsyncNoCacheWithErrorMessage<VmiCountModel>(editUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<VmiCountModel>> DeleteBinCount(Guid vmiLocationId, Guid vmiBinId, Guid vmiCountId)
        {
            if (vmiLocationId == null || vmiBinId == null || vmiCountId == null)
            {
                throw new ArgumentException($"{nameof(vmiLocationId)} or {nameof(vmiBinId)} or {nameof(vmiCountId)} is empty");
            }

            try
            {
                var deleteUrl = $"{VMILocationsUrl}/{vmiLocationId}/vmibins/{vmiBinId}/bincounts/{vmiCountId}";
                return await this.DeleteAsyncWithErrorMessage<VmiCountModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        #endregion

        #region VMI Note

        public async Task<GetVmiNoteResult> GetVmiLocationNotes(BaseVmiLocationQueryParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                var url = $"{VMILocationsUrl}/{parameters.VmiLocationId}/vminotes{parameters.ToQueryString()}";
                var result = await this.GetAsyncNoCache<GetVmiNoteResult>(url);

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<VmiNoteModel> GetVmiBinNote(VmiNoteDetailParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                var url = $"{VMILocationsUrl}/{parameters.VmiLocationId}/vmibins/{parameters.VmiBinId}/vminotes/{parameters.VmiNoteId}";

                var result = await this.GetAsyncNoCache<VmiNoteModel>(url);
                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<VmiNoteModel>> SaveVmiBinNote(Guid vmiLocationId, Guid vmiBinId, VmiNoteModel model)
        {
            if (vmiLocationId.Equals(Guid.Empty) || vmiBinId.Equals(Guid.Empty) || model == null)
            {
                throw new ArgumentException($"{nameof(vmiLocationId)} or {nameof(vmiBinId)} or {nameof(model)} is empty");
            }

            try
            {
                var url = $"{VMILocationsUrl}/{vmiLocationId}/vmibins/{vmiBinId}/vminotes";
                var stringContent = await Task.Run(() => SerializeModel(model));
                if (model.Id.Equals(Guid.Empty))
                {
                    return await this.PostAsyncNoCacheWithErrorMessage<VmiNoteModel>(url, stringContent);
                }
                else
                {
                    var editUrl = $"{url}/{model.Id}";
                    return await this.PatchAsyncNoCacheWithErrorMessage<VmiNoteModel>(editUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<ServiceResponse<VmiNoteModel>> DeleteVmiBinNote(Guid vmiLocationId, Guid vmiBinId, Guid vmiNoteId)
        {
            if (vmiLocationId == null || vmiBinId == null || vmiNoteId == null)
            {
                throw new ArgumentException($"{nameof(vmiLocationId)} or {nameof(vmiBinId)} or {nameof(vmiNoteId)} is empty");
            }

            try
            {
                var deleteUrl = $"{VMILocationsUrl}/{vmiLocationId}/vmibins/{vmiBinId}/vminotes/{vmiNoteId}";
                return await this.DeleteAsyncWithErrorMessage<VmiNoteModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        #endregion

        // TODO: Is this still needed?
        private void FixProduct(Product product)
        {
            if (product.Pricing == null)
            {
                product.Pricing = new ProductPriceDto();
            }

            if (product.Availability == null)
            {
                product.Availability = new AvailabilityDto();
            }
        }

        public async Task<GetProductCollectionResult> GetProducts(VmiLocationProductParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                if (parameters.VmiLocationId.Equals(Guid.Empty))
                {
                    throw new ArgumentException($"{nameof(parameters.VmiLocationId)} is null");
                }

                var url = $"{VMILocationsUrl}/{parameters.VmiLocationId}/vmibins{parameters.ToQueryString()}";
                var response = await this.GetAsyncWithCachedResponse<GetVmiBinResult>(url);
                if (response?.VmiBins != null)
                {
                    var result = new GetProductCollectionResult
                    {
                        Pagination = response.Pagination,
                        Products = new List<Product>(),
                    };

                    foreach (var item in response.VmiBins)
                    {
                        var productResult = await this.GetAsyncWithCachedResponse<GetProductResult>($"{ProductsUrl}/{item.ProductId}");
                        if (productResult?.Product != null)
                        {
                            this.FixProduct(productResult.Product);
                            result.Products.Add(productResult.Product);
                        }
                    }

                    return result;
                }

                return null;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }

        public async Task<IList<AutocompleteProduct>> GetAutocompleteProducts(VmiLocationProductParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                if (parameters.VmiLocationId.Equals(Guid.Empty))
                {
                    throw new ArgumentException($"{nameof(parameters.VmiLocationId)} is null");
                }

                var url = $"{VMILocationsUrl}/{parameters.VmiLocationId}/vmibins{parameters.ToQueryString()}";
                var response = await this.GetAsyncWithCachedResponse<GetVmiBinResult>(url);
                if (response?.VmiBins != null)
                {
                    var result = new List<AutocompleteProduct>();

                    foreach (var item in response.VmiBins)
                    {
                        var productResult = await this.GetAsyncWithCachedResponse<GetProductResult>($"{ProductsUrl}/{item.ProductId}");
                        if (productResult?.Product != null)
                        {
                            var product = new AutocompleteProduct
                            {
                                Id = productResult.Product.Id.ToString(),
                                Title = productResult.Product.ShortDescription,
                                Image = productResult.Product.MediumImagePath,
                                Name = productResult.Product.Name,
                                ErpNumber = productResult.Product.ERPNumber,
                                Url = productResult.Product.Uri,
                                ManufacturerItemNumber = productResult.Product.ManufacturerItem,
                                BrandName = productResult.Product.Brand?.Name,
                                BrandDetailPagePath = productResult.Product.Brand?.DetailPagePath,
                            };

                            result.Add(product);
                        }
                    }

                    return result;
                }

                return null;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}