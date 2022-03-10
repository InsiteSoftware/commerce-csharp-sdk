using System;
using System.Collections.Generic;
using System.Text;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Test.TestHelpers
{
    class WishListFactory
    {
        public static List<WishListLine> CreateWishListLines()
        {
            return new List<WishListLine>
                       {
                           new WishListLine
                               {
                                   ShortDescription = "a product",
                                   QtyOrdered = 1,
                                   Pricing =
                                       new ProductPriceDto
                                           {
                                               UnitListPrice = 1,
                                               UnitNetPrice = 1
                                           },
                                   ProductId = new Guid(),
                                   IsActive = true,
                                   IsVisible = true
                               },
                           new WishListLine
                               {
                                   ShortDescription = "c product",
                                   QtyOrdered = 1,
                                   Pricing =
                                       new ProductPriceDto
                                           {
                                               UnitListPrice = 4,
                                               UnitNetPrice = 5
                                           },
                                   ProductId = new Guid(),
                                   IsActive = true,
                                   IsVisible = true
                               },
                           new WishListLine
                               {
                                   ShortDescription = "b product",
                                   QtyOrdered = 1000,
                                   Pricing =
                                       new ProductPriceDto
                                           {
                                               UnitListPrice = 10,
                                               UnitNetPrice = 9.14M
                                           },
                                   ProductId = new Guid(),
                                   IsActive = true,
                                   IsVisible = true
                               },
                           new WishListLine
                               {
                                   ShortDescription = "0 product",
                                   QtyOrdered = 2,
                                   Pricing =
                                       new ProductPriceDto
                                           {
                                               UnitListPrice = 2,
                                               UnitNetPrice = 1
                                           },
                                   ProductId = new Guid(),
                                   IsActive = true,
                                   IsVisible = true
                               }
                       };


        }

        public static List<WishListLine> CreateWishListLinesforSearch(
            int start,
            int end,
            string preFix = "",
            string postFix = "")
        {
            List<WishListLine> wishlistLines = new List<WishListLine>();
            int price = end + 1;
            for (int i = start; i < end; i++)
            {
                price--;
                wishlistLines.Add(
                    new WishListLine
                        {
                            ShortDescription =
                                CreateShortDescription(
                                    CommerceAPIConstants.KeyWord,
                                    20,
                                    preFix + CommerceAPIConstants.Shortdescriptionunique + i.ToString() + postFix),
                            QtyOrdered = 2,
                            Pricing = new ProductPriceDto { UnitListPrice = price, UnitNetPrice = price },
                            CustomerName =
                                CreateShortDescription(
                                    CommerceAPIConstants.CustomerName,
                                    2,
                                    preFix + CommerceAPIConstants.CustomerNameUnique + i.ToString() + postFix),
                            ManufacturerItem =
                                CreateShortDescription(
                                    CommerceAPIConstants.Manufactureritem,
                                    1,
                                    preFix + CommerceAPIConstants.ManufacturerItemUnique + i.ToString() + postFix),
                            ERPNumber = CreateShortDescription(
                                CommerceAPIConstants.ErpNumber,
                                1,
                                preFix + CommerceAPIConstants.ErpNumber + i.ToString() + postFix),
                            ProductId = new Guid(),
                            IsActive = true,
                            IsVisible = true
                        });
            }


            return wishlistLines;

        }

        public static string CreateShortDescription(string prefix, int index, string unique)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < index; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(i.ToString(" "));
                    stringBuilder.Append(prefix);
                    stringBuilder.Append(i.ToString());
                }
                else if (!string.IsNullOrEmpty(unique))
                {
                    stringBuilder.Append(unique);
                    stringBuilder.Append(" ");
                    stringBuilder.Append(unique + CommerceAPIConstants.KeyWord);
                    stringBuilder.Append(" ");
                }
            }

            return stringBuilder.ToString();

        }
    }
}
