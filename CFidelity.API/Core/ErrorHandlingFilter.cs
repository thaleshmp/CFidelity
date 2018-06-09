using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace CFidelity.API.Core
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HandleExceptionAsync(context);
            context.ExceptionHandled = true;
        }

        private void HandleExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception.GetType().Name.Contains("Unauthorized"))
                SetExceptionCodeResult(context, exception, HttpStatusCode.Unauthorized);
            else
            {
                HttpStatusCode status;

                if (exception.Message.ToLowerInvariant().Contains("timeout"))
                    status = HttpStatusCode.RequestTimeout;
                else
                    status = HttpStatusCode.InternalServerError;

                SetExceptionResult(context, exception, status);
            }
        }

        private void SetExceptionResult(ExceptionContext context, Exception exception, HttpStatusCode code)
        {
            context.Result = new JsonResult(exception)
            {
                StatusCode = (int)code
            };
        }

        private void SetExceptionCodeResult(ExceptionContext context, Exception exception, HttpStatusCode code)
        {
            context.Result = new ObjectResult(null) { StatusCode = (int)code };
        }
    }
}
