using System;
using System.Collections.Generic;

namespace webmva.Models
{
    /// <summary>
    /// Questo modello rappresenta le entità Report
    /// </summary>
    public class Report
    {
        public int ID { get; set; }
        public int ProgettoID { get; set; }
        public Progetto Progetto { get; set; }
        public List<string> Percorsi { get; set; }

    }
}