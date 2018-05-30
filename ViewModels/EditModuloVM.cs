using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webmva.Models;

namespace webmva.ViewModels
{
    public class EditModuloVM
    {
        public ModuloNMAP NMAP { get; set; }
        public ModuloNESSUS NESSUS { get; set; }
        public ModuloDNSRECON DNSRECON { get; set; }
        public ModuloFIERCE FIERCE { get; set; }
        public ModuloDROOPE DROOPE{get; set;}
        public ModuloJOOMSCAN JOOMSCAN{get; set;}
        public ModuloWPSCAN WPSCAN{get; set;} 
        public ModuloINFOGA INFOGA{get; set;}
        public ModuloOPENDOOR OPENDOOR{get; set;}
        public ModuloINFOGAEMAIL INFOGAEMAIL{get; set;}
        public ModuloSUBLIST3R SUBLIST3R{get; set;}
        public ModuloWASCAN WASCAN{get; set;}
        public ModuloWAPITI WAPITI{get; set;}
        public ModuloSQLMAP SQLMAP{get; set;}
        public ModuloWIFITE WIFITE{get; set;}
        public ModuloNOSQL NOSQL{get; set;}

        public EditModuloVM()
        {
            NMAP = new ModuloNMAP();
            NESSUS = new ModuloNESSUS();
            DNSRECON = new ModuloDNSRECON();
            FIERCE = new ModuloFIERCE();
            DROOPE = new ModuloDROOPE();
            JOOMSCAN = new ModuloJOOMSCAN();
            WPSCAN = new ModuloWPSCAN();
            INFOGA = new ModuloINFOGA();
            INFOGAEMAIL = new ModuloINFOGAEMAIL();
            SUBLIST3R = new ModuloSUBLIST3R();
            WAPITI = new ModuloWAPITI();
            SQLMAP = new ModuloSQLMAP();
            WIFITE = new ModuloWIFITE();
            OPENDOOR = new ModuloOPENDOOR();
            WASCAN = new ModuloWASCAN();
            NOSQL = new ModuloNOSQL();


        }
        public EditModuloVM(ModuloNMAP mod)
        {
            NMAP = mod;
            NESSUS = null;
            DNSRECON = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            INFOGA = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;

        }
        public EditModuloVM(ModuloNESSUS mod)
        {
            NMAP = null;
            DNSRECON = null;
            NESSUS = mod;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            INFOGA = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            FIERCE = null;
            WIFITE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }
        public EditModuloVM(ModuloDNSRECON mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = mod;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            INFOGA = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null; 
            WIFITE = null;
            FIERCE = null; 
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }
        public EditModuloVM(ModuloDROOPE mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = mod;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null;
            WIFITE = null; 
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }
        public EditModuloVM(ModuloJOOMSCAN mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = mod;
            WPSCAN = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null;
            WIFITE = null; 
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }
        public EditModuloVM(ModuloINFOGA mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = mod;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null; 
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null;
            WIFITE = null; 
            FIERCE = null; 
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }
         public EditModuloVM(ModuloINFOGAEMAIL mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null; 
            WAPITI= null;
            INFOGAEMAIL = mod;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null; 
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }
         public EditModuloVM(ModuloSUBLIST3R mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null; 
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = mod;
            SQLMAP = null;
            WIFITE = null; 
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }
         public EditModuloVM(ModuloWAPITI mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
             WPSCAN = null;
            WAPITI= mod;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;  
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }
          public EditModuloVM(ModuloSQLMAP mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null; 
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = mod; 
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }

          public EditModuloVM(ModuloWIFITE mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null; 
            WIFITE = mod; 
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }

          public EditModuloVM(ModuloWPSCAN mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = mod;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null; 
            WIFITE = null;
            FIERCE = null; 
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }

          public EditModuloVM(ModuloFIERCE mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null; 
            WIFITE = null;
            FIERCE = mod; 
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
        }

          public EditModuloVM(ModuloOPENDOOR mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null; 
            WIFITE = null;
            FIERCE = null; 
            OPENDOOR = mod;
            WASCAN = null;
            NOSQL = null;
        }
          public EditModuloVM(ModuloWASCAN mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null; 
            WIFITE = null;
            FIERCE = null; 
            OPENDOOR = null;
            WASCAN = mod;
            NOSQL = null;
        }
          public EditModuloVM(ModuloNOSQL mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI= null;
            INFOGAEMAIL = null;
            SUBLIST3R = null; 
            SQLMAP = null; 
            WIFITE = null;
            FIERCE = null; 
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = mod;
        }
    }
}
