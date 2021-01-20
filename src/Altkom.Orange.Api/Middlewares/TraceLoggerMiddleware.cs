using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.Api.Middlewares
{
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMy(this IApplicationBuilder app)
        {
            app.UseTraceLogger();
         //   app.UseAuthentication();
            app.UseMiddleware<DashboardMiddleware>();

            return app;
        }
    }


    public static class TraceLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseTraceLogger(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TraceLoggerMiddleware>();
        }
    }

    public class TraceLoggerMiddleware
    {
        private readonly RequestDelegate next;

        public TraceLoggerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Trace.WriteLine($"{context.Request.Method} {context.Request.Path}");

            await next(context);

            Trace.WriteLine($"{context.Response.StatusCode}");
        }
    }
}
