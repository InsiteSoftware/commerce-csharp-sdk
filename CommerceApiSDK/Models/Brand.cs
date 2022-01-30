namespace CommerceApiSDK.Models
{
    using System;
    using System.Collections.Generic;

    public class Brand : BaseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string ExternalUrl { get; set; }

        public string DetailPagePath { get; set; }

        public string ProductListPagePage { get; set; }

        public string LogoSmallImagePath { get; set; }

        public string LogoLargeImagePath { get; set; }

        public string LogoAltText { get; set; }

        public string FeaturedImagePath { get; set; }

        public string FeaturedImageAltText { get; set; }

        public string HtmlContent { get; set; }

        public List<Product> TopSellerProducts { get; set; }
    }

    public class BrandAlphabet
    {
        public string Letter { get; set; }

        public int Count { get; set; }
    }
}
