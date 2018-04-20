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
        private static void CreaCartellaProgetti(){
            if(!Directory.Exists(Globals.CartellaTuttiProgetti))
            {
                // Se sono qui la cartella che conterrà tutti i progetti ancora non esiste
                // Quindi la creo
                Directory.CreateDirectory(Globals.CartellaTuttiProgetti);
            }
        }
        public static void Main(string[] args)
        {
            // Mi assicuro che la cartella dove risiederanno
            // i report dei Progetti esista
            CreaCartellaProgetti();
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

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
