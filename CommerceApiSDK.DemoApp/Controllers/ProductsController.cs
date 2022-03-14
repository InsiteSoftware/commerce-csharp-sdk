using System.Threading.Tasks;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommerceApiSDK.DemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet(Name = "Products")]
        public async Task<GetProductCollectionResult> Get()
        {
            return await this.productService.GetProductsNoCache(new ProductsQueryParameters());
        }
    }
}
