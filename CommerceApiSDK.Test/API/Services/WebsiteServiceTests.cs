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
                ClientServiceMock.Object,
                NetworkServiceMock.Object,
                TrackingServiceMock.Object,
                SessionServiceMock.Object,
                CacheServiceMock.Object);
        }

        [Test]
        public void GetAuthorizedURL_WithPath_ReturnsValidUrl()
        {
            string languageCode = SessionServiceMock.Object.CurrentSession?.Language?.LanguageCode;
            string currencyCode = SessionServiceMock.Object.CurrentSession?.Currency?.CurrencyCode;
            string validUrl = $"https://mobileautomation.insitesandbox.com/Catalog/Power-Tools/Circular-Saws?SetContextLanguageCode={languageCode}&SetContextCurrencyCode={currencyCode}";
            string domain = "https://mobileautomation.insitesandbox.com";
            string path = "/Catalog/Power-Tools/Circular-Saws";

            websiteService = new WebsiteService(ClientServiceMock.Object, NetworkServiceMock.Object, TrackingServiceMock.Object, SessionServiceMock.Object, CacheServiceMock.Object);

            ClientServiceMock.Setup(o => o.Url).Returns(new Uri(domain));
            SessionServiceMock.Setup(x => x.CurrentSession).Returns(new Session { });

            string returnedUrl = websiteService.GetAuthorizedURL(path).Result;

            Assert.AreEqual(validUrl, returnedUrl);
        }
    }
}
