using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
 public enum SOURCE{
        GOOGLE, BING, YAHOO, ASK, BAIDU, DOGPILE, EXALEAD, PGP, ALL
     }
     public enum VERBOSE{
        UNO, DUE, TRE, NESSUNA
     }
    public class ModuloINFOGA : Modulo
    {
        public string Dominio { get; set; }
        
        public string Email { get; set; }
        
        public bool Breach { get; set; }
       
        private SOURCE _source= SOURCE.ALL;
        public SOURCE Source{
            get {return _source;}
            set{ _source=value;}
        }
    
        private VERBOSE _verbose= VERBOSE.NESSUNA;
        public VERBOSE Verbose{
            get {return _verbose;}
            set{ _verbose=value;}
        }
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    
                    string risultato ="infoga.py" ;
                    
                    // prova
                    if(!string.IsNullOrEmpty(Email)){
                        risultato+=" -i "+ Email;
                        if(Breach)
                            risultato += " -b";
                    }else{
                        risultato += " -d " + Dominio;
                        // prova
                        switch (Source)
                        {
                            case SOURCE.GOOGLE:
                                risultato += " -s google";
                                break;
                            case SOURCE.BING:
                                risultato += " -s bing";
                                break;
                            case SOURCE.YAHOO:
                                risultato += " -s yahoo";
                                break;
                            case SOURCE.ASK:
                                risultato += " -s ask";
                                break;
                            case SOURCE.BAIDU:
                                risultato += " -s baidu";
                                break;
                            case SOURCE.DOGPILE:
                                risultato += " -s dogpile";
                                break;
                            case SOURCE.EXALEAD:
                                risultato += " -s exalead";
                                break;
                            case SOURCE.PGP:
                                risultato += " -s pgp";
                                break;
                            default:
                                break;
                        }
                    }
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


                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}