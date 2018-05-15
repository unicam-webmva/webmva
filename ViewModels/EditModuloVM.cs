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

        public EditModuloVM()
        {
            NMAP = new ModuloNMAP();
            NESSUS = new ModuloNESSUS();
            DNSRECON = new ModuloDNSRECON();
        }
        public EditModuloVM(ModuloNMAP mod)
        {
            NMAP = mod;
            NESSUS = null;
            DNSRECON = null;
        }
        public EditModuloVM(ModuloNESSUS mod)
        {
            NMAP = null;
            DNSRECON = null;
            NESSUS = mod;
        }
        public EditModuloVM(ModuloDNSRECON mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = mod;
        }
    }
}
