using System;

namespace webmva.Models
{
    /// <summary>
    /// Summary description for Modulo
    /// </summary>
    public enum Tipo {
        SCRIPT, API
    }
    public class Modulo
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Comando { get; set; }
        public Tipo Tipo {get; set;}
    }
}