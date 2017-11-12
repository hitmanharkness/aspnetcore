using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BI.ServiceBus.Manager.NGTotalAccessApi.Contract;
using BI.WebApi.NGTotalAccessApi.Helper;
using BI.Enterprise.ViewModels.NGTotalAccessApi;
using System.Net;

namespace BI.WebApi.NGTotalAccessApi.Controllers
{

    [Produces("application/json")]
    [Route("api/v1")]
    [SecurityHeaders]
    public class NGTotalAccessApiController : Controller
    { 
        #region Private Variables
        private INGTotalAccessApiManager _manager;
        #endregion

        /// <summary>
        /// Initializes a new instance of the NGTotalAccessApiController.
        /// </summary>
        /// <param name="manager">The manager implementation to be used by the controller.</param>
        public NGTotalAccessApiController(INGTotalAccessApiManager manager)
        {
            this._manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        [HttpGet,
        Route("MenuItems/{userId}")]
        //SwaggerResponse(HttpStatusCode.OK, type: typeof(GetMenuItemsResponseModel)),
        //SwaggerResponse(HttpStatusCode.Forbidden, type: typeof(ErrorMessage)),
        //SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(ErrorMessage)),
        //SwaggerResponse(HttpStatusCode.InternalServerError, type: typeof(ErrorMessage)),
        //SwaggerPayloadSample(typeof(NGTotalAccessAPIInfo), typeof(GetNGTotalAccessAPISampleProvider))]
        public async Task<IActionResult> GetMenuItems(int userId)
        {
            //var methodName = this.GetCallerMethodName();

            if (userId <= 0)
                return StatusCode((int)HttpStatusCode.BadRequest);
            //    throw new ServiceBadRequestException(0, "Missing userId"); // Exception throw???????????????????????????

            var resp = this._manager.GetMenuItems<GetMenuItemsResponseModel>(userId);
            //return base.Request.CreateResponse(HttpStatusCode.OK, resp);
            return Ok(resp);

        }

        [HttpGet,
        Route("client/{clientId}")]
        //SwaggerResponse(HttpStatusCode.OK, type: typeof(GetMenuItemsResponseModel)),
        //SwaggerResponse(HttpStatusCode.Forbidden, type: typeof(ErrorMessage)),
        //SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(ErrorMessage)),
        //SwaggerResponse(HttpStatusCode.InternalServerError, type: typeof(ErrorMessage)),
        //SwaggerPayloadSample(typeof(NGTotalAccessAPIInfo), typeof(GetNGTotalAccessAPISampleProvider))]
        public async Task<IActionResult> GetClientDetails(int clientId)
        {
            if (clientId <= 0)
                return StatusCode((int)HttpStatusCode.BadRequest);
            //    throw new ServiceBadRequestException(0, "Missing userId"); // Exception throw???????????????????????????

            var resp = this._manager.GetMenuItems<GetMenuItemsResponseModel>(clientId);
            //return base.Request.CreateResponse(HttpStatusCode.OK, resp);
            return Ok(resp);

        }


        [HttpGet,
        Route("officer/{officerId}")]
        //SwaggerResponse(HttpStatusCode.OK, type: typeof(GetMenuItemsResponseModel)),
        //SwaggerResponse(HttpStatusCode.Forbidden, type: typeof(ErrorMessage)),
        //SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(ErrorMessage)),
        //SwaggerResponse(HttpStatusCode.InternalServerError, type: typeof(ErrorMessage)),
        //SwaggerPayloadSample(typeof(NGTotalAccessAPIInfo), typeof(GetNGTotalAccessAPISampleProvider))]
        public async Task<IActionResult> GetOfficerDetails(int officerId)
        {
            if (officerId <= 0)
                return StatusCode((int)HttpStatusCode.BadRequest);
            //    throw new ServiceBadRequestException(0, "Missing userId"); // Exception throw???????????????????????????

            var resp = this._manager.GetMenuItems<GetMenuItemsResponseModel>(officerId);
            //return base.Request.CreateResponse(HttpStatusCode.OK, resp);
            return Ok(resp);

        }

        [HttpGet,
         Route("agency/{agencyId}")]
        //SwaggerResponse(HttpStatusCode.OK, type: typeof(GetMenuItemsResponseModel)),
        //SwaggerResponse(HttpStatusCode.Forbidden, type: typeof(ErrorMessage)),
        //SwaggerResponse(HttpStatusCode.BadRequest, type: typeof(ErrorMessage)),
        //SwaggerResponse(HttpStatusCode.InternalServerError, type: typeof(ErrorMessage)),
        //SwaggerPayloadSample(typeof(NGTotalAccessAPIInfo), typeof(GetNGTotalAccessAPISampleProvider))]
        public async Task<IActionResult> GetAgencyDetails(int agencyId)
        {
            if (agencyId <= 0)
                return BadRequest(ModelState); //StatusCode((int)HttpStatusCode.BadRequest);
            //    throw new ServiceBadRequestException(0, "Missing userId"); // Exception throw???????????????????????????

            var resp = this._manager.GetMenuItems<GetMenuItemsResponseModel>(agencyId);
            //return base.Request.CreateResponse(HttpStatusCode.OK, resp);
            return Ok(resp);
        }
    }
}
