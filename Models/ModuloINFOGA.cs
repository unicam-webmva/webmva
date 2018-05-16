using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
 public enum SOURCE{
         ALL, GOOGLE, BING, YAHOO, ASK, BAIDU, DOGPILE, EXALEAD, PGP
     }
     public enum VERBOSE{
        UNO, DUE, TRE, NESSUNA
     }
    public class ModuloINFOGA : Modulo
    {
        public string Dominio { get; set; }
        public string Email { get; set; }
        public bool Info{get; set;}
        public bool Breach{get; set;}
       
        private SOURCE _source= SOURCE.ALL;
        public SOURCE source{get {return _source;}
    set{ _source=value;}}
    
 private VERBOSE _verbose= VERBOSE.NESSUNA;
        public VERBOSE verbose{get {return _verbose;}
    set{ _verbose=value;}}
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    
                    string risultato ="infoga.py" ;
                    string controlloSource="";
                    // prova
                    switch (source)
                    {
                    case SOURCE.GOOGLE:
                         controlloSource=" -s google";
                        break;
                    case SOURCE.BING:
                         controlloSource=" -s bing";
                        break;
                    case SOURCE.YAHOO:
                         controlloSource=" -s yahoo";
                        break;
                    case SOURCE.ASK:
                         controlloSource=" -s ask";
                        break;
                    case SOURCE.BAIDU:
                         controlloSource=" -s baidu";
                        break;
                    case SOURCE.DOGPILE:
                         controlloSource=" -s dogpile";
                        break;
                    case SOURCE.EXALEAD:
                         controlloSource=" -s exalead";
                        break;  
                    case SOURCE.PGP:
                         controlloSource=" -s pgp";
                        break;          
                    default: 
                        controlloSource="";
                        break;
                    }
                     string controlloVerbose="";
                    // prova
                    switch (verbose)
                    {
                    case VERBOSE.UNO:
                         controlloVerbose=" -v 1";
                        break;
                    case VERBOSE.DUE:
                         controlloVerbose=" -v 2";
                        break;    
                    case VERBOSE.TRE:
                         controlloVerbose=" -v 3";
                        break;      
                    default: 
                        controlloVerbose="";
                        break;
                    }
                    if(Info){
                    risultato+=" -i "+ Email;
                    if(Breach)
                    risultato += " -b";
                    }else{
                        risultato += " -d " + Dominio;
                        risultato += controlloSource;
                    }
                    risultato += controlloVerbose;
                   
                   
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}