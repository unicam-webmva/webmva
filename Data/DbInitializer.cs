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
            new ModuloNMAP{Applicazione = APPLICAZIONE.NMAP,Nome="Ping Scan", NonTCPScan=NONTCPSCAN.NOPORT}, //-sn
            new ModuloNMAP{Applicazione = APPLICAZIONE.NMAP,Nome="Scan Veloce",Tempo = TEMPI.QUATTRO, FastScan=true}, // -T4 -F
            new ModuloNMAP{Applicazione = APPLICAZIONE.NMAP,Nome="Scan Intenso",Tempo = TEMPI.QUATTRO, AllDetections=true, IncreaseVerbosity=true }, // -T4 -A -v
            new ModuloNMAP{Applicazione = APPLICAZIONE.NMAP,Nome="Scan Porte UDP", TCPScan=TCPSCAN.SYN, NonTCPScan=NONTCPSCAN.UDP}, //-sS -sU
            new ModuloNMAP{Applicazione = APPLICAZIONE.NMAP,Nome="Scan All TCP", ListSpecificPort="1-65535"}, // -p 1-65535
            new ModuloNESSUS{Applicazione = APPLICAZIONE.NESSUS,Nome="TestNessus", JSON="prova"}
            };
            foreach (Modulo m in moduli)
            { context.Moduli.Add(m); }
            context.SaveChanges();

            if (context.Progetti.Any())
            { return; }
            var progetti = new Progetto[]
            {
            new Progetto{Nome="Test1", Data=new DateTime(2018,02,07,12,00,34),Target="90.147.0.0", Descrizione="Questo carattere speciale viene chiamato spazio unificatore perché non indica la fine della linea e impedisce il conseguente ritorno a capo. Utilizzando eccessivamente tale simbolo i browser avrebbero delle difficoltà a inserire correttamente le interruzioni al termine delle linee di testo nel tentativo di renderlo ordinato e leggibile"},
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
            new ModuliProgetto{ModuloID=3,ProgettoID=1},
            new ModuliProgetto{ModuloID=2,ProgettoID=2},
            };
            foreach (ModuliProgetto mp in modProg)
            { context.ModuliProgetto.Add(mp); }
            context.SaveChanges();
            
        }
    }
}
