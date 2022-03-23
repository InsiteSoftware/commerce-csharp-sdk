﻿using System;
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
            string validUrl = $"https://mobileautomation.insitesandbox.com/Catalog/Power-Tools/Circular-Saws?SetContextLanguageCode=&SetContextCurrencyCode=";
            string domain = "https://mobileautomation.insitesandbox.com";
            string path = "/Catalog/Power-Tools/Circular-Saws";

            websiteService = new WebsiteService(OptiAPIBaseServiceMock.Object);

            OptiAPIBaseServiceMock.Setup(o => o.GetClientService().Url).Returns(new Uri(domain));
            OptiAPIBaseServiceMock.Setup(x => x.GetSessionService().CurrentSession).Returns(new Session { });

            string returnedUrl = websiteService.GetAuthorizedURL(path).Result;

            Assert.AreEqual(validUrl, returnedUrl);
        }
    }
}
