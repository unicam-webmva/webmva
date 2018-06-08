using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public enum SCOPE
    {
        PAGE, FOLDER, DOMAIN, URL, NESSUNO
    }
    public enum FORCE
    {
        PARANOID, SNEAKY, POLITE, AGGRESSIVE, INSANE, NORMAL
    }
    public enum VERBOSEWAPITI
    {
        ZERO, UNO, DUE
    }
    public class ModuloWAPITI : Modulo
    {
       

        public bool All { get; set; }
        private bool _common = true;
        public bool Common
        {
            get { return _common; }
            set { _common = value; }
        }
        private bool _nessuno = false;
        public bool Nessuno
        {
            get { return _nessuno; }
            set { _nessuno = value; }
        }

        public bool BackUp { get; set; }
        public bool BlindSql { get; set; }
        public bool Crlf { get; set; }
        public bool Exec { get; set; }
        public bool File { get; set; }
        public bool Htaccess { get; set; }
        public bool Nikto { get; set; }
        public bool PermanentXss { get; set; }
        public bool Sql { get; set; }
        public bool Xss { get; set; }
        public bool Buster { get; set; }
        public bool ShellShock { get; set; }

        private SCOPE _Scope = SCOPE.NESSUNO;
        public SCOPE Scope
        {
            get { return _Scope; }
            set { _Scope = value; }
        }
        private FORCE _Force = FORCE.NORMAL;
        public FORCE Force
        {
            get { return _Force; }
            set { _Force = value; }
        }
        private VERBOSEWAPITI _Verbose = VERBOSEWAPITI.UNO;
        public VERBOSEWAPITI Verbose
        {
            get { return _Verbose; }
            set { _Verbose = value; }
        }
        public int MaxMinutes
        {
            get { return _max; }
            set { _max = value; }
        }
        private int _max = -1;
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {

                    string risultato = "wapiti --color";
                    if (Nessuno) risultato += " -m \"\"";
                    else if (!All)
                    {
                        risultato += " -m ";
                        // ho visto che con la virgola anche all'inzio funziona
                        if (Common) risultato += ",common";
                        if (BackUp) risultato += ",backup";
                        if (BlindSql) risultato += ",blindsql";
                        if (Crlf) risultato += ",crlf";
                        if (Exec) risultato += ",exec";
                        if (File) risultato += ",file";
                        if (Htaccess) risultato += ",htaccess";
                        if (Nikto) risultato += ",nikto";
                        if (PermanentXss) risultato += ",permanentxss";
                        if (Sql) risultato += ",sql";
                        if (Xss) risultato += ",xss";
                        if (Buster) risultato += ",buster";
                        if (ShellShock) risultato += ",shellshock";
                    }
                    if (_max != -1)
                    {
                        risultato += " --max-scan-time " + MaxMinutes;
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
