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
            new ModuloNMAP{Nome="Ping Scan", NoPortScan=true}, //-sn
            new ModuloNMAP{Nome="Scan Veloce",LivelloParanoia=4, FastScan=true}, // -T4 -F
            new ModuloNMAP{Nome="Scan Intenso",LivelloParanoia=4, AllDetections=true, IncreaseVerbosity=true }, // -T4 -A -v
            new ModuloNMAP{Nome="Scan Porte UDP", SYNScan=true, UDPScan=true }, //-sS -sU
            new ModuloNMAP{Nome="Scan All TCP", ListSpecificPort="1-65535"}, // -p 1-65535
            new ModuloNESSUS{Nome="TestNessus", JSON="prova"}
            };
            foreach (Modulo m in moduli)
            { context.Moduli.Add(m); }
            context.SaveChanges();

            if (context.Progetti.Any())
            { return; }
            var progetti = new Progetto[]
            {
            new Progetto{Nome="Test1"},
            new Progetto{Nome="Test2"},
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
