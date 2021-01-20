using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.Api.Middlewares
{
    public class DashboardMiddleware
    {
        private readonly RequestDelegate next;

        public DashboardMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        // http://localhost:5000/dashboard

        public async Task InvokeAsync(HttpContext context)
        {
            HttpRequest request = context.Request;

            if (!request.Path.HasValue || request.Path == "/dashboard")
            {
                string html = "<html><h1>Hello from Dashboard!</h1></html>";

                HttpResponse response = context.Response;

                response.StatusCode = StatusCodes.Status200OK;
                response.ContentType = "text/html";
                await response.WriteAsync(html);
            }
            else
                await next(context);
        }

    }
}
