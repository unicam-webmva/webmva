using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    

    public class ModuloJOOMSCAN : Modulo
    {
       
        public string ComandoPersonalizzato { get; set; }
        public string UserAgent{get;set;}
         public bool EnumerateComponents { get; set; }
        public override string Comando
        {
            get
            {
                string risultato= "joomscan.pl";
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                if(EnumerateComponents)
                   risultato +=" -ec";                
                if (!string.IsNullOrEmpty(UserAgent))  
                risultato +=" -a " + UserAgent;


                
                   
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}