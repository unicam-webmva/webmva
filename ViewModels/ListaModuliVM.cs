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
        public IEnumerable<Modulo> ModuliDROOPE { get; set; }

    }
}
