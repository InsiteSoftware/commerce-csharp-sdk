using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Services;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceApiSDK.DemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillTosController : Controller
    {
        private readonly IBillToService billToService;

        public BillTosController(IBillToService billToService)
        {
            this.billToService = billToService;
        }

        [HttpGet(Name = "CurrentBillTo")]
        public async Task<ServiceResponse<BillTo>> Get(string expand)
        {
            var param = new BillTosQueryParameters() { Expand = expand?.Split(",").ToList() };
            return await this.billToService.GetCurrentBillTo(param);
        }
    }
}
