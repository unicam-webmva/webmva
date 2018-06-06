using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    
    public enum WPS
    {
        NO, ONLY, DEFAULT
    }
    public class ModuloWIFITE : Modulo
    {
        
       
        private bool _client = false;
        public bool client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
            }
        }
        private bool _wep = false;
        public bool wep
        {
            get
            {
                return _wep;
            }
            set
            {
                _wep = value;
            }
        }
        private bool _fakeAutenticazione = false;
        public bool fakeAutenticazione
        {
            get
            {
                return _fakeAutenticazione;
            }
            set
            {
                _fakeAutenticazione = value;
            }
        }
        private bool _keepIvs = false;
        public bool keepIvs
        {
            get
            {
                return _keepIvs;
            }
            set
            {
                _keepIvs = value;
            }
        }
        private bool _newhs = false;
        public bool newHs
        {
            get
            {
                return _newhs;
            }
            set
            {
                _newhs = value;
            }
        }
        public bool verbositaWifite { get; set; }
        public bool wpa { get; set; }
        public bool accessPoint { get; set; }
        public bool crack { get; set; }
        public bool wpsSetting { get; set; }
   
        public bool bully { get; set; }
        public int scanTime { get; set; }
        private int _channel = -1;

        public int channel
        {
            get
            {
                return _channel;
            }
            set
            {
                _channel = value;
            }
        }
       
        private WPS _wps = WPS.DEFAULT;
        public WPS Wps
        {
            get { return _wps; }
            set { _wps = value; }
        }
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {

            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {

                    string risultato = "Wifite.py";
                  
                        switch (Wps)
                        {
                            case WPS.ONLY:
                                risultato += " --wps-only";
                                break;
                            case WPS.NO:
                                risultato += " --no-wps";
                                break;
                            default:
                                break;
                        }
                        
                        if (client)
                            risultato += " -co";
                        if (wep)
                            risultato += " --wep";
                        if (fakeAutenticazione)
                            risultato += " --require-fakeauth";
                        if (keepIvs)
                            risultato += " --keep-ivs";
                        if (wpa)
                            risultato += " --wpa";
                        if (newHs)
                            risultato += " --new-hs";
                        if (wpsSetting)
                            risultato += " --wps";
                        if (accessPoint)
                            risultato += " --cracked";
                        if (crack)
                            risultato += " --crack";
                        if (bully ){
                            risultato += " --bully";
                        if (channel != 0 && channel <= 14)
                            risultato += " -c " + channel;
                        if (scanTime != 0)
                            risultato += " -p " + scanTime;
                        }
                        return risultato;
                    }
                    else return ComandoPersonalizzato;
                }

            }
        }
    }
