using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public class ModuloINFOGAEMAIL : Modulo
    {
        public bool Breach { get; set; }
        private VERBOSE _verbose = VERBOSE.NESSUNA;
        public VERBOSE Verbose
        {
            get { return _verbose; }
            set { _verbose = value; }
        }
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    string risultato = "infoga.py";
                    switch (Verbose)
                    {
                        case VERBOSE.UNO:
                            risultato += " -v 1";
                            break;
                        case VERBOSE.DUE:
                            risultato += " -v 2";
                            break;
                        case VERBOSE.TRE:
                            risultato += " -v 3";
                            break;
                        default:
                            risultato += "";
                            break;
                    }
                    if (Breach)
                        risultato += " -b";
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }
        }
    }
}