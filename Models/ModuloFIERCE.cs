using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public class ModuloFIERCE : Modulo{
    
        public string DnServer { get; set; }
        public string SubDomain { get; set; }
        public bool Connect { get; set; }
        public bool Wide { get; set; }
        

        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                   
                    string risultato ="fierce.py";
                    if (!string.IsNullOrEmpty(DnServer)) 
                    risultato += " --dns-servers" + DnServer;
                    if (!string.IsNullOrEmpty(SubDomain)) 
                    risultato += " --subdomains" + SubDomain;
                    if (Connect) risultato += " --connect";
                    if (Wide) risultato += " --wide";
                    
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}
