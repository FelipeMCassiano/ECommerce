using Communication.Responses;
using Exceptions.BaseExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.API.Filters;

using Microsoft.AspNetCore.Mvc.Filters;

public class ExceptionFilter : IExceptionFilter 
{
    public void OnException(ExceptionContext context)
    {
        
        context.ExceptionHandled = true;
        
        if (context.Exception is ECommerceException ex)
        {
                context.HttpContext.Response.StatusCode = (int)ex.GetHttpStatus();
                context.Result = new ObjectResult(new ResponseError(ex.GetErrorMessages()));
                return;
        }
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseError(context.Exception.Message));
    }
}