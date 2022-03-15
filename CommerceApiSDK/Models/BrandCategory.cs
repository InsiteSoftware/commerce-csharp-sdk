using System;
using System.Collections.Generic;
using CommerceApiSDK.Models.Results;
using Newtonsoft.Json;

namespace CommerceApiSDK.Models
{
    public class BrandCategory : BaseModel
    {
        public Guid BrandId { get; set; }

        public Guid CategoryId { get; set; }

        public string ContentManagerId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryShortDescription { get; set; }

        public string FeaturedImagePath { get; set; }

        public string FeaturedImageAltText { get; set; }

        public string ProductListPagePath { get; set; }

        public string HtmlContent { get; set; }

        public IList<BrandCategory> SubCategories { get; set; }

        [JsonIgnore]
        public bool IsLoading { get; set; }

        public static BrandCategory MapCategoryToBrandCategory(GetBrandSubCategoriesResult brandCategoryResult)
        {
            if (brandCategoryResult == null)
            {
                return null;
            }

            BrandCategory brandCategory = new BrandCategory()
            {
                BrandId = new Guid(brandCategoryResult.BrandId),
                CategoryId = new Guid(brandCategoryResult.CategoryId),
                CategoryName = brandCategoryResult.CategoryName,
                CategoryShortDescription = brandCategoryResult.CategoryShortDescription,
                FeaturedImagePath = brandCategoryResult.FeaturedImagePath,
                FeaturedImageAltText = brandCategoryResult.FeaturedImageAltText,
                ProductListPagePath = brandCategoryResult.ProductListPagePath,
                HtmlContent = brandCategoryResult.HtmlContent,
            };
            List<BrandCategory> brandSubCategories = new List<BrandCategory>();

            if (brandCategoryResult.SubCategories != null)
            {
                foreach (GetBrandSubCategoriesResult subCategory in brandCategoryResult.SubCategories)
                {
                    BrandCategory brandSubCategory = MapCategoryToBrandCategory(subCategory);
                    brandSubCategories.Add(brandSubCategory);
                }
            }

            brandCategory.SubCategories = brandSubCategories;

            return brandCategory;
        }
    }
}