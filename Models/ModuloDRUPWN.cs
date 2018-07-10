using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public class ModuloDRUPWN : Modulo
    {
        public bool UsersDrupwn { get; set; }
        public bool NodesDrupwn { get; set; }
        public bool ModulesDrupwn { get; set; }
        public bool DFilesDrupwn { get; set; }
        public bool ThemesDrupwn { get; set; }
        private int _thread = -1;
        public int ThreadDrupwn
        {
            get
            {
                return _thread;
            }
            set
            {
                _thread = value;
            }
        }
        public string UserAgentDrupwn { get; set; }
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    string risultato = "drupwn enum ";
                    if (UsersDrupwn)
                        risultato += "--users ";
                    if (NodesDrupwn)
                        risultato += "--nodes ";
                    if (ModulesDrupwn)
                        risultato += "--modules ";
                    if (DFilesDrupwn)
                        risultato += "--dfiles ";
                    if (ThemesDrupwn)
                        risultato += "--themes ";
                    if (ThreadDrupwn != -1 && ThreadDrupwn > 0)
                        risultato += "--thread " + ThreadDrupwn + " ";
                    if (!string.IsNullOrEmpty(UserAgentDrupwn))
                        risultato += "--ua " + UserAgentDrupwn + " ";
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }
        }
    }
}