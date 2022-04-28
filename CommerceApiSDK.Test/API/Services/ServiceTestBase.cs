using CommerceApiSDK.Services.Interfaces;
using Moq;

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
        protected Mock<ICacheService> CacheServiceMock;
        protected Mock<ILoggerService> LoggerServiceMock;

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
