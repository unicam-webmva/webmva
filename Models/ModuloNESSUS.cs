using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace webmva.Models
{

    public class ModuloNESSUS : Modulo
    {
        public string ServerIP { get; set; }
        private int _porta = 8834;
        public int Porta
        {
            get { return _porta; }
            set { _porta = value; }
        }
        

        public override string Comando
        {
            // DALLA PROSSIMA VERSIONE DI NESSUS LE API PER CREARE GLI SCAN DA REMOTO
            // NON ESISTERANNO PIU', QUINDI ABBIAMO DECISO DI NON IMPLEMENTARE QUESTA
            // FUNZIONALITA'.
            // FONTE: https://www.tenable.com/products/nessus/nessus-answers
            // Data: 22/05/2018
            get
            {
                return "https://" + ServerIP + ":" + Porta;
            }
        }
    }
}