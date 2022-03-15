using CommerceApiSDK.Models;
using NUnit.Framework;

namespace CommerceApiSDK.Test.Models
{
    [TestFixture]
    public class BreakPriceDtoTests
    {
        [TestCase("1.00", "1")]
        [TestCase("1.2345", "1.2345")]
        [TestCase("1.0700", "1.07")]
        public void BreakQtyDisplay_Removes_Insignificant_Decimals(decimal breakQty, string expected)
        {
            BreakPriceDto breakPriceDto = new BreakPriceDto { BreakQty = breakQty };
            Assert.AreEqual(expected, breakPriceDto.BreakQtyDisplay);
        }
    }
}