using webmva.Models;
using System;
using System.Linq;
using System.Collections.Generic;

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
            new Modulo{Comando="nmap -T4",Nome="TestNMAP",Tipo=Tipo.SCRIPT,Applicazione=Applicazione.NMAP},
            new Modulo{Comando="SCAN",Nome="TestNessus",Tipo=Tipo.API,Applicazione=Applicazione.NESSUS}
            };
            foreach (Modulo m in moduli)
            { context.Moduli.Add(m); }
            context.SaveChanges();

            if (context.Progetti.Any())
            { return; }
            var progetti = new Progetto[]
            {
            new Progetto{Nome="Test1", Data=new DateTime(2018,02,07,12,00,34),Target="90.147.0.0"},
            new Progetto{Nome="Test2", Data=new DateTime(2018,02,15,12,00,34),Target="90.147.0.0"},
            };
            foreach (Progetto p in progetti)
            { context.Progetti.Add(p); }
            context.SaveChanges();

            if (context.ModuliProgetto.Any())
            { return; }
            var modProg = new ModuliProgetto[]
            {
            new ModuliProgetto{ModuloID=1,ProgettoID=1},
            new ModuliProgetto{ModuloID=2,ProgettoID=1},
            new ModuliProgetto{ModuloID=2,ProgettoID=2},
            };
            foreach (ModuliProgetto mp in modProg)
            { context.ModuliProgetto.Add(mp); }
            context.SaveChanges();
            
        }
    }
}
