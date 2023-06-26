using System.Threading.Tasks;
using CommerceApiSDK.Services;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommerceApiSDK.DemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public LogoutController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost(Name = "Logout")]
        public async Task Post()
        {
            await this.authenticationService.LogoutAsync();
        }
    }
}
