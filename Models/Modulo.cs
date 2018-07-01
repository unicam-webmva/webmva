using System;
using System.ComponentModel.DataAnnotations;
namespace webmva.Models
{
    public enum APPLICAZIONE
    {
        NMAP, NESSUS, DNSRECON, FIERCE, DROOPE, JOOMSCAN, WPSCAN, OPENDOOR, INFOGA, INFOGAEMAIL, SUBLIST3R, WAPITI, SQLMAP, WIFITE, WASCAN, NOSQL, ODAT, DNSENUM, OPENVAS, THEHARVESTER, AMASS
    }
    /// <summary>
    /// Questo modello rappresenta un Modulo, usabile in più Progetti.
    /// </summary>
    public abstract class Modulo
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public virtual string Comando { get; }
        public APPLICAZIONE Applicazione { get; set; }

    }
}