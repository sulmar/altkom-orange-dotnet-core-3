using Altkom.Orange.Fakers;
using Altkom.Orange.FakeServices;
using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using Altkom.Orange.Models.Validators;
using Altkom.Orange.WebApi.RouteConstraints;
using Bogus;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true; // adres url małymi literami
                options.LowercaseQueryStrings = true; // parametry małymi literami

              // Rejestracja wlasnej reguly
                options.ConstraintMap.Add("pesel", typeof(PeselRouteConstraint));
            });

            // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

            services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<CustomerValidator>();
                })
                .AddNewtonsoftJson(options =>
                {
                    options.UseCamelCasing(true); // poprawka malych liter
                    options.SerializerSettings.Converters.Add(new StringEnumConverter(camelCaseText: true));  // Serializacja enum jako tekst
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; // Pomijanie wartosci null
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // Zapobieganie cyklicznej serializacji
                });

            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();

            services.AddSingleton<IMessageService, FakeMessageService>();

            services.AddSingleton<IPeselValidator, MyPeselValidator>();

            // Open API (swagger)
            // dotnet add package NSwag.AspNetCore
            services.AddOpenApiDocument( options =>
            {
                options.Title = "Orange API";
                options.DocumentName = "Orange API by Orange";
                options.Version = "v1";
                options.Description = "Lorem ipsum";
            });

            // Generowanie dokumentacji - należy dodać do pliku projektu
            // <GenerateDocumentationFile>true</GenerateDocumentationFile>

            // dotnet add package Swashbuckle.AspNetCore


            // dotnet add package FluentValidation.AspNetCore

            // services.AddTransient<IValidator<Customer>, CustomerValidator>();

            // Rejestracja opcji za pomoca IOptions
            // services.Configure<FakeCustomerServiceOptions>(Configuration.GetSection("CustomerOptions"));

            // Rejestracja opcji za pomoca klasy POCO
            FakeCustomerServiceOptions customerOptions = new FakeCustomerServiceOptions();
            Configuration.GetSection("CustomerOptions").Bind(customerOptions);
            services.AddSingleton(customerOptions);

            // setx ASPNETCORE_ENVIRONMENT "Staging"

            // dotnet run --environment "Staging"

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsStaging())
            {

            }

#if DEBUG
            Trace.WriteLine("XXXX");
#endif

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Open API (swagger)
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            string smsApi = Configuration["sms"];

            string smsUri = Configuration["Sms:Url"];
            int port = int.Parse(Configuration["Sms:Port"]);

            var sms = Configuration.GetSection("Sms").Get<Dictionary<string, object>>();

            string connectionString = Configuration.GetConnectionString("OrangeConnection");

            string googleMapsApiKey = Configuration["GoogleMapsApiKey"];

            // user-secrets

            // C:\Users\{user}\AppData\Roaming\Microsoft\UserSecrets\{guid}
        }
    }
}
