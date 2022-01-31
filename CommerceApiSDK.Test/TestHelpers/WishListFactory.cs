
namespace CommerceApiSDK.Test.TestHelpers{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CommerceApiSDK.Models;
    using CommerceApiSDK.Services.Interfaces;

    class WishListFactory{
        public const string Shortdescriptionunique = "ShortDescriptionunique";

        public const string ErpNumber = "ERPNumber";

        public const string ManufacturerItemUnique = "ManufacturerItemunique";

        public const string CustomerNameUnique = "CustomerNameunique";

        public const string CustomerName = "CustomerName";

        public const string KeyWord = "word";

        public const string Manufactureritem = "ManufacturerItem";

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
            List<WishListLine> wishlistLines = new();
            int price = end + 1;
            for (int i = start; i < end; i++)
            {
                price--;
                wishlistLines.Add(
                    new WishListLine
                        {
                            ShortDescription =
                                CreateShortDescription(
                                    KeyWord,
                                    20,
                                    preFix + Shortdescriptionunique + i.ToString() + postFix),
                            QtyOrdered = 2,
                            Pricing = new ProductPriceDto { UnitListPrice = price, UnitNetPrice = price },
                            CustomerName =
                                CreateShortDescription(
                                    CustomerName,
                                    2,
                                    preFix + CustomerNameUnique + i.ToString() + postFix),
                            ManufacturerItem =
                                CreateShortDescription(
                                    Manufactureritem,
                                    1,
                                    preFix + ManufacturerItemUnique + i.ToString() + postFix),
                            ERPNumber = CreateShortDescription(
                                ErpNumber,
                                1,
                                preFix + ErpNumber + i.ToString() + postFix),
                            ProductId = new Guid(),
                            IsActive = true,
                            IsVisible = true
                        });
            }


            return wishlistLines;

        }

        public static string CreateShortDescription(string prefix, int index, string unique)
        {
            StringBuilder stringBuilder = new();
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
                    stringBuilder.Append(unique + KeyWord);
                    stringBuilder.Append(" ");
                }
            }

            return stringBuilder.ToString();

        }
    }
}
