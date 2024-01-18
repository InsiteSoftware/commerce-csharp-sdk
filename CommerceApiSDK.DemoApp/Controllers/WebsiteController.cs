using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceApiSDK.Models;
using CommerceApiSDK.Models.Parameters;
using CommerceApiSDK.Models.Results;
using CommerceApiSDK.Services;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CommerceApiSDK.DemoApp.Controllers
{
    [ApiController]
    [Route("api/v1/websites/current")]
    public class WebsiteController : Controller
    {
        private readonly IWebsiteService websiteService;

        public WebsiteController(IWebsiteService websiteService)
        {
            this.websiteService = websiteService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Returns current website")]
        public async Task<ServiceResponse<Website>> Get(WebsiteQueryParameters parameters = null)
        {
            return await this.websiteService.GetWebsite(parameters);
        }

        [HttpGet]
        [Route("addressfields")]
        [SwaggerOperation(Summary = "Returns address fields from current website")]
        public async Task<ServiceResponse<AddressFieldCollection>> GetAddressFields()
        {
            return await this.websiteService.GetAddressFields();
        }

        [HttpGet]
        [Route("countries")]
        [SwaggerOperation(Summary = "Gets all countries from current website")]
        public async Task<ServiceResponse<CountryCollection>> GetCountries()
        {
            return await this.websiteService.GetCountries();
        }

        [HttpGet]
        [Route("countries/{countryId}")]
        [SwaggerOperation(Summary = "Gets specified country from current website")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(Country))]
        public async Task<ServiceResponse<Country>> GetCountry(
            [FromRoute, SwaggerParameter("Country ID", Required = true)] Guid countryId
        )
        {
            return await this.websiteService.GetCountry(countryId);
        }

        [HttpGet]
        [Route("crosssells")]
        [SwaggerOperation(Summary = "Gets website cross sells from current website")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(WebsiteCrosssells))]
        public async Task<ServiceResponse<WebsiteCrosssells>> GetCrosssells()
        {
            return await this.websiteService.GetCrosssells();
        }

        [HttpGet]
        [Route("currencies")]
        [SwaggerOperation(Summary = "Gets available currencies from current website")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(CurrencyCollection))]
        public async Task<ServiceResponse<CurrencyCollection>> GetCurrencies()
        {
            return await this.websiteService.GetCurrencies();
        }

        [HttpGet]
        [Route("currencies/{currencyId}")]
        [SwaggerOperation(Summary = "Returns specified currency from current website")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(Currency))]
        public async Task<ServiceResponse<Currency>> GetCurrency(
            [FromRoute, SwaggerParameter("Currency ID", Required = true)] Guid currencyId
        )
        {
            return await this.websiteService.GetCurrency(currencyId);
        }

        [HttpGet]
        [Route("languages")]
        [SwaggerOperation(Summary = "Get available languages from current website")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(LanguageCollection))]
        public async Task<ServiceResponse<LanguageCollection>> GetLanguages()
        {
            return await this.websiteService.GetLanguages();
        }

        [HttpGet]
        [Route("languages/{languageId}")]
        [SwaggerOperation(Summary = "Returns specified language from current website")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(Language))]
        public async Task<ServiceResponse<Language>> GetLanguage(
            [FromRoute, SwaggerParameter("Language ID", Required = true)] Guid languageId
        )
        {
            return await this.websiteService.GetLanguage(languageId);
        }

        [HttpGet]
        [Route("sitemessages")]
        [SwaggerOperation(Summary = "Get specified site messages from current website")]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Request Successful",
            typeof(GetSiteMessageCollectionResult)
        )]
        public async Task<ServiceResponse<GetSiteMessageCollectionResult>> GetSiteMessages(
            List<string> names = null
        )
        {
            return await this.websiteService.GetSiteMessages(names);
        }

        [HttpGet]
        [Route("states")]
        [SwaggerOperation(Summary = "Gets available states from current website")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(StateCollection))]
        public async Task<ServiceResponse<StateCollection>> GetStates()
        {
            return await this.websiteService.GetStates();
        }

        [HttpGet]
        [Route("states/{stateId}")]
        [SwaggerOperation(Summary = "Returns specified state from current website")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(State))]
        public async Task<ServiceResponse<State>> GetState(
            [FromRoute, SwaggerParameter("Currency ID", Required = true)] Guid stateId
        )
        {
            return await this.websiteService.GetState(stateId);
        }
    }
}
