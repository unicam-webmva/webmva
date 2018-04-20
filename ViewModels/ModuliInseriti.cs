using webmva.Models;
using System.Collections.Generic;
namespace webmva.ViewModels
{
    public class ModuliInseriti {
        public int ModuloID{get; set;}
        public string Nome{get; set;}
        public string Comando {get; set;}
        public APPLICAZIONE Applicazione {get; set;}
        public bool Inserito {get; set;}

    }
}