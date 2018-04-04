using System;
using System.Collections.Generic;

namespace webmva.Models
{
    /// <summary>
    /// Questo modello rappresenta le entità Progetto
    /// </summary>
    public class Progetto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public ICollection<ModuliProgetto> ModuliProgetto {get; set;}
        
    }
}