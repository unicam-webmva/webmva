using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
   
    public class ModuloAMASS : Modulo
    {
       
    private bool _allTheH = true; 
    public bool AllTheH { get {return _allTheH;} set {_allTheH = value;} } 
       
        public bool NoDnsAmass {get;set;}
        public bool IpAmass {get;set;}
        public bool NoAltsAmass {get;set;}
         public bool ActiveAmass {get;set;}
        public bool BruteAmass{get;set;}
        public bool WhoisAmass {get;set;}
        public bool VerboseAmass {get;set;}
       
        public int NumberOfFrequences
        {
            get { return _numberOfFrequences; }
            set { _numberOfFrequences = value; }
        }
        private int _numberOfFrequences = -1;
        public string PorteAmass { get; set; }
        public string  BlacklistAmass{ get; set; }
        public string BlacklistSubdomainAmass { get; set; }
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {

                    string risultato = "amass ";
                 
                    if (_numberOfFrequences != -1 && _numberOfFrequences > 0)
                    {
                        risultato += " -freq " + NumberOfFrequences;
                    }
                    if (!string.IsNullOrEmpty(BlacklistAmass)) 
                    risultato += " -bl" + BlacklistAmass;
                    if (!string.IsNullOrEmpty(BlacklistSubdomainAmass)) 
                    risultato += " -blf" + BlacklistSubdomainAmass;
                    if(!string.IsNullOrEmpty(PorteAmass))
                    risultato += " -p " + PorteAmass;  
                    if (NoDnsAmass)
                        risultato += " -nodns";
                    if (IpAmass)
                        risultato += " -ip";
                    if(NoAltsAmass)
                     risultato += " -noalts";
                    if(ActiveAmass)
                     risultato += " -active";  
                    if(BruteAmass)
                     risultato += " -brute"; 
                    if(WhoisAmass)
                     risultato += " -whois";  
                    if(VerboseAmass)
                     risultato += " -v";
                     return risultato;
                    
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}
