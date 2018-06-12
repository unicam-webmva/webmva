using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using webmva.Data;
using Microsoft.Extensions.DependencyInjection;

namespace webmva
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MyDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Errore durante la creazione del db");
                }
            }
            host.Run();   
        }

        /*
        
        https://stackoverflow.com/questions/42079956/suppress-sql-queries-logging-in-entity-framework-core
        
         */

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:80")
                .ConfigureLogging((context, logging)=> {
                    var env = context.HostingEnvironment;
                    var config = context.Configuration.GetSection("Logging");
                    logging.AddConfiguration(config);
                    logging.AddConsole();
                    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                })
                .Build();
    }
}
