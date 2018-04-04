using webmva.Models;
using System;
using System.Linq;

namespace webmva.Data
{
    public class DbInitializer
    {
        public static void Initialize(MyDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Moduli.Any())
            { return; }
            var moduli = new Modulo[]
            {
            new Modulo{Comando="nmap -T4",Nome="TestNMAP",Tipo=Tipo.SCRIPT},
            new Modulo{Comando="SCAN",Nome="TestNessus",Tipo=Tipo.API}
            };
            foreach (Modulo m in moduli)
            { context.Moduli.Add(m); }
            context.SaveChanges();
        }
    }
}
