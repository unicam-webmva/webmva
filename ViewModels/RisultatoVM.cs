using webmva.Models;
using System.Collections.Generic;
namespace webmva.ViewModels
{
    public class RisultatoVM {
        public string NomeProgetto {get; set;}
        // Chiave: nome del modulo, Valore: risultato dello scan
        public IDictionary<string, string> risultati {get; set;}
    }
}