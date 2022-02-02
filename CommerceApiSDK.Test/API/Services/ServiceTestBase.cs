using CommerceApiSDK.Services.Interfaces;
using Moq;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Logging;

namespace CommerceApiSDK.Test.Services
{
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
            ClientServiceMock = new Mock<IClientService>();
            NetworkServiceMock = new Mock<INetworkService>();
            TrackingServiceMock = new Mock<ITrackingService>();
            SessionServiceMock = new Mock<ISessionService>();
            LogProviderMock = new Mock<IMvxLogProvider>();
            CacheServiceMock = new Mock<ICacheService>();

            MvxSingleton.ClearAllSingletons();
            MvxIocOptions iocOptions = new MvxIocOptions()
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.All
            };
            _ = MvxIoCProvider.Initialize(iocOptions);
            Mvx.IoCProvider.RegisterSingleton(LogProviderMock.Object);
        }
    }
}
