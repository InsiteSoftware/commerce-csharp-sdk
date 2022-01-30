﻿namespace CommerceApiSDK.Test.Extensions
{
    using System;

    using NUnit.Framework;

    using CommerceApiSDK.Extensions;

    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase("No html")]
        [TestCase("Longer string with no html.")]
        public void StripHtml_WithNoHtml_ReturnsSameString(string testString)
        {
            var outputString = testString.StripHtml();

            Assert.AreEqual(outputString, testString);
        }

        [TestCase("<div>Some string with html</div>", "Some string with html")]
        [TestCase("<div>Embedded <strong>html</strong></div>", "Embedded html")]
        [TestCase("<a href=\"/RedirectTo/SignInPage\" isc-redirect-to-sign-in return-to-url=\"true\">sign in</a>", "sign in")]
        public void StripHtml_WithHtml_ShouldRemoveHtmlFromString(string testString, string expected)
        {
            var output = testString.StripHtml();

            Assert.AreEqual(expected, output);
        }
    }
}
