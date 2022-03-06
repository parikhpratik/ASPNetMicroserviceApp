using Common.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotAPIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingcontet, config) =>
                {
                    config.AddJsonFile($"ocelot.{hostingcontet.HostingEnvironment.EnvironmentName}.json", true, true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog(SeriLogger.Configure);
            //.ConfigureLogging((hostingcontext, loggingBuilder) =>
            //{
            //    loggingBuilder.AddConfiguration(hostingcontext.Configuration.GetSection("Logging"));
            //    loggingBuilder.AddConsole();
            //    loggingBuilder.AddDebug();
            //});
    }
}
