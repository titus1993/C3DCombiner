using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FSino
    {
        public Ambito Ambito;

        public Simbolo Padre;

        public FSino(Ambito Ambito)
        {
            this.Ambito = Ambito;
            this.Padre = null;
        }
    }
}
