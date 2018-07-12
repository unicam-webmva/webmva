using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace webmva.Models
{
    public class ModuloSERVER : Modulo
    {
        private bool _https;

        public string ServerIP { get; set; }
        public int Porta { get; set; }
        public bool Https { get => _https; set => _https = value; }
        public override string Comando
        {
            get
            {
                return "https://" + ServerIP + ":" + Porta;
            }
        }
    }
}