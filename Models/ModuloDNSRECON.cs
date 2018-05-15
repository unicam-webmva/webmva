using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public class ModuloDNSRECON : Modulo
    {
        public string Dominio { get; set; }
        public string NameServer { get; set; }
        public bool AXFREnum { get; set; }
        public bool ReverseLookupEnum { get; set; }
        public bool GoogleEnum { get; set; }
        public bool BingEnum { get; set; }
        public bool CrtShEnum { get; set; }
        public bool DeepWhois { get; set; }
        public bool ZoneWalk { get; set; }

        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    // cominciamo
                    string risultato = Globals.PYTHON + " dnsrecon.py -d " + Dominio;
                    if (!string.IsNullOrEmpty(NameServer)) risultato += " -n " + NameServer;
                    if (AXFREnum) risultato += " -a";
                    if (ReverseLookupEnum) risultato += " -s";
                    if (GoogleEnum) risultato += " -g";
                    if (BingEnum) risultato += " -b";
                    if (CrtShEnum) risultato += " -k";
                    if (DeepWhois) risultato += " -w";
                    if (ZoneWalk) risultato += " -z";
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}
