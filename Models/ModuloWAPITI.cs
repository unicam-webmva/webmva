using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
public enum SCOPE{
        PAGE, FOLDER, DOMAIN, URL, NESSUNO
     }
public enum FORCE{
        PARANOID, SNEAKY, POLITE, AGGRESSIVE, INSANE, NORMAL
     }
public enum VERBOSEWAPITI{
        ZERO, UNO, DUE
     }
    public class ModuloWAPITI : Modulo
    {
        public string URL { get; set; }
        private bool NotAll= false;
        private bool _all = true;
        public bool All{get{return _all;}
        set{
            if(value)
            NotAll=false;
            else
             NotAll=true;
            _all = value;
        }}
        private bool _backUp = false;
        public bool BackUp { get{
            return _backUp;
        } set{
            NotAll=true;
            _backUp=value;
        } }
         private bool _blindSql = false;
        public bool BlindSql { get{
            return _blindSql;
        } set{
            NotAll=true;
            _blindSql=value;
        } }
         private bool _crlf = false;
        public bool Crlf  { get{
            return _crlf;
        } set{
            NotAll=true;
            _crlf=value;
        } }
        private bool _exec = false;
        public bool Exec  { get{
            return _exec;
        } set{
            NotAll=true;
            _exec=value;
        } }
        private bool _file = false;
        public bool File { get{
            return _file;
        } set{
            NotAll=true;
            _file=value;
        } }
        private bool _htAccess = false;
        public bool Htaccess { get{
            return _htAccess;
        } set{
            NotAll=true;
            _htAccess=value;
        } }  
        private bool _nikto = false;      
        public bool Nikto  { get{
            return _nikto;
        } set{
            NotAll=true;
            _nikto=value;
        } }
        private bool _permanentXss = false;     
        public bool PermanentXss { get{
            return  _permanentXss;
        } set{
            NotAll=true;
             _permanentXss=value;
        } }
        private bool _sql = false;     
        public bool Sql  { get{
            return _sql;
        } set{
            NotAll=true;
            _sql=value;
        } }
        private bool _xss = false;  
        public bool Xss  { get{
            return _xss;
        } set{
            NotAll=true;
            _xss=value;
        } }
        private bool _buster = false;  
        public bool Buster  { get{
            return _buster;
        } set{
            NotAll=true;
            _buster=value;
        } }
        private bool _shellshock = false;
        public bool ShellShock  { get{
            return _shellshock;
        } set{
            NotAll=true;
            _shellshock=value;
        } }

      private SCOPE _Scope= SCOPE.NESSUNO;
        public SCOPE Scope{
            get {return _Scope;}
            set{ _Scope=value;}
        }
    private FORCE _Force= FORCE.NORMAL;
        public FORCE Force{
            get {return _Force;}
            set{ _Force=value;}
        }
    private VERBOSEWAPITI _Verbose= VERBOSEWAPITI.UNO;
        public VERBOSEWAPITI Verbose{
            get {return _Verbose;}
            set{ _Verbose =value;}
        }
       public int MaxMinutes{
           get{return _max;}
           set{_max=value;}
       }
       private int _max= -1;
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    
                    string risultato ="wapiti.py" +" --color" ;

                  if (NotAll){
                    risultato+=" -m \"-all";
                   if (BackUp) risultato +=",backup";
                   if (BlindSql) risultato +=",blindsql";
                   if (Crlf) risultato +=",crlf";
                   if (Exec) risultato +=",exec";
                   if (File) risultato +=",file";
                   if (Htaccess) risultato +=",htaccess";
                   if (Nikto) risultato +=",nikto";
                   if (PermanentXss) risultato +=",permanentxss";
                   if (Sql) risultato +=",sql";
                   if (Xss) risultato +=",xss";
                   if (Buster) risultato +=",buster";
                   if (ShellShock) risultato +=",shellshock";
                   risultato+="\"";
                  }
                if(_max!=-1){
                risultato+="--max-scan-time "+MaxMinutes;
                }
                switch (Force)
                    {
                        case FORCE.PARANOID:
                            risultato += " -S paranoid";
                            break;
                        case FORCE.SNEAKY:
                            risultato += " -S sneaky";
                            break;
                        case FORCE.POLITE:
                            risultato += " -S polite";
                            break;
                        case FORCE.AGGRESSIVE:
                            risultato += " -S aggressive";
                            break;
                        case FORCE.INSANE:
                            risultato += " -S insane";
                            break;
                        default: break;
                    }
                switch (Verbose)
                    {
                        case VERBOSEWAPITI.ZERO:
                            risultato += " -v 0";
                            break;
                         case VERBOSEWAPITI.DUE:
                            risultato += " -v 2";
                            break;
                        default: break;
                    }
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}