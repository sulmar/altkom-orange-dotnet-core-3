using Altkom.Orange.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Altkom.Orange.Api.Middlewares
{
    public class CustomersMiddleware
    {
        private readonly RequestDelegate next;
        private ICustomerService customerService;

        public CustomersMiddleware(RequestDelegate next, ICustomerService customerService)
        {
            this.next = next;
            this.customerService = customerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            HttpRequest request = context.Request;

            if (!request.Path.HasValue || request.Path == "/api/customers" && request.Method == "GET")
            {
                var customers = customerService.Get();

                var json = JsonSerializer.Serialize(customers);

                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            else
            {
                await next(context);
            }
        }
    }
}
