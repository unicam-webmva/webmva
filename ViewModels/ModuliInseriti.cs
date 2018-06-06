using webmva.Models;
using System.Collections.Generic;
namespace webmva.ViewModels
{
    public class ModuliInseriti {
        public Progetto Progetto {get; set;}
        public List<ModuliInProgetto> ListaModuliConTarget {get; set;}

    }
    public class ModuliInProgetto{

        public int ModuloID{get; set;}
        public string Nome{get; set;}
        public string Comando {get; set;}
        public APPLICAZIONE Applicazione {get; set;}
        public string Target {get; set;}
        public bool Inserito {get; set;}
    }
}