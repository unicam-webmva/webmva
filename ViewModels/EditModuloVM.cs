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
        public ModuloDROOPE DROOPE{get; set;}
        public ModuloINFOGA INFOGA{get; set;}
         public ModuloINFOGAEMAIL INFOGAEMAIL{get; set;}
        public ModuloWAPITI WAPITI{get; set;}

        public EditModuloVM()
        {
            NMAP = new ModuloNMAP();
            NESSUS = new ModuloNESSUS();
            DNSRECON = new ModuloDNSRECON();
            DROOPE = new ModuloDROOPE();
            INFOGA = new ModuloINFOGA();
            INFOGAEMAIL = new ModuloINFOGAEMAIL();
            WAPITI = new ModuloWAPITI();

        }
        public EditModuloVM(ModuloNMAP mod)
        {
            NMAP = mod;
            NESSUS = null;
            DNSRECON = null;
            DROOPE = null;
            INFOGA = null;
            WAPITI= null;
            INFOGAEMAIL = null;
        }
        public EditModuloVM(ModuloNESSUS mod)
        {
            NMAP = null;
            DNSRECON = null;
            NESSUS = mod;
            DROOPE = null;
            INFOGA = null;
            WAPITI= null;
            INFOGAEMAIL = null;
        }
        public EditModuloVM(ModuloDNSRECON mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = mod;
            DROOPE = null;
            INFOGA = null;
            WAPITI= null;
            INFOGAEMAIL = null;
        }
        public EditModuloVM(ModuloDROOPE mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = mod;
            WAPITI= null;
            INFOGAEMAIL = null;
        }
        public EditModuloVM(ModuloINFOGA mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = mod;
            DROOPE = null;
            WAPITI= null;
            INFOGAEMAIL = null;
        }
         public EditModuloVM(ModuloINFOGAEMAIL mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            WAPITI= null;
            INFOGAEMAIL = mod;
        }
         public EditModuloVM(ModuloWAPITI mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            WAPITI= mod;
            INFOGAEMAIL = null;
        }
    }
}
