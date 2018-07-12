using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{


    public class ModuloWHOIS : Modulo
    {
        private int _portaWhois = -1;

        public string ComandoPersonalizzato { get; set; }
        public string HostWhois { get; set; }
        public int PortaWhois { get => _portaWhois; set => _portaWhois = value; }
        public bool HideLegalWhois { get; set; }
        public bool SearchAllWhois { get; set; }
        public bool VerboseWhois { get; set; }
        public override string Comando
        {
            get
            {
                string risultato = "whois";
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    if (!string.IsNullOrEmpty(HostWhois))
                        risultato += " -h " + HostWhois;
                    if (PortaWhois > 0)
                        risultato += " -p " + PortaWhois;
                    if(HideLegalWhois)
                        risultato += " -H";
                    if(SearchAllWhois)
                        risultato += " -a";
                    if(VerboseWhois)
                        risultato += " --verbose";
                    
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }
        }
    }
}