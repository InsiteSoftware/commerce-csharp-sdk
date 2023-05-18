using System.Threading.Tasks;
using CommerceApiSDK.Services;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommerceApiSDK.DemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticateController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost(Name = "Authenticate")]
        public async Task<ServiceResponse<bool>> Post(string username, string password)
        {
            var result = await this.authenticationService.LogInAsync(username, password);
            return result;
        }
    }
}
