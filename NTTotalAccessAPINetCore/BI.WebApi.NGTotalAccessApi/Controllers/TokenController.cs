//using BI.Security.Models;
//using IdentityModel.Client;
//using IdentityServerWebAPI.Filters;
//using IdentityServerWebAPI.Helper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System;
//using System.Net;
//using System.Threading.Tasks;

//namespace BI.Security.Identity.Controllers
//{
//    [SecurityHeaders]
//    public class TokenController : Controller
//    {
//        #region Private Variables

//        private readonly string _identityServer;
//        private readonly string _tokenEndpont;

//        private readonly ILogger _logger;


//        private readonly IOptions<AppSettings> _appSettingsAccessor;
//        private AppSettings _appSettings
//        {
//            get { return _appSettingsAccessor.Value; }
//        }

//        #endregion


//        public TokenController(IOptions<AppSettings> appSettingsAccessor,
//                               ILogger<TokenController> logger) // Same as calling loggerFactory.CreateLogger<TokenController>();
//        {
//            _appSettingsAccessor = appSettingsAccessor;
//            _logger = logger;

//            _identityServer = _appSettings.IdentityServer;
//            _tokenEndpont = _appSettings.IdentityServer + "/connect/token";
//        }



//        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//        /// <summary>
//        /// Endpoints for the Token WEBAPI calls.
//        /// </summary>
//        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//        // [FromBody] on the parameter. This tells the framework to use the content-type header of the request to decide which of the configured IInputFormatters to use for model binding.
//        [HttpPost]
//        [ValidateModelAttribute] // Model validation filter.
//        [AllowAnonymous] // This will not be allowed.
//        [Route("api/token/renewtoken")]
//        public async Task<IActionResult> RenewToken([FromBody] RenewTokenModel model)
//        {
//            // They didn't give us a model. Shame. The rest of the model validation is handled in the ValidationModelAttribute.
//            if (model == null)
//                return StatusCode((int)HttpStatusCode.BadRequest, new RenewTokenResponse { Error = "Payload is empty or malformed." });

//            AppSettings appSettings = _appSettingsAccessor.Value;

//            var disco = await DiscoveryClient.GetAsync(appSettings.IdentityServer);
//            if (disco.IsError) throw new Exception(disco.Error); //500 error. Global handler will catch it.

//            var tokenClient = new TokenClient(disco.TokenEndpoint, model.ClientApplication, model.ClientSecret);
//            var tokenResult = await tokenClient.RequestRefreshTokenAsync(model.RefreshToken);

//            if (!tokenResult.IsError)
//            {
//                var renewTokenResult = new RenewTokenResponse()
//                {
//                    AccessToken = tokenResult.AccessToken,
//                    RefreshToken = tokenResult.RefreshToken,
//                };
//                return Ok(renewTokenResult);
//            }

//            this._logger.LogWarning(LoggingEvents.GetItemNotFound, $"RenewToken - {tokenResult.Error}", tokenResult.Error);
//            // I don't know what happened.
//            return StatusCode((int)tokenResult.HttpStatusCode, new RenewTokenResponse() { Error = tokenResult.Error }); // invalid_grant type or something more sinister.
//        }


//        #region Private Methods

//        #endregion
//    }
//}