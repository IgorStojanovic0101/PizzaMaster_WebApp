using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMaster.Infrastructure.System
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging library here)
                context.Items["ErrorMessage"] = ex.Message;

                // Redirect to the Error view
                var queryString = $"?errorMessage={Uri.EscapeDataString(ex.Message)}";
                context.Response.Redirect("/Home/Error" + queryString);
            }
        }
    }
}
