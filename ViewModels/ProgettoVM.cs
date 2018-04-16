using webmva.Models;
using System.Collections.Generic;
namespace webmva.ViewModels
{
    public class ProgettoVM {
        public Progetto Progetto {get; set;}
        public IEnumerable<Modulo> TuttiModuli {get; set;}
    }
}