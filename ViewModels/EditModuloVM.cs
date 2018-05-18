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

        public EditModuloVM()
        {
            NMAP = new ModuloNMAP();
            NESSUS = new ModuloNESSUS();
            DNSRECON = new ModuloDNSRECON();
            DROOPE = new ModuloDROOPE();
            INFOGA = new ModuloINFOGA();

        }
        public EditModuloVM(ModuloNMAP mod)
        {
            NMAP = mod;
            NESSUS = null;
            DNSRECON = null;
            DROOPE = null;
            INFOGA = null;
        }
        public EditModuloVM(ModuloNESSUS mod)
        {
            NMAP = null;
            DNSRECON = null;
            NESSUS = mod;
            DROOPE = null;
            INFOGA = null;
        }
        public EditModuloVM(ModuloDNSRECON mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = mod;
            DROOPE = null;
            INFOGA = null;
        }
        public EditModuloVM(ModuloDROOPE mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = mod;
        }
        public EditModuloVM(ModuloINFOGA mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = mod;
            DROOPE = null;
        }
    }
}
