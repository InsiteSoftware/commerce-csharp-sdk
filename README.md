# Optimizely API SDK

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

## Installation

Add the `<nuget package name>` to your projects.

This package contains all of the endpoints provided by Optimizely Commerce API. A comprehensive list of services can be found in the swagger document [here](https://mobiledev.insitesandbox.com/swagger/ui/index).

Before you begin using this API, there are a few setup steps that need to be taken care of:
1. Configure the project.
2. Implement additional services.
3. Inject the API Service provider into your class. (How to use)


## 1. Configure the project
To begin, the API SDK needs to know the host url, client secret, and client ID. There is also an option to enabled/disable caching that needs to be configured. You can achieve this several different ways. We provide a IServiceCollection extension method that handles registering all of the needed services, as well as configures the project for you. The other option is to override our `ClientService` class's `CreateClient()` method.

##### IServiceCollection extension
The IServiceCollection extention method registers all the necessary services to your IoC Container, as well as assigns the host, client id and client secret, and can toggle caching on or off. To do this, you'll need to use the provided [ServiceCollectionExtension](https://github.com/InsiteSoftware/commerce-csharp-sdk/blob/develop/CommerceApiSDK/Extensions/ServiceCollectionExtensions.cs)'s extension method `AddComerceSdk(string Host, string ClientID, string ClientSecret, bool IsCachingEnabled)`. 
```sh
yourServiceCollection.AddCommerceSdk("yourHost.url", "yourClientID", "yourClientSecret",  enableCaching)
```
- `host`: The domain url.
- `clientID`: your id <-- where is this found
- `clientSecret`: your access token <-- where is this found
- `isCachingEnabled`: A boolean used to determine if the SDK should load a cached version if service returned an empty response.

Additionally, you will also need to register the services you implement ([required services](#implement-additional-services)) to your IoC Container.
```sh
yourServiceCollection.AddSingleton<ICommerceAPIServiceProvider, YourAPIServiceProviderImp();
yourServiceCollection.AddSingleton<ILocalStorageService, YourLocalStorageImp>();
yourServiceCollection.AddSingleton<ISecureStorageService, YourSecureStorageImp>();
yourServiceCollection.AddSingleton<INetworkService, YourNetworkServiceImp>();
yourServiceCollection.AddSingleton<ITrackingService, YourTrackingServiceImp>();
```

##### Manual Configuration
If you use something other than the IServiceCollection to help with dependency injection, you can manually register the services to whichever container you're using. Below is a list of the services and their implementation class that need to be registered.

```sh
IAccountService, AccountService>();
IAdminAuthenticationService, AdminAuthenticationService
IAdminClientService, AdminClientService>();
IAuthenticationService, AuthenticationService>();
IAutocompleteService, AutocompleteService>();
IBillToService, BillToService>();
IBrandService, BrandService>();
ICacheService, CacheService>();
ICartLineService, CartLineService>();
ICartService, CartService>();
ICatalogpagesService, CatalogpagesService>();
ICategoryService, CategoryService>();
IClientService, ClientService>();
ICommerceAPIServiceProvider, CommerceAPIServiceProvider>();
IDashboardPanelsService, DashboardPanelsService>();
IDealerService, DealerService>();
IInvoiceService, InvoiceService>();
IJobQuoteService, JobQuoteService>();
ILoggerService, DefaultLogger>();
IMessengerService, MessengerService>();
IMobileContentService, MobileContentService>();
IMobileSpireContentService, MobileSpireContentService>();
IOrderService, OrderService>();
IPaymentProfileService, PaymentProfileService>();
IProductService, ProductService>();
IProductV2Service, ProductV2Service>();
IQuoteLineService, QuoteLineService>();
IQuoteService, QuoteService>();
ISessionService, SessionService>();
ISettingsService, SettingsService>();
ITokenExConfigService, TokenExConfigService>();
ITranslationService, TranslationService>();
IVmiLocationsService, VmiLocationsService>();
IWarehouseService, WarehouseService>();
IWebsiteService, WebsiteService>();
IWishListLineService, WishListLineService>();
IWishListService, WishListService>();
ICommerceAPIServiceProvider, CommerceAPIServiceProvider();
```

To config the project to your environment, we recommend to overwrite our `ClientService.CreateClient()` method. In this method, you'll need to assign the Host, ClientId, ClientSecret, and set IsCachingEnabled boolean. This is also where you can configure the http client. An example implementation is show below:

```sh
    public class CommerceClientService : ClientService
    {
        public CommerceClientService(ICommerceAPIServiceProvider commerceAPIServiceProvider)
            : base(commerceAPIServiceProvider)
        {
        }

        public void CreateClient()
        {
            this.Host = "yourHostURL";
            this.ClientId = "yourID";
            this.ClientSecret = "yourSecret";
            ClientConfig.IsCachingEnabled = true;

            base.httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new CookieContainer(),
                ClientCertificateOptions = ClientCertificateOption.Automatic,
            };

            this.client = new HttpClient(new RefreshTokenHandler(base.httpClientHandler, this.RenewAuthenticationTokens, this._commerceAPIServiceProvider.GetLoggerService(), this.NotifyRefreshTokenExpired))
            {
                Timeout = Timeout.InfiniteTimeSpan,
            };
        }
    }
```

A another option if you're not using the IServiceCollection extension method is to call our static `ClientConfig` class's `InitClientConfig(string hostURL, string clientId, string clientSecret, bool isCachingEnabled)` within your start up class, before you register the services to your IoC Container.

```sh
ClientConfig.InitClientConfig("hostURL", "clientId", "clientSecret", isCachingEnabled)
```

##### If Caching is enabled
If you have enabled caching, [Akavache](https://github.com/reactiveui/Akavache) will be required to add to your project. Once added, you'll need to grab an instance of Akavache's `IFilesystemProvider` interface. We recommend using Splat's Locator class to grab this service to register to your IoC Container.

If using IServiceCollection:
`services.AddSingleton(Locator.Current.GetService<IFilesystemProvider>());`


# 2. Implement Additional Services
This project relies on some platform specific services in order to function properly. The interfaces are provided by this API SDK. However, you will need to implement these services yourself.
1. ILocalStorageService
2. ISecureStorageService
3. INetworkService
4. ITrackingService
5. ICommerceAPIServiceProvider

#### ILocalStorageService
A service which manages persitant local storage.

`string Load(string key)`
`string Load(string key, string defaultValue);`
`int LoadInt(string key);`
`void Save(string key, string value);`
`void Save(string key, int value);`
`bool ClearAll();`
`bool Remove(string key);`

#### ISecureStorageService
A service which can persist sensitive data securely. This is used to store session information such as Refresh & Access tokens.

`string Load(string key);`
`bool Save(string key, string value);`
`bool Remove(string key);`
`bool ClearAll();`

#### INetworkService
A service which determines network availability. This is an optional service, used to determine if a network request should be sent or not. If this method returns false, the SDK will attempt to grab a cached version of the response. Otherwise, if true, the SDK will make the request as normal.
_Note: If IsOnline() returns_ `false`_, and if you've configured caching off, then the request will always return_ `null`_._

`bool IsOnline();`

#### ITrackingService
A service for event tracking. This is where you would add your analytic service implementation. If you don't yet have an analytic service, you should still implement this interface, but the methods can be left blank.

`ISessionService SessionService { get; }`
`void Initialize();`
`void TrackEvent(AnalyticsEvent analyticsEvent);`
`void TrackException(Exception exception, Dictionary<string, string> properties = null);`
`void ForceCrash();`
`void SetUserID(string userId);`

#### ICommerceAPIServiceProvider
A service for retrieving the API related services. Although you are able to inject the dependencies you need manually, we've provideded this interface to help condence your class's constructor size. It will be up to you to implement the accessor methods. To do this, you can use IServiceProvider's [GetService(Type)](https://docs.microsoft.com/en-us/dotnet/api/system.iserviceprovider.getservice?view=net-6.0) or if you're using MvvmCross, you can resolve the dependency via: `Mvx.IoCProvider.Resolve<IService>();`
# How to use the SDK
Now that the API SDK has been setup, you're ready to start using it! In order to reduce the number of services you need to inject on your own, we recommend you use the `ICommerceAPIServiceProvider` implementation you created in Step #2. This will be able to provide each service offered by the API SDK. To use it, you'll need to inject this into your class, and then call the appropriate getter method.
```sh
public YourClass(ICommerceAPIServiceProvider commerceAPIServiceProvider)
{
    public void DoSomething()
    {
        var result = await commerceAPIServiceProvider.GetAccountService().GetAccountsAsync();
        // Handle result
    }
}
```

There are some service methods that will require you to provide values for the query parameters. We offer [QueryParameter objects](https://github.com/InsiteSoftware/commerce-csharp-sdk/tree/develop/CommerceApiSDK/Models/Parameters) that can assit you in this area. 
```sh
var parameters = new BillTosQueryParameters
    {
        Filter = this.CurrentSearchText,
        Page = this.currentPage,
        Exclude = new List<string> { "excludeshowall" },
    };

var billToResult = await commerceAPIServiceProvider.GetBillToService().GetBillTosAsync(parameters);
```
