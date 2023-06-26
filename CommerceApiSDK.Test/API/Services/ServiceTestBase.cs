using CommerceApiSDK.Services.Interfaces;
using Moq;

namespace CommerceApiSDK.Test.Services
{
    /// <summary>
    /// Base class for tests of api services
    /// </summary>
    public class ServiceTestBase
    {
        protected Mock<IClientService> ClientServiceMock = default!;
        protected Mock<INetworkService> NetworkServiceMock = default!;
        protected Mock<ITrackingService> TrackingServiceMock = default!;
        protected Mock<ISessionService> SessionServiceMock = default!;
        protected Mock<ICacheService> CacheServiceMock = default!;
        protected Mock<ILoggerService> LoggerServiceMock = default!;

        protected virtual void SetUp()
        {
            ClientServiceMock = new Mock<IClientService>();
            NetworkServiceMock = new Mock<INetworkService>();
            TrackingServiceMock = new Mock<ITrackingService>();
            SessionServiceMock = new Mock<ISessionService>();
            CacheServiceMock = new Mock<ICacheService>();
            LoggerServiceMock = new Mock<ILoggerService>();
        }
    }
}
