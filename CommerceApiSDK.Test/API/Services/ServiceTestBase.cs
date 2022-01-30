namespace CommerceApiSDK.Test.Services
{
    using CommerceApiSDK.Services.Interfaces;
    using Moq;
    using MvvmCross;
    using MvvmCross.Base;
    using MvvmCross.IoC;
    using MvvmCross.Logging;

    /// <summary>
    /// Base class for tests of api services
    /// </summary>
    public class ServiceTestBase
    {
        protected Mock<IClientService> ClientServiceMock;
        protected Mock<INetworkService> NetworkServiceMock;
        protected Mock<ITrackingService> TrackingServiceMock;
        protected Mock<ISessionService> SessionServiceMock;
        protected Mock<IMvxLogProvider> LogProviderMock;
        protected Mock<ICacheService> CacheServiceMock;

        protected virtual void SetUp()
        {
            this.ClientServiceMock = new Mock<IClientService>();
            this.NetworkServiceMock = new Mock<INetworkService>();
            this.TrackingServiceMock = new Mock<ITrackingService>();
            this.SessionServiceMock = new Mock<ISessionService>();
            this.LogProviderMock = new Mock<IMvxLogProvider>();
            this.CacheServiceMock = new Mock<ICacheService>();

            MvxSingleton.ClearAllSingletons();
            var iocOptions = new MvxIocOptions
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.All
            };
            var IocProvider = MvxIoCProvider.Initialize(iocOptions);
            Mvx.IoCProvider.RegisterSingleton<IMvxLogProvider>(this.LogProviderMock.Object);
        }
    }
}
