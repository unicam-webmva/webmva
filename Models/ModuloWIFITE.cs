using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{    public class ModuloWIFITE : Modulo
    {
       
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {

                    string risultato = "wifite.py" ;
                   
                    
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}
