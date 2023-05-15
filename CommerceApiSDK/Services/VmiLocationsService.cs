using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using static Akavache.Sqlite3.Internal.SQLite3;

namespace CommerceApiSDK.Services
{
    public class VmiLocationsService : ServiceBase, IVmiLocationsService
    {
        public VmiLocationsService(
            IClientService ClientService,
            INetworkService NetworkService,
            ITrackingService TrackingService,
            ICacheService CacheService,
            ILoggerService LoggerService
        ) : base(ClientService, NetworkService, TrackingService, CacheService, LoggerService) { }

        #region VMI Locations

        public async Task<ServiceResponse<GetVmiLocationResult>> GetVmiLocations(
            VmiLocationQueryParameters parameters = null
        )
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url = $"{CommerceAPIConstants.VmiLocationsUrl}{parameters.ToQueryString()}";
                return await GetAsyncNoCache<GetVmiLocationResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetVmiLocationResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiLocationModel>> GetVmiLocation(
            VmiLocationDetailParameters parameters = null
        )
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url = $"{CommerceAPIConstants.VmiLocationsUrl}/{parameters.VmiLocationId}";

                if (parameters?.Expand != null)
                {
                    string queryString = parameters.ToQueryString();
                    url += queryString;
                }

                var response = await GetAsyncNoCache<VmiLocationModel>(url);
                VmiLocationModel result = response.Model;

                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiLocationModel>(exception: exception);
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
                    return await PostAsyncNoCacheWithErrorMessage<VmiLocationModel>(
                        CommerceAPIConstants.VmiLocationsUrl,
                        stringContent
                    );
                }
                else
                {
                    string editUrl = $"{CommerceAPIConstants.VmiLocationsUrl}/{model.Id}";
                    return await PatchAsyncNoCacheWithErrorMessage<VmiLocationModel>(
                        editUrl,
                        stringContent
                    );
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiLocationModel>(exception: exception);
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
                string deleteUrl = $"{CommerceAPIConstants.VmiLocationsUrl}/{vmiLocationId}";
                return await DeleteAsyncWithErrorMessage<VmiLocationModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiLocationModel>(exception: exception);
            }
        }

        #endregion

        #region VMI Bin

        public async Task<ServiceResponse<GetVmiBinResult>> GetVmiBins(VmiBinQueryParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{parameters.VmiLocationId}/bins{parameters.ToQueryString()}";
                return await GetAsyncNoCache<GetVmiBinResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetVmiBinResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiBinModel>> GetVmiBin(VmiBinDetailParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{parameters.VmiLocationId}/bins/{parameters.VmiBinId}";

                var response = await GetAsyncNoCache<VmiBinModel>(url);
                VmiBinModel result = response.Model;
                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiBinModel>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiBinModel>> SaveVmiBin(
            Guid vmiLocationId,
            VmiBinModel model
        )
        {
            if (vmiLocationId.Equals(Guid.Empty) || model == null || model.Id == null)
            {
                throw new ArgumentException($"{nameof(vmiLocationId)} or {nameof(model)} is empty");
            }

            try
            {
                string url = $"{CommerceAPIConstants.VmiLocationsUrl}/{vmiLocationId}/bins";
                StringContent stringContent = await Task.Run(() => SerializeModel(model));
                if (model.Id.Equals(Guid.Empty))
                {
                    return await PostAsyncNoCacheWithErrorMessage<VmiBinModel>(url, stringContent);
                }
                else
                {
                    string editUrl = $"{url}/{model.Id}";
                    return await PatchAsyncNoCacheWithErrorMessage<VmiBinModel>(
                        editUrl,
                        stringContent
                    );
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiBinModel>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiBinModel>> DeleteVmiBin(
            Guid vmiLocationId,
            Guid vmiBinId
        )
        {
            if (vmiLocationId == null || vmiBinId == null)
            {
                throw new ArgumentException(
                    $"{nameof(vmiLocationId)} or {nameof(vmiBinId)} is empty"
                );
            }

            try
            {
                string deleteUrl =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{vmiLocationId}/bins/{vmiBinId}";
                return await DeleteAsyncWithErrorMessage<VmiBinModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiBinModel>(exception: exception);
            }
        }

        #endregion

        #region VMI Count

        public async Task<ServiceResponse<GetVmiCountResult>> GetBinCounts(VmiCountQueryParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{parameters.VmiLocationId}/bins/{parameters.VmiBinId}/bincounts{parameters.ToQueryString()}";
                return await GetAsyncNoCache<GetVmiCountResult>(url);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetVmiCountResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiCountModel>> GetBinCount(VmiCountDetailParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{parameters.VmiLocationId}/bins/{parameters.VmiBinId}/bincounts/{parameters.VmiCountId}";

                var response = await GetAsyncNoCache<VmiCountModel>(url);
                VmiCountModel result = response.Model;
                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiCountModel>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiCountModel>> SaveBinCount(
            Guid vmiLocationId,
            Guid vmiBinId,
            VmiCountModel model
        )
        {
            if (vmiLocationId.Equals(Guid.Empty) || vmiBinId.Equals(Guid.Empty) || model == null)
            {
                throw new ArgumentException(
                    $"{nameof(vmiLocationId)} or {nameof(vmiBinId)} or {nameof(model)} is empty"
                );
            }

            try
            {
                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{vmiLocationId}/bins/{vmiBinId}/bincounts";
                StringContent stringContent = await Task.Run(() => SerializeModel(model));

                if (model.Id.Equals(Guid.Empty))
                {
                    return await PostAsyncNoCacheWithErrorMessage<VmiCountModel>(
                        url,
                        stringContent
                    );
                }
                else
                {
                    string editUrl = $"{url}/{model.Id}";
                    return await PatchAsyncNoCacheWithErrorMessage<VmiCountModel>(
                        editUrl,
                        stringContent
                    );
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiCountModel>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiCountModel>> DeleteBinCount(
            Guid vmiLocationId,
            Guid vmiBinId,
            Guid vmiCountId
        )
        {
            if (vmiLocationId == null || vmiBinId == null || vmiCountId == null)
            {
                throw new ArgumentException(
                    $"{nameof(vmiLocationId)} or {nameof(vmiBinId)} or {nameof(vmiCountId)} is empty"
                );
            }

            try
            {
                string deleteUrl =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{vmiLocationId}/bins/{vmiBinId}/bincounts/{vmiCountId}";
                return await DeleteAsyncWithErrorMessage<VmiCountModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiCountModel>(exception: exception);
            }
        }

        #endregion

        #region VMI Note

        public async Task<ServiceResponse<GetVmiNoteResult>> GetVmiLocationNotes(
            BaseVmiLocationQueryParameters parameters = null
        )
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{parameters.VmiLocationId}/notes{parameters.ToQueryString()}";

                var response = await GetAsyncNoCache<GetVmiNoteResult>(url);

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetVmiNoteResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiNoteModel>> GetVmiBinNote(VmiNoteDetailParameters parameters = null)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }

                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{parameters.VmiLocationId}/bins/{parameters.VmiBinId}/notes/{parameters.VmiNoteId}";

                var response = await GetAsyncNoCache<VmiNoteModel>(url);
                VmiNoteModel result = response?.Model;
                if (result == null)
                {
                    throw new Exception("The item requested cannot be found.");
                }

                return response;
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiNoteModel>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiNoteModel>> SaveVmiBinNote(
            Guid vmiLocationId,
            Guid vmiBinId,
            VmiNoteModel model
        )
        {
            if (vmiLocationId.Equals(Guid.Empty) || vmiBinId.Equals(Guid.Empty) || model == null)
            {
                throw new ArgumentException(
                    $"{nameof(vmiLocationId)} or {nameof(vmiBinId)} or {nameof(model)} is empty"
                );
            }

            try
            {
                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{vmiLocationId}/bins/{vmiBinId}/notes";
                StringContent stringContent = await Task.Run(() => SerializeModel(model));
                if (model.Id.Equals(Guid.Empty))
                {
                    return await PostAsyncNoCacheWithErrorMessage<VmiNoteModel>(url, stringContent);
                }
                else
                {
                    string editUrl = $"{url}/{model.Id}";
                    return await PatchAsyncNoCacheWithErrorMessage<VmiNoteModel>(
                        editUrl,
                        stringContent
                    );
                }
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiNoteModel>(exception: exception);
            }
        }

        public async Task<ServiceResponse<VmiNoteModel>> DeleteVmiBinNote(
            Guid vmiLocationId,
            Guid vmiBinId,
            Guid vmiNoteId
        )
        {
            if (vmiLocationId == null || vmiBinId == null || vmiNoteId == null)
            {
                throw new ArgumentException(
                    $"{nameof(vmiLocationId)} or {nameof(vmiBinId)} or {nameof(vmiNoteId)} is empty"
                );
            }

            try
            {
                string deleteUrl =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{vmiLocationId}/bins/{vmiBinId}/notes/{vmiNoteId}";
                return await DeleteAsyncWithErrorMessage<VmiNoteModel>(deleteUrl);
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<VmiNoteModel>(exception: exception);
            }
        }

        #endregion

        // TODO: Is this still needed?
        private void FixProduct(Product product)
        {
            if (product.Pricing == null)
            {
                product.Pricing = new ProductPrice();
            }

            if (product.Availability == null)
            {
                product.Availability = new Availability();
            }
        }

        public async Task<ServiceResponse<GetProductCollectionResult>> GetProducts(
            VmiLocationProductParameters parameters = null
        )
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

                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{parameters.VmiLocationId}/bins{parameters.ToQueryString()}";
                var response = await GetAsyncWithCachedResponse<GetVmiBinResult>(url);
                if (response.Model?.VmiBins != null)
                {
                    GetProductCollectionResult result = new GetProductCollectionResult()
                    {
                        Pagination = response.Model?.Pagination,
                        Products = new List<Product>(),
                    };

                    /// TODO: Individual product responses should also be
                    /// wrapped in ServiceResponse
                    /// skipping it for now
                    var listOfTasks = new List<Task<ServiceResponse<GetProductResult>>>();
                    foreach (VmiBinModel item in response.Model?.VmiBins)
                    {
                        var task = GetAsyncWithCachedResponse<GetProductResult>(
                                $"{CommerceAPIConstants.ProductsUrl}/{item.ProductId}"
                            );
                        listOfTasks.Add(task);
                    }

                    await Task.WhenAll(listOfTasks);

                    foreach (var itemTask in listOfTasks)
                    {
                        var productResult = itemTask.Result;
                        if (productResult.Model?.Product != null)
                        {
                            FixProduct(productResult.Model?.Product);
                            result.Products.Add(productResult.Model?.Product);
                        }
                    }

                    return new ServiceResponse<GetProductCollectionResult>() { 
                        Model = result,
                        Error = response.Error,
                        Exception = response.Exception,
                        StatusCode = response.StatusCode,
                        IsCached = response.IsCached
                    };
                }

                return new ServiceResponse<GetProductCollectionResult>()
                {
                    Model = null,
                    Error = response.Error,
                    Exception = response.Exception,
                    StatusCode = response.StatusCode,
                    IsCached = response.IsCached
                };
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<GetProductCollectionResult>(exception: exception);
            }
        }

        public async Task<ServiceResponse<IList<AutocompleteProduct>>> GetAutocompleteProducts(
            VmiLocationProductParameters parameters = null
        )
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

                string url =
                    $"{CommerceAPIConstants.VmiLocationsUrl}/{parameters.VmiLocationId}/bins{parameters.ToQueryString()}";
                var response = await GetAsyncWithCachedResponse<GetVmiBinResult>(url);
                if (response.Model?.VmiBins != null)
                {
                    List<AutocompleteProduct> result = new List<AutocompleteProduct>();

                    foreach (VmiBinModel item in response.Model?.VmiBins)
                    {
                        var productResult =
                            await GetAsyncWithCachedResponse<GetProductResult>(
                                $"{CommerceAPIConstants.ProductsUrl}/{item.ProductId}"
                            );
                        if (productResult.Model?.Product != null)
                        {
                            AutocompleteProduct product = new AutocompleteProduct()
                            {
                                Id = productResult.Model?.Product.Id.ToString(),
                                Title = productResult.Model?.Product.ProductTitle,
                                Image = productResult.Model?.Product.MediumImagePath,
                                Name = productResult.Model?.Product.Name,
                                ErpNumber = productResult.Model?.Product.ERPNumber,
                                Url = productResult.Model?.Product.Uri,
                                ManufacturerItemNumber = productResult.Model?.Product.ManufacturerItem,
                                BrandName = productResult.Model.Product.Brand?.Name,
                                BrandDetailPagePath = productResult.Model.Product.Brand?.DetailPagePath,
                            };

                            result.Add(product);
                        }
                    }

                    return new ServiceResponse<IList<AutocompleteProduct>>() { 
                        Model = result,
                        Error = response.Error,
                        Exception = response.Exception,
                        StatusCode = response.StatusCode,
                        IsCached = response.IsCached
                    };
                }

                return GetServiceResponse<IList<AutocompleteProduct>>(
                    error: response.Error,
                    exception: response.Exception,
                    statusCode: response.StatusCode,
                    isCached: response.IsCached
                );
            }
            catch (Exception exception)
            {
                this.TrackingService.TrackException(exception);
                return GetServiceResponse<IList<AutocompleteProduct>>(exception: exception);
            }
        }
    }
}
