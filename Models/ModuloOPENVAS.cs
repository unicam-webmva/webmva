using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace webmva.Models
{
    public class ModuloOPENVAS : Modulo
    {
        public string ServerIPOpenvas { get; set; }
        private int _portaOpenvas = 9391;
        public int PortaOpenvas
        {
            get { return _portaOpenvas; }
            set { _portaOpenvas = value; }
        }
        public override string Comando
        {
            get
            {
                return "https://" + ServerIPOpenvas + ":" + PortaOpenvas;
            }
        }
    }
}