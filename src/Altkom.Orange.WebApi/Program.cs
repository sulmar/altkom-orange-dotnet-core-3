using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Orange.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // dotnet add package Serilog.AspNetCore

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()  // Sinks
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File(new CompactJsonFormatter(), "logs/log.json")
                .CreateLogger();

            // Enrich

            // Seq https://datalust.co/seq

            // dotnet add package Serilog.Sinks.Seq

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config)=>
                {
                    string environmentName = hostingContext.HostingEnvironment.EnvironmentName;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
                    config.AddXmlFile("appsettings.xml", optional: true);
                    config.AddUserSecrets<Program>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
