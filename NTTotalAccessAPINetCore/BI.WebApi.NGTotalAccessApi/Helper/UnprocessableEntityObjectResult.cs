using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// This modelstate validation from kevin dockx building a aspdotnet restful api pluralsight course.
namespace IdentityServerWebAPI.Helper
{
    public class UnprocessableEntityObjectResult : ObjectResult
    {
        public UnprocessableEntityObjectResult(ModelStateDictionary modelState) : base(new SerializableError(modelState))
        {
            if(modelState == null)
            {
                throw new ArgumentException(nameof(modelState));
            }
            StatusCode = 422;
        }
    }
}
