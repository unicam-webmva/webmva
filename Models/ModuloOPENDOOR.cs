using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public class ModuloOPENDOOR : Modulo
    {

        public int PortaO
        {
            get
            {
                return _porta;
            }
            set
            {
                _porta = value;
            }
        }

        private int _porta = 80;
        public int RetriesO
        {
            get
            {
                return _retries;
            }
            set
            {
                _retries = value;
            }
        }

        private int _retries = 3;
        public string Metodo
        {
            get
            {
                return _metodo;
            }
            set
            {
                _metodo = value;
            }
        }
        private string _metodo = "HEAD";
        public int DelayO { get; set; }
        public int TimeoutO
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }
        private int _timeout = 30;
        public bool AcceptCookies{get;set;}
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {

                    string risultato = "opendoor.py";
                    if (PortaO != 80 && PortaO > 0)
                        risultato += "-p" + PortaO;
                    if (string.IsNullOrEmpty(_metodo))
                        risultato += " -m" + _metodo;
                    if (DelayO > 0 && DelayO != 30)
                        risultato += " -d" + DelayO;
                    if(RetriesO !=3 && RetriesO>0)
                        risultato += " -r" + RetriesO;
                    if(TimeoutO !=30 && TimeoutO>0)
                        risultato += " ---timeout" + TimeoutO;
                        if(AcceptCookies)
                        risultato += " --accept-cookies";
                

                return risultato;
            }
                else return ComandoPersonalizzato;
        }

    }
    }
}

