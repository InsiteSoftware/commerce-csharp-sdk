using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class VmiLocationsService : ServiceBase, IVmiLocationsService
    {
        private readonly IGooglePlacesService googlePlacesService;

        public VmiLocationsService(
            IClientService clientService,
            INetworkService networkService,
            ITrackingService trackingService,
            ICacheService cacheService,
            IGooglePlacesService googlePlacesService,
            ILoggerService loggerService)
            : base(
                  clientService,
                  networkService,
                  trackingService,
                  cacheService,
                  loggerService)
        {
            this.googlePlacesService = googlePlacesService;
        }

        #region VMI Locations

        public async Task<LatLong> GetPlaceFromAddresss(Address address)
        {
            string fullAddress = $"{address?.Address1} {address?.City} {(address?.State == null ? string.Empty : address?.State.Name)} {address?.PostalCode}";
            try
            {
                GooglePlace result = await googlePlacesService.GetPlace(fullAddress);
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
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}{parameters.ToQueryString()}";
                return await GetAsyncNoCache<GetVmiLocationResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{parameters.VmiLocationId}";

                if (parameters?.Expand != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                VmiLocationModel result = await GetAsyncNoCache<VmiLocationModel>(url);

                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                StringContent stringContent = await Task.Run(() => SerializeModel(model));
                if (model.Id.Equals(Guid.Empty))
                {
                    return await PostAsyncNoCacheWithErrorMessage<VmiLocationModel>(CommerceAPIConstants.VMILocationsUrl, stringContent);
                }
                else
                {
                    string editUrl = $"{CommerceAPIConstants.VMILocationsUrl}/{model.Id}";
                    return await PatchAsyncNoCacheWithErrorMessage<VmiLocationModel>(editUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                string deleteUrl = $"{CommerceAPIConstants.VMILocationsUrl}/{vmiLocationId}";
                return await DeleteAsyncWithErrorMessage<VmiLocationModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{parameters.VmiLocationId}/bins{parameters.ToQueryString()}";
                return await GetAsyncNoCache<GetVmiBinResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{parameters.VmiLocationId}/bins/{parameters.VmiBinId}";

                VmiBinModel result = await GetAsyncNoCache<VmiBinModel>(url);
                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{vmiLocationId}/bins";
                StringContent stringContent = await Task.Run(() => SerializeModel(model));
                if (model.Id.Equals(Guid.Empty))
                {
                    return await PostAsyncNoCacheWithErrorMessage<VmiBinModel>(url, stringContent);
                }
                else
                {
                    string editUrl = $"{url}/{model.Id}";
                    return await PatchAsyncNoCacheWithErrorMessage<VmiBinModel>(editUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                string deleteUrl = $"{CommerceAPIConstants.VMILocationsUrl}/{vmiLocationId}/bins/{vmiBinId}";
                return await DeleteAsyncWithErrorMessage<VmiBinModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{parameters.VmiLocationId}/bins/{parameters.VmiBinId}/bincounts{parameters.ToQueryString()}";
                return await GetAsyncNoCache<GetVmiCountResult>(url);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{parameters.VmiLocationId}/bins/{parameters.VmiBinId}/bincounts/{parameters.VmiCountId}";
                VmiCountModel result = await GetAsyncNoCache<VmiCountModel>(url);
                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{vmiLocationId}/bins/{vmiBinId}/bincounts";
                StringContent stringContent = await Task.Run(() => SerializeModel(model));

                if (model.Id.Equals(Guid.Empty))
                {
                    return await PostAsyncNoCacheWithErrorMessage<VmiCountModel>(url, stringContent);
                }
                else
                {
                    string editUrl = $"{url}/{model.Id}";
                    return await PatchAsyncNoCacheWithErrorMessage<VmiCountModel>(editUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                string deleteUrl = $"{CommerceAPIConstants.VMILocationsUrl}/{vmiLocationId}/bins/{vmiBinId}/bincounts/{vmiCountId}";
                return await DeleteAsyncWithErrorMessage<VmiCountModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{parameters.VmiLocationId}/notes{parameters.ToQueryString()}";
                GetVmiNoteResult result = await GetAsyncNoCache<GetVmiNoteResult>(url);

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{parameters.VmiLocationId}/bins/{parameters.VmiBinId}/notes/{parameters.VmiNoteId}";

                VmiNoteModel result = await GetAsyncNoCache<VmiNoteModel>(url);
                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return result;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{vmiLocationId}/bins/{vmiBinId}/notes";
                StringContent stringContent = await Task.Run(() => SerializeModel(model));
                if (model.Id.Equals(Guid.Empty))
                {
                    return await PostAsyncNoCacheWithErrorMessage<VmiNoteModel>(url, stringContent);
                }
                else
                {
                    string editUrl = $"{url}/{model.Id}";
                    return await PatchAsyncNoCacheWithErrorMessage<VmiNoteModel>(editUrl, stringContent);
                }
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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
                string deleteUrl = $"{CommerceAPIConstants.VMILocationsUrl}/{vmiLocationId}/bins/{vmiBinId}/notes/{vmiNoteId}";
                return await DeleteAsyncWithErrorMessage<VmiNoteModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{parameters.VmiLocationId}/bins{parameters.ToQueryString()}";
                GetVmiBinResult response = await GetAsyncWithCachedResponse<GetVmiBinResult>(url);
                if (response?.VmiBins != null)
                {
                    GetProductCollectionResult result = new GetProductCollectionResult()
                    {
                        Pagination = response.Pagination,
                        Products = new List<Product>(),
                    };

                    foreach (VmiBinModel item in response.VmiBins)
                    {
                        GetProductResult productResult = await GetAsyncWithCachedResponse<GetProductResult>($"{CommerceAPIConstants.ProductsUrl}/{item.ProductId}");
                        if (productResult?.Product != null)
                        {
                            FixProduct(productResult.Product);
                            result.Products.Add(productResult.Product);
                        }
                    }

                    return result;
                }

                return null;
            }
            catch (Exception exception)
            {
                TrackingService.TrackException(exception);
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

                string url = $"{CommerceAPIConstants.VMILocationsUrl}/{parameters.VmiLocationId}/bins{parameters.ToQueryString()}";
                GetVmiBinResult response = await GetAsyncWithCachedResponse<GetVmiBinResult>(url);
                if (response?.VmiBins != null)
                {
                    List<AutocompleteProduct> result = new List<AutocompleteProduct>();

                    foreach (VmiBinModel item in response.VmiBins)
                    {
                        GetProductResult productResult = await GetAsyncWithCachedResponse<GetProductResult>($"{CommerceAPIConstants.ProductsUrl}/{item.ProductId}");
                        if (productResult?.Product != null)
                        {
                            AutocompleteProduct product = new AutocompleteProduct()
                            {
                                Id = productResult.Product.Id.ToString(),
                                Title = productResult.Product.ProductTitle,
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
                TrackingService.TrackException(exception);
                return null;
            }
        }
    }
}