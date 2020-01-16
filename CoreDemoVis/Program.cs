using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreDemoVis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
           .UseStartup<Startup>()
           .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var builtConfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .AddCommandLine(args)
                                        .Build();
                        config.AddConfiguration(builtConfig);
                    })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        //{
        //    var builtConfig = new ConfigurationBuilder()
        //       .AddJsonFile("appsettings.json")
        //       .AddCommandLine(args)
        //       .Build();

        //    Logger.Logger = new LoggerConfiguration()
        //        .WriteTo.Console()
        //        .WriteTo.File(builtConfig["Logging:FilePath"])
        //        .CreateLogger();

        //    try
        //    {
        //        return WebHost.CreateDefaultBuilder(args)
        //            .ConfigureServices((context, services) =>
        //            {
        //                services.AddMvc();
        //            })
        //            .ConfigureAppConfiguration((hostingContext, config) =>
        //            {
        //                config.AddConfiguration(builtConfig);
        //            })
        //            .ConfigureLogging(logging =>
        //            {
        //                logging.AddSerilog();
        //            })
        //            .UseStartup<Startup>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Fatal(ex, "Host builder error");

        //        throw;
        //    }
        //    finally
        //    {
        //        Log.CloseAndFlush();
        //    }
        //}
    }
}
