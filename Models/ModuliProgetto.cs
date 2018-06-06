using System;
using System.Collections.Generic;

namespace webmva.Models
{
    /// <summary>
    /// Questo modulo rappresenta le singole associazioni tra un progetto e i suoi moduli.
    /// </summary>
    public class ModuliProgetto
    {
        public int ID { get; set; }
        public int ModuloID { get; set; }
        public int ProgettoID { get; set; }
        public string Target { get; set; }
        public Progetto Progetto { get; set; }
        public Modulo Modulo { get; set; }
    }
}