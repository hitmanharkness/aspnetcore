using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BI.WebApi.NGTotalAccessApi.Filters.ActionFilters
{
    public class LoggingActionFilter : IActionFilter
    {
        #region Private Variables
        private readonly ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private string _actionName;
        #endregion
        public LoggingActionFilter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var _actionName = controllerActionDescriptor?.ActionName;

            _logger = _loggerFactory.CreateLogger(controllerActionDescriptor.DisplayName);
            this._logger.LogInformation(_actionName + " - Started");

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

            //To do : before the action executes  
            //context.Result = new ContentResult()
            //{
            //    Content = "Short circuit filter"
            //};
            this._logger.LogInformation(_actionName + " - Completed");
        }
    }
}
