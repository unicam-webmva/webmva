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

        public EditModuloVM()
        {
            NMAP = new ModuloNMAP();
            NESSUS = new ModuloNESSUS();
        }
        public EditModuloVM(ModuloNMAP mod)
        {
            NMAP = mod;
            NESSUS = null;
        }
        public EditModuloVM(ModuloNESSUS mod)
        {
            NMAP = null;
            NESSUS = mod;
        }
    }
}
