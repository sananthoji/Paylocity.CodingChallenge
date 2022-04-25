using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.Framework.Middlewares
{
    public static class PaylocityUserContextMiddlewareExtension
    {
        public static IApplicationBuilder UsePaylocityUserContext(
               this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PaylocityUserContextMiddleware>();
        }
    }
}
