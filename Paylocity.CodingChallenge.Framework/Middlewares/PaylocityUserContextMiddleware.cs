using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Paylocity.CodingChallenge.Framework.Middlewares
{
    public class PaylocityUserContextMiddleware
    {
        /// <summary>
        /// The next.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleManagementContextMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <exception cref="System.ArgumentNullException">The exception.</exception>
        public PaylocityUserContextMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>the task.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
                var peopleManagementUserContext = context.RequestServices.GetService(typeof(IPaylocityUserContext)) as IPaylocityUserContext;
                ClaimsPrincipal localuser = context.User;
                var identityClaims = localuser.Claims.Select(x => new { Type = x.Type, Value = x.Value });
                peopleManagementUserContext.Name = identityClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                peopleManagementUserContext.Email = identityClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                peopleManagementUserContext.UserId = Guid.Parse(identityClaims.FirstOrDefault(c => c.Type == ObjectIdentifier)?.Value);
            }

            await this.next(context).ConfigureAwait(false);
        }
    }
}
