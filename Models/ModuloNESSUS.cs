using System;
namespace webmva.Models
{
    public class ModuloNESSUS : Modulo
    {
        // PROVVISORIO!
        public string JSON {get; set;}
        public override string Comando {get{return JSON;}}
    }
}