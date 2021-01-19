using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Trace Logger             // Middleware
            app.Use(async (context, next) =>
            {
                Trace.WriteLine($"{context.Request.Method} {context.Request.Path}");

                await next();

                Trace.WriteLine($"{context.Response.StatusCode}");
            });

            // api/customers?format=kml         // Middleware
            app.Use(async (context, next) =>
            {
                // context.Request.QueryString[]
                string accept = "kml";

                context.Request.Headers["Accept"] = accept;

                await next();
            });

            app.Use(async (context, next) =>
            {
                // POST

                context.Request.Headers.Add("X-HTTP-Method-Override", "PUT");

                await next();
            });

            // Autentykacja 
            app.Use(async (context, next) =>
            {
                if (context.Request.Headers.ContainsKey("Authorization"))
                {
                    await next();
                }
                else
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            });

            app.Run(context => context.Response.WriteAsync("Hello .NET Core!"));
        }
    }
}
