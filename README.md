# Optimizely B2C Commerce API SDK
[11:09 AM] Lee Anderson
[![Apache 2.0](https://img.shields.io/github/license/nebula-plugins/gradle-extra-configurations-plugin.svg)](http://www.apache.org/licenses/LICENSE-2.0)
[![NuGet](https://img.shields.io/nuget/v/Optimizely.SDK.svg?style=plastic)](https://www.nuget.org/packages/Optimizely.SDK/)


## About the SDK
Optimizely B2B Commerce API SDK is a .NET based Nuget package that provides a C# API wrapper for to the B2B Commerce API. This allows developers to interact with the B2C platform API through an object-oriented interface, rather than making direct HTTP calls to the API. In addition to ease of use, the API SDK package abstracts the API so that updates to the underlying API should not break solutions built off of the SDK.

Employing this API SDK enables developers to pull data out of the B2B Commerce system into any C# based application that needs to interact with the B2B Commerce Cloud platform. This enables rapid development of application such as mobile apps built with the Xamarin-based B2B Commerce Mobile App SDK and cross-product integrations, such as B2B-Content Cloud CMS integration.

This package contains all of the endpoints provided by Optimizely Commerce API. A comprehensive list of services can be found in the swagger API documentation [here](https://mobiledev.insitesandbox.com/swagger/ui/index).

## Installation
The API SDK is delivered as a Nuget package that can be imported by adding the `Optimizely.Commerce.API` to your projects. For more information on the package, see the [Nuget package description](https://www.nuget.org/packages/Optimizely.Commerce.API/)

Before using the API SDK, you will need to perform a few setup steps:

1. Configure the project.
2. Implement additional services.
3. Inject the API Service provider into your class.

## 1. Configure the project
Before it can communicate with the B2B Commerce API, the API SDK needs to know the host url, client secret, and client ID of your B2B instance. You also need to enable/disable caching via the package configuration.

You can configure the package several ways. We provide an `IServiceCollection` extension that registers all of the needed services, as well as configuring the project for you. Another option is to override our `ClientService` class's `CreateClient()` manually with your own custom code.

##### IServiceCollection extension
The `IServiceCollection` extension method registers all the necessary services to your IoC Container, as well as assigning the host, client id and client secret, and can toggle caching on or off. To configure the SDK this way, call the provided [ServiceCollectionExtension](https://github.com/InsiteSoftware/commerce-csharp-sdk/blob/develop/CommerceApiSDK/Extensions/ServiceCollectionExtensions.cs)'s extension method, `AddComerceSdk(string Host, string ClientID, string ClientSecret, bool IsCachingEnabled)`.

```sh
yourServiceCollection.AddCommerceSdk("yourHost.url", "yourClientID", "yourClientSecret",  enableCaching)
```
Substitute the following variables with the appropriate values for your context:
- `host`: The domain url.
- `clientID`: Your ID
- `clientSecret`: Your Access Token
- `isCachingEnabled`: A boolean used to determine if the SDK should load a cached version if the service returns an empty response.

You will also need to register the services you implement ([required services](#implement-additional-services)) to your IoC Container:

```sh
yourServiceCollection.AddSingleton<ICommerceAPIServiceProvider, YourAPIServiceProviderImp();
yourServiceCollection.AddSingleton<ILocalStorageService, YourLocalStorageImp>();
yourServiceCollection.AddSingleton<ISecureStorageService, YourSecureStorageImp>();
yourServiceCollection.AddSingleton<INetworkService, YourNetworkServiceImp>();
yourServiceCollection.AddSingleton<ITrackingService, YourTrackingServiceImp>();
```

##### Manual Configuration
If you use something other than the `IServiceCollection` to help with dependency injection, you can manually register the services to whichever container you are using. Below is a list of the services and their respective implementation classes that need to be registered.

```sh
ICommerceAPIServiceProvider, CommerceAPIServiceProvider>();
IMessengerService, MessengerService>();
ICartService, CartService>();
ILoggerService, Logger>();
IClientService, OptClientService>();
IAdminClientService, AdminClientService>();
Locator.Current.GetService<IFilesystemProvider>());
heService, CacheService>();
ISessionService, SessionService>();
ICategoryService, CategoryService>();
IAccountService, AccountService>();
IAuthenticationService, AuthenticationService>();
IAdminAuthenticationService, AdminAuthenticationService>();
ISettingsService, SettingsService>();
IProductService, ProductService>();
IProductV2Service, ProductV2Service>();
IWebsiteService, WebsiteService>();
IWarehouseService, WarehouseService>();
ILocatorService, LocatorService>();
IBillToService, BillToService>();
IOrderService, OrderService>();
IWishListService, WishListService>();
IMessageService, MessageService>();
IMobileContentService, MobileContentService>();
IMobileSpireContentService, MobileSpireContentService>();
IAutocompleteService, AutocompleteService>();
IBrandService, BrandService>();
ITranslationService, TranslationService>();
IDealerService, DealerService>();
IInvoiceService, InvoiceService>();
IJobQuoteService, JobQuoteService>();
IQuoteService, QuoteService>();
IVmiLocationsService, VmiLocationsService>();

```

When manually configuring the project to your environment, we recommend you overwrite our `ClientService.CreateClient()` method. In this method, you will need to assign the Host, ClientId, ClientSecret, and set the `IsCachingEnabled` boolean value. This is also where you can configure the HTTP client. An example implementation might look something like the following:

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

A another option if you're not using the `IServiceCollection` extension method is to call our static `ClientConfig` class's `InitClientConfig(string hostURL, string clientId, string clientSecret, bool isCachingEnabled)` within your start up class, before you register the services to your IoC Container.

```sh
ClientConfig.InitClientConfig("hostURL", "clientId", "clientSecret", isCachingEnabled)
```

##### If Caching is enabled
If you have enabled caching, you will need to add [Akavache](https://github.com/reactiveui/Akavache) to your project. Once added, you will need to grab an instance of Akavache's `IFilesystemProvider` interface. We recommend using Splat's Locator class to grab this service to register to your IoC Container.

If using IServiceCollection:
`services.AddSingleton(Locator.Current.GetService<IFilesystemProvider>());`


# 2. Implement Additional Services
Once the package has been configured using either approach, you will need to implement platform specific services in order for the SDK to function properly. The interfaces to do this are provided by the API SDK package. However, you will need to implement these services yourself.
1. ILocalStorageService
2. ISecureStorageService
3. INetworkService
4. ITrackingService
5. ICommerceAPIServiceProvider

#### ILocalStorageService
This service manages persistent local storage. The parameters needed to configure the service are as follows:

`string Load(string key)`
`string Load(string key, string defaultValue);`
`int LoadInt(string key);`
`void Save(string key, string value);`
`void Save(string key, int value);`
`bool ClearAll();`
`bool Remove(string key);`

#### ISecureStorageService
This service can persist sensitive data securely. This is used to store session information such as Refresh & Access tokens. The parameters needed to configure the service are as follows:

`string Load(string key);`
`bool Save(string key, string value);`
`bool Remove(string key);`
`bool ClearAll();`

#### INetworkService
This service determines network availability. This is an optional service, used to determine if a network request should be sent or not. If this method returns false, the SDK will attempt to grab a cached version of the response. Otherwise, if true, the SDK will make the request as normal.
_Note: If IsOnline() returns_ `false`_, and if you have configured caching off, then the request will always return_ `null`_._

`bool IsOnline();`

#### ITrackingService
This service tracks events. This is where you would add your analytics service implementation. If you don't yet have an analytic service, you should still implement this interface, but the methods can be left blank.  The parameters needed to configure the service are as follows:

`ISessionService SessionService { get; }`
`void Initialize();`
`void TrackEvent(AnalyticsEvent analyticsEvent);`
`void TrackException(Exception exception, Dictionary<string, string> properties = null);`
`void ForceCrash();`
`void SetUserID(string userId);`

#### ICommerceAPIServiceProvider
This service retrieves the API related services. Although you are able to inject the dependencies you need manually, we have provideded this interface to help condense your class's constructor size. It will be up to you to implement the accessor methods. To do this, you can use `IServiceProvider`'s [GetService(Type)](https://docs.microsoft.com/en-us/dotnet/api/system.iserviceprovider.getservice?view=net-6.0) or if you're using MvvmCross, you can resolve the dependency via: `Mvx.IoCProvider.Resolve<IService>();`

# How to use the SDK
Once you have set up the API SDK, you are ready to start using it to make calls to the B2B Commerce API! In order to reduce the number of services you need to inject on your own, we recommend you use the `ICommerceAPIServiceProvider` implementation you created in Step 2. This will enable each service offered by the API SDK. To use it, you need to inject this into your class, and then call the appropriate getter method.
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

There are some service methods that will require you to provide values for the query parameters. We offer [QueryParameter objects](https://github.com/InsiteSoftware/commerce-csharp-sdk/tree/develop/CommerceApiSDK/Models/Parameters) that can assist you in this area.
```sh
var parameters = new BillTosQueryParameters
    {
        Filter = this.CurrentSearchText,
        Page = this.currentPage,
        Exclude = new List<string> { "excludeshowall" },
    };

var billToResult = await commerceAPIServiceProvider.GetBillToService().GetBillTosAsync(parameters);
```
