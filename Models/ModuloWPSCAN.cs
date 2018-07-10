using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public class ModuloWPSCAN : Modulo
    {
        public string ComandoPersonalizzato { get; set; }
        public bool Force { get; set; }
        public bool RandomAgent { get; set; }
        public bool VerbositàWP { get; set; }
        public bool DisableChecks { get; set; }
        public string UserAgent { get; set; }
        public string Cookie { get; set; }
        public override string Comando
        {
            get
            {
                string risultato = "wpscan.rb --batch ";
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    if (Force)
                        risultato += " -f";
                    if (RandomAgent)
                        risultato += " -r";
                    if (VerbositàWP)
                        risultato += " -v";
                    if (DisableChecks)
                        risultato += " --disable-tls-checks";
                    if (!string.IsNullOrEmpty(Cookie))
                        risultato += " -cookie " + Cookie;
                    if (!string.IsNullOrEmpty(UserAgent))
                        risultato += " -a " + UserAgent;
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }
        }
    }
}