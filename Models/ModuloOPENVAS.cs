using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace webmva.Models
{

    public class ModuloOPENVAS : Modulo
    {
        public string ServerIP { get; set; }
        private int _portaOpenvas = 8834;
        public int Porta
        {
            get { return _portaOpenvas; }
            set { _portaOpenvas = value; }
        }

        public override string Comando
        {
           
            get
            {
                return "https://" + ServerIP + ":" + Porta;
            }
        }
    }
}