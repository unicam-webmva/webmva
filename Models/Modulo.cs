using System;

namespace webmva.Models
{
    /// <summary>
    /// Specifica se questo è un comando da eseguire tramite Script (es. batch) o tramite una chiamata API esterna. 
    /// </summary>
    public enum Tipo {
        SCRIPT, API
    }

    /// <summary>
    /// Applicazioni compatibili con webMVA.
    /// </summary>
    public enum Applicazione {
        NMAP, NESSUS, OPENVAS, OWASP
    }
    /// <summary>
    /// Questo modello rappresenta un Modulo, usabile in più Progetti.
    /// </summary>
    public class Modulo
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Comando { get; set; }
        public Tipo Tipo {get; set;}
        public Applicazione Applicazione {get; set;}
    }
}