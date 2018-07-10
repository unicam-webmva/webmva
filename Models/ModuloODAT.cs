using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public enum VERBOSEODAT
    {
        UNO, DUE, TRE
    }
    public class ModuloODAT : Modulo
    {
        private VERBOSEODAT _verboseOdat = VERBOSEODAT.UNO;
        public VERBOSEODAT VerboseOdat
        {
            get
            {
                return _verboseOdat;
            }
            set
            {
                _verboseOdat = value;
            }
        }
        private int _portaOdat = 1521;
        public int PortaOdat
        {
            get
            {
                return _portaOdat;
            }
            set
            {
                _portaOdat = value;
            }
        }
        public string PasswordOdat { get; set; }
        private bool _allOdat = true;
        public bool AllOdat
        {
            get
            {
                return _allOdat;
            }
            set
            {
                _allOdat = value;
            }
        }
        public bool Tnspoison { get; set; }
        public bool Tnscmd { get; set; }
        public bool SmbOdat { get; set; }
        public bool PasswordStealer { get; set; }
        public bool PasswordGuesser { get; set; }
        public string UtenteOdat { get; set; }
        public string SID { get; set; }
        public bool TestOdat { get; set; }
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    string risultato = "odat.py";

                    if (PortaOdat != 1521 && PortaOdat > 0)
                        risultato += " -p" + PortaOdat;
                    if (!string.IsNullOrEmpty(PasswordOdat))
                        risultato += " -P" + PasswordOdat;
                    if (!string.IsNullOrEmpty(SID))
                        risultato += " -d" + SID;
                    if (!string.IsNullOrEmpty(UtenteOdat))
                        risultato += " -U" + UtenteOdat;
                    if (AllOdat)
                        risultato += " all";
                    if (Tnspoison)
                        risultato += " tnspoison";
                    if (Tnscmd)
                        risultato += " tnscmd";
                    if (SmbOdat)
                        risultato += " smb";
                    if (PasswordStealer)
                        risultato += " passwordstealer";
                    if (PasswordGuesser)
                        risultato += " passwordguesser";
                    if (TestOdat)
                        risultato += " --test-module";
                    switch (VerboseOdat)
                    {
                        case VERBOSEODAT.DUE:
                            risultato += " -vv";
                            break;
                        case VERBOSEODAT.TRE:
                            risultato += " -vvv";
                            break;
                        default:
                            risultato += " -v";
                            break;
                    }
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }
        }
    }
}