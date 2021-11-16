using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.Infrastrcture.Data;

namespace Ordering.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateAndSeedDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        async private static void CreateAndSeedDatabase(IHost host)
        {
            using (var scope=host.Services.CreateScope())
            {
                var serices = scope.ServiceProvider;
                var loggerFactory = serices.GetRequiredService<ILoggerFactory>();
                try
                {
                    var orderContext = serices.GetRequiredService<OrderContext>();
                    await OrderContextSeed.SeedAsync(orderContext, loggerFactory);
                }
                catch (Exception exception)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(exception.Message);
                }
            }
        }
    }
}
