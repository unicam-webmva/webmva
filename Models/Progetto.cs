using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace webmva.Models
{
    /// <summary>
    /// Questo modello rappresenta le entità Progetto
    /// </summary>
    public class Progetto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
 
        public string Target{get;set;}
        public ICollection<ModuliProgetto> ModuliProgetto {get; set;}
        public String Descrizione{get; set; }
    }
}