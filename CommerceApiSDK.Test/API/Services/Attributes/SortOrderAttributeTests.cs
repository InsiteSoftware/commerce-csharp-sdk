using System;
using System.Collections.Generic;
using System.Linq;
using CommerceApiSDK.Services.Attributes;
using CommerceApiSDK.Test.TestHelpers;
using NUnit.Framework;

namespace CommerceApiSDK.Test.Services.Attributes
{
    [TestFixture]
    class SortOrderAttributeTests
    {
        private List<TestEnumSortOrder> sortOrders = default!;

        [SetUp]
        public void SetUp()
        {
            sortOrders = Enum.GetValues(typeof(TestEnumSortOrder))
                .Cast<TestEnumSortOrder>()
                .ToList();
        }

        [Test]
        public void When5Enums_count_expect_5()
        {
            Assert.AreEqual(5, sortOrders.Count);
        }

        [Test]
        public void When2EnumsSame_count_expect_3()
        {
            List<string> distinctShortOrders = sortOrders
                .Select(SortOrderAttribute.GetSortOrderGroupTitle)
                .Distinct()
                .ToList();
            Assert.AreEqual(3, distinctShortOrders.Count);
        }
    }
}
