
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityServerWebAPI.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        #region Private Variables
        //private readonly ILogger _logger;
        #endregion
        public ValidateModelAttribute()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller.ToString();

            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var actionName = controllerActionDescriptor?.ActionName;

            
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
