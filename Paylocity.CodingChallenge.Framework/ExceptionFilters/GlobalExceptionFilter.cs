using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Paylocity.CodingChallenge.Framework.Model;

namespace Paylocity.CodingChallenge.Framework.ExceptionFilters
{
    /// <summary>
    /// The GlobalExceptionFilter.
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpGlobalExceptionFilterBase"/> class.
        /// </summary>
        /// <param name="logger">the logger.</param>
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            //this.HostingEnvironment = env;
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the hosting environment.
        /// </summary>
        /// <value>
        /// The hosting environment.
        /// </value>
        //protected IWebHostEnvironment HostingEnvironment { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger<GlobalExceptionFilter> Logger { get; }

        /// <summary>
        /// Applies the  exception filter to the specified operation using the given context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                return;
            }

            if (context.Exception is BusinessException businessException)
            {
                this.Logger.LogError(this.GetDomainErrorMessage(businessException), context.Exception);
                context.Result = this.GetDomainErrorResult(businessException);
                context.HttpContext.Response.StatusCode = (int)this.GetDomainErrorHttpStatusCode(businessException);
            }
            else
            {
                this.Logger.LogError($"{HttpStatusCode.InternalServerError} - {context.Exception?.Message}", context.Exception);
                var json = new ErrorDetails()
                {
                    ErrorCode = nameof(HttpStatusCode.InternalServerError),
                    ErrorMessage = context.Exception?.Message,
                };

                if (Debugger.IsAttached)
                {
                    //json.DetailedError = context.Exception;
                }

                context.Result = new ObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Gets the domain error message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The error message.</returns>
        protected virtual string GetDomainErrorMessage(BusinessException exception)
        {
            return exception?.Message;
        }

        /// <summary>
        /// Gets the domain error result.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The result.</returns>
        protected virtual IActionResult GetDomainErrorResult(BusinessException exception)
        {
            // For now using this later we can change based on business use case.
            return new UnprocessableEntityObjectResult(exception?.ErrorMessage);
        }

        /// <summary>
        /// Gets the domain error HTTP status code.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The http status code.</returns>
        protected virtual HttpStatusCode GetDomainErrorHttpStatusCode(BusinessException exception)
        {
            // For now using this later we can change based on business use case.
            return HttpStatusCode.UnprocessableEntity;
        }
    }
}
