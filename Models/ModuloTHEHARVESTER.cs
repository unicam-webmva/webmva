using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{

    public class ModuloTHEHARVESTER : Modulo
    {
        private bool _allTheH = true;
        public bool AllTheH { get { return _allTheH; } set { _allTheH = value; } }
        public bool GoogleTheH { get; set; }
        public bool BingTheH { get; set; }
        public bool BingApiTheH { get; set; }
        public bool PgpTheH { get; set; }
        public bool LinkedinTheH { get; set; }
        public bool GoogleProfilesTheH { get; set; }
        public bool People123TheH { get; set; }
        public bool JigsawTheH { get; set; }
        public bool RicercaHostsVirtualiTheH { get; set; }
        public bool DNSReverseQueryTheH { get; set; }
        public bool DNSBruteForceTheH { get; set; }
        public bool DNSTLDTheH { get; set; }
        public int NumberOfResult
        {
            get { return _numberOfResult; }
            set { _numberOfResult = value; }
        }
        private int _numberOfResult = -1;
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    string risultato = "theharvester ";
                    if (!AllTheH)
                    {
                        string appoggio = " -b ";
                        if (GoogleTheH) appoggio += "google ";
                        if (BingTheH) appoggio += "bing ";
                        if (BingApiTheH) appoggio += "bongapi ";
                        if (PgpTheH) appoggio += "pgp ";
                        if (LinkedinTheH) appoggio += "linkedin ";
                        if (GoogleProfilesTheH) appoggio += "google-profiles ";
                        if (People123TheH) appoggio += "people123 ";
                        if (JigsawTheH) appoggio += "jigsaw ";
                        string[] appoggio2 = appoggio.Split(' ');
                        appoggio = "";
                        for (int i = 0; i < appoggio2.Length - 1; i++)
                        {
                            if (i < 3)
                                appoggio += " " + appoggio2[i];
                            else appoggio += "," + appoggio2[i];
                        }
                        risultato += appoggio;
                    }
                    else risultato += "-b all ";
                    if (_numberOfResult != -1 && _numberOfResult > 0)
                    {
                        risultato += " -l " + NumberOfResult;
                    }
                    if (RicercaHostsVirtualiTheH)
                        risultato += " -v";
                    if (DNSReverseQueryTheH)
                        risultato += " -n";
                    if (DNSBruteForceTheH)
                        risultato += " -c";
                    if (DNSTLDTheH)
                        risultato += " -t";
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }
        }
    }
}
