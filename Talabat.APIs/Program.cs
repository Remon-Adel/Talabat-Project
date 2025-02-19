using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
          var host = CreateHostBuilder(args).Build();

           using var Scope = host.Services.CreateScope();
            var services = Scope.ServiceProvider;
            var LoggerFactory= services.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();

              await StoreContextSeed.SeedAsync(context, LoggerFactory);
            }
            catch (Exception ex)
            {
                var logger=LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured During Apply Migration");
                
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
