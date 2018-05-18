using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public enum CMS{
        DRUPAL, JOOMLA, MOODLE, SILVERSTRIPE, WORDPRESS, NESSUNO
    }
    public enum CHECKS{
         PLUGIN, THEME, VERSION, INTERESTING, NESSUNO
     }

    public class ModuloDROOPE : Modulo
    {
        public string URL { get; set; } 
private CMS _cms = CMS.NESSUNO;

private CHECKS _check= CHECKS.NESSUNO;
    

        public CMS cms{get {return _cms;}
    set{_cms=value;}}
        public CHECKS check{get {return _check;}
    set{_check=value;}}
        

        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    string controlloCMS= "";
                    string controlloCheck="";
                    // prova
                    switch (cms)
                    {
                    case CMS.DRUPAL:
                        controlloCMS="drupal";
                        break;
                    case  CMS.JOOMLA: 
                        controlloCMS="joomla";
                        break;
                    case  CMS.MOODLE : 
                        controlloCMS="moodle";
                        break;
                    case  CMS.SILVERSTRIPE : 
                        controlloCMS="silverstripe";
                        break;
                    case  CMS.WORDPRESS : 
                        controlloCMS="wordpress";
                        break;
                     default: 
                        controlloCMS="";
                        break;
                    }
                    
                    switch (check)
                    {
                    case  CHECKS.PLUGIN:  
                          controlloCheck =" -e p";
                          break;
                    case  CHECKS.THEME:  
                          controlloCheck =" -e t";
                          break;
                    case  CHECKS.VERSION:  
                          controlloCheck =" -e v";
                          break;
                    case  CHECKS.INTERESTING:  
                          controlloCheck =" -e i";
                          break;
                    
                     default: 
                          controlloCheck="";
                          break;
                    }
                    string risultato = "droopescan scan " + controlloCMS +" -u "+ URL + controlloCheck ;

                   
                   
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}