using System;
using System.Collections.Generic;

namespace webmva.Models
{
    /// <summary>
    /// Questo modulo rappresenta le singole associazioni tra un report e i suoi percorsi.
    /// </summary>
    public class PercorsiReport
    {
        public int ID { get; set; }
        public int ReportID { get; set; }
        public Report Report { get; set; }
        public string Percorso { get; set; }
    }
}