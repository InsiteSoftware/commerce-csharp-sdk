using CommerceApiSDK.Services.Interfaces;
using Moq;

namespace CommerceApiSDK.Test.Services
{
    /// <summary>
    /// Base class for tests of api services
    /// </summary>
    public class ServiceTestBase
    {
        protected Mock<IOptiAPIBaseServiceProvider> OptiAPIBaseServiceMock;

        protected virtual void SetUp()
        {
            OptiAPIBaseServiceMock = new Mock<IOptiAPIBaseServiceProvider>();
        }
    }
}
