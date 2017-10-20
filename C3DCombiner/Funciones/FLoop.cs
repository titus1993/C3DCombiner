using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FLoop
    {
        public Ambito Ambito;

        public Simbolo Padre;

        public FLoop(Ambito Ambito)
        {
            this.Ambito = Ambito;
            this.Padre = null;
        }
    }
}
