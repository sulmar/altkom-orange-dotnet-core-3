using Altkom.Orange.Fakers;
using Altkom.Orange.FakeServices;
using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Altkom.Orange.WebApi.RouteConstraints;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Rejestracja wlasnej reguly
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("pesel", typeof(PeselRouteConstraint));
            });

            services.AddControllers();

            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();

            services.AddSingleton<IPeselValidator, MyPeselValidator>();
        }

      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
