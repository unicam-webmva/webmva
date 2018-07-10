using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public class ModuloNOSQL : Modulo
    {
        public override string Comando
        {
            get
            {
                return "nosqlmap.py";
            }
        }
    }
}