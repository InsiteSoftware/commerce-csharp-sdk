using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommerceApiSDK.DemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        [HttpGet(Name = "Session")]
        public async Task<Session> Get()
        {
            return await this.sessionService.GetCurrentSession();
        }
    }
}

