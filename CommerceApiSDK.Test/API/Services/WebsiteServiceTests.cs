namespace CommerceApiSDK.Test.Services
{
    using System;
    using System.Threading.Tasks;
    using CommerceApiSDK.Models;
    using CommerceApiSDK.Services;
    using NUnit.Framework;

    [TestFixture]
    public class WebsiteServiceTests : ServiceTestBase
    {
        private WebsiteService websiteService;

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();

            this.websiteService = new WebsiteService(
                this.ClientServiceMock.Object,
                this.NetworkServiceMock.Object,
                this.TrackingServiceMock.Object,
                this.SessionServiceMock.Object,
                this.CacheServiceMock.Object);
        }

        [Test]
        public void GetAuthorizedURL_WithPath_ReturnsValidUrl()
        {
            var languageCode = this.SessionServiceMock.Object.CurrentSession?.Language?.LanguageCode;
            var currencyCode = this.SessionServiceMock.Object.CurrentSession?.Currency?.CurrencyCode;
            var validUrl = $"https://mobileautomation.insitesandbox.com/Catalog/Power-Tools/Circular-Saws?SetContextLanguageCode={languageCode}&SetContextCurrencyCode={currencyCode}";
            var domain = "https://mobileautomation.insitesandbox.com";
            var path = "/Catalog/Power-Tools/Circular-Saws";

            this.websiteService = new WebsiteService(this.ClientServiceMock.Object, this.NetworkServiceMock.Object, this.TrackingServiceMock.Object, this.SessionServiceMock.Object, this.CacheServiceMock.Object);

            this.ClientServiceMock.Setup(o => o.Url).Returns(new Uri(domain));
            this.SessionServiceMock.Setup(x => x.CurrentSession).Returns(new Session { });

            var returnedUrl = this.websiteService.GetAuthorizedURL(path).Result;

            Assert.AreEqual(validUrl, returnedUrl);
        }
    }
}
