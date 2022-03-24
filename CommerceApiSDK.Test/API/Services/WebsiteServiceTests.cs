using System;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services;
using NUnit.Framework;

namespace CommerceApiSDK.Test.Services
{
    [TestFixture]
    public class WebsiteServiceTests : ServiceTestBase
    {
        private WebsiteService websiteService;

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();

            websiteService = new WebsiteService(
                OptiAPIBaseServiceMock.Object);
        }

        [Test]
        public void GetAuthorizedURL_WithPath_ReturnsValidUrl()
        {
            string domain = "https://mobileautomation.insitesandbox.com";
            string path = "/Catalog/Power-Tools/Circular-Saws";

            OptiAPIBaseServiceMock.Setup(o => o.GetClientService().Url).Returns(new Uri(domain));
            OptiAPIBaseServiceMock.Setup(x => x.GetSessionService().CurrentSession).Returns(new Session { });

            string languageCode = OptiAPIBaseServiceMock.Object.GetSessionService().CurrentSession?.Language?.LanguageCode;
            string currencyCode = OptiAPIBaseServiceMock.Object.GetSessionService().CurrentSession?.Currency?.CurrencyCode;

            string validUrl = $"https://mobileautomation.insitesandbox.com/Catalog/Power-Tools/Circular-Saws?SetContextLanguageCode={languageCode}&SetContextCurrencyCode={currencyCode}";

            websiteService = new WebsiteService(OptiAPIBaseServiceMock.Object);

            string returnedUrl = websiteService.GetAuthorizedURL(path).Result;

            Assert.AreEqual(validUrl, returnedUrl);
        }
    }
}
