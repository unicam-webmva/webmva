using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{

    public enum METHODW
    {
        GET, POST
    }

    public class ModuloWASCAN : Modulo
    {

        public string ComandoPersonalizzato { get; set; }
        public string HeadersW { get; set; }
        public string AutenticazioneW { get; set; }
        public string UserAgentW { get; set; }
        public string CookiesW { get; set; }
        public bool Fingerprint { get; set; } //0
        public bool Attacks { get; set; } //1
        public bool Audit { get; set; } //2
        public bool Bruteforce { get; set; } //3
        public bool Disclosure { get; set; } //4
        private bool _fullScanW = true;
        public bool FullScanW
        {
            get
            {
                return _fullScanW;
            }
            set
            {
                _fullScanW = value;
            }
        }
        public bool ReagentW { get; set; }
        public bool VerbositàW { get; set; }
        private bool _redirectW = false;
        public bool RedirectW
        {
            get
            {
                return _redirectW;
            }
            set
            {
                _redirectW = value;
            }
        }
        private int _timeoutW = 3;
        public int TimeoutW
        {
            get
            {
                return _timeoutW;
            }
            set
            {
                _timeoutW = value;
            }
        }
        private METHODW _methodsW = METHODW.GET;

        public METHODW MethodsW
        {
            get { return _methodsW; }
            set { _methodsW = value; }
        }
        public override string Comando
        {
            get
            {
                string risultato = "wpscan.rb --batch ";
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {

                    
                    {
                        string appoggio = "";
                        if (Fingerprint) appoggio += "0 ";
                        if (Attacks) appoggio += "1 ";
                        if (Audit) appoggio += "2 ";
                        if (Bruteforce) appoggio += "3 ";
                        if (Disclosure) appoggio += "4 ";
                        if(appoggio.Length!=0 && appoggio.Length!=10){
                        string[] appoggio2= appoggio.Split(' ');
                        
                        appoggio = appoggio2[0];
                        for(int i=1; i < appoggio2.Length-1; i++){
                            appoggio += "," +appoggio2[i];
                        }
                        risultato +="-s [" + appoggio + "]";
                        }
                        
                    }
                    switch (MethodsW)
                    {
                        case METHODW.POST:
                            risultato += " -m POST";
                            break;
                        default:
                            risultato += " -m GET";
                            break;

                    }
                    if (!string.IsNullOrEmpty(HeadersW))
                        risultato += " -H" + HeadersW;
                    //(user:pass)
                    if (!string.IsNullOrEmpty(AutenticazioneW))
                        risultato += " -a" + AutenticazioneW;
                    if (!string.IsNullOrEmpty(UserAgentW))
                        risultato += " -A" + UserAgentW;
                    if (!string.IsNullOrEmpty(CookiesW))
                        risultato += " -c" + CookiesW;
                    if (ReagentW)
                        risultato += " -r";
                    if (TimeoutW != 3 && TimeoutW > 0)
                        risultato += " -t" + TimeoutW;
                    if (RedirectW)
                        risultato += " -n";
                    if (VerbositàW)
                        risultato += " -v";
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}