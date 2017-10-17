using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Ejecucion
{
    class Archivo
    {
        public String Ruta { get; set; }

        public List<String> Imports;

        public List<Simbolo> Clases;
    }
}
