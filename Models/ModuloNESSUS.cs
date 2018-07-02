using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace webmva.Models
{

    public class ModuloNESSUS : Modulo
    {
        public string ServerIP { get; set; }
        public int Porta {get;set;}
        

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