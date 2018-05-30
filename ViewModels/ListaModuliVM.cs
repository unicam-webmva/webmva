﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webmva.Models;

namespace webmva.ViewModels
{
    public class ListaModuliVM
    {
        public IEnumerable<Modulo> ModuliNMAP { get; set; }
        public IEnumerable<Modulo> ModuliNESSUS { get; set; }
        public IEnumerable<Modulo> ModuliDNSRECON { get; set; }
        public IEnumerable<Modulo> ModuliFIERCE { get; set; }
         public IEnumerable<Modulo> ModuliOPENDOOR { get; set; }
        public IEnumerable<Modulo> ModuliDROOPE { get; set; }
        public IEnumerable<Modulo> ModuliJOOMSCAN { get; set; }
        public IEnumerable<Modulo> ModuliWPSCAN { get; set; }
        public IEnumerable<Modulo> ModuliWASCAN { get; set; }
        public IEnumerable<Modulo> ModuliINFOGA { get; set; }
        public IEnumerable<Modulo> ModuliINFOGAEMAIL { get; set; }
        public IEnumerable<Modulo> ModuliSUBLIST3R { get; set; }
        public IEnumerable<Modulo> ModuliWAPITI { get; set; }
        public IEnumerable<Modulo> ModuliSQLMAP { get; set; }
        public IEnumerable<Modulo> ModuliWIFITE { get; set; }
        


    }
}
