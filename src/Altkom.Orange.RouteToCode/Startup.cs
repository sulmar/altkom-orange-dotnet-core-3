using Altkom.Orange.Fakers;
using Altkom.Orange.FakeServices;
using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Altkom.Orange.RouteToCode
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapGet("/api/customers", async context =>
                {
                    ICustomerService customerService = context.Request.HttpContext.RequestServices.GetRequiredService<ICustomerService>();
                    var customers = customerService.Get();

                    var json = JsonSerializer.Serialize(customers);

                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                });

                endpoints.MapGet("/api/customers/{id:int}", async context =>
                {
                    context.Request.RouteValues.TryGetValue("id", out object id);

                    int customerId = int.Parse(id.ToString());

                    ICustomerService customerService = context.Request.HttpContext.RequestServices.GetRequiredService<ICustomerService>();
                    Customer customer = customerService.Get(customerId);

                    var json = JsonSerializer.Serialize(customer);

                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);

                });
            });
        }
    }
}
