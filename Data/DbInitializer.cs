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
            new ModuloWAPITI{Applicazione = APPLICAZIONE.WAPITI, Nome="Scan Base"},
            new ModuloOPENDOOR{Applicazione = APPLICAZIONE.OPENDOOR, Nome="Scan Base"},
            new ModuloWASCAN{Applicazione = APPLICAZIONE.WASCAN, Nome="Scan Base"},
            new ModuloDROOPE{Applicazione = APPLICAZIONE.DROOPE, Nome="Scan generico automatico"},
            new ModuloDROOPE{Applicazione = APPLICAZIONE.DROOPE, Nome="Scan Drupal", Cms=CMS.DRUPAL},
            new ModuloWPSCAN{Applicazione = APPLICAZIONE.WPSCAN, Nome="Scan base non intrusivo"},
            new ModuloJOOMSCAN{Applicazione = APPLICAZIONE.JOOMSCAN, Nome="Scan base"},
            new ModuloDNSRECON{Applicazione = APPLICAZIONE.DNSRECON, Nome="Scan base"},
            new ModuloDNSENUM{Applicazione = APPLICAZIONE.DNSENUM, Nome="Scan base"},
            new ModuloFIERCE{Applicazione = APPLICAZIONE.FIERCE, Nome="Scan base"},
            new ModuloINFOGA{Applicazione = APPLICAZIONE.INFOGA, Nome="Scan base"},
            new ModuloINFOGAEMAIL{Applicazione = APPLICAZIONE.INFOGAEMAIL, Nome="Scan Email con breach", Breach=true},
            new ModuloSUBLIST3R{Applicazione = APPLICAZIONE.SUBLIST3R, Nome="Scan base", AllSUB=true},
            new ModuloSQLMAP{Applicazione = APPLICAZIONE.SQLMAP, Nome="Scan base"},
            new ModuloNOSQL{Applicazione = APPLICAZIONE.NOSQL, Nome="NoSQL"},
            new ModuloODAT{Applicazione = APPLICAZIONE.ODAT, Nome="Scan base"},
            new ModuloWIFITE{Applicazione = APPLICAZIONE.WIFITE, Nome="Scan base interattivo"}

            //new ModuloNESSUS{Applicazione = APPLICAZIONE.NESSUS,Nome="TestNessus", JSON="prova"}
            };
            foreach (Modulo m in moduli)
            { context.Moduli.Add(m); }
            context.SaveChanges();

            if (context.Progetti.Any())
            { return; }
            var progetti = new Progetto[]
            {
            new Progetto{Nome="Test1", Descrizione="Questo carattere speciale viene chiamato spazio unificatore perché non indica la fine della linea e impedisce il conseguente ritorno a capo. Utilizzando eccessivamente tale simbolo i browser avrebbero delle difficoltà a inserire correttamente le interruzioni al termine delle linee di testo nel tentativo di renderlo ordinato e leggibile"},
            new Progetto{Nome="Test2"},
            };
            foreach (Progetto p in progetti)
            { context.Progetti.Add(p); }
            context.SaveChanges();

            
            
        }
    }
}

