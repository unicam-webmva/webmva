using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webmva.Models
{
    /// <summary>
    /// Questo modello rappresenta le entità Report
    /// </summary>
    public class Report
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; internal set; }        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd-HH-mm-ss}", ApplyFormatInEditMode = true)]
       
       
        public int ProgettoID { get; set; }
        public Progetto Progetto { get; set; }
        public IEnumerable<PercorsiReport> Percorsi { get; set; }

    }
}