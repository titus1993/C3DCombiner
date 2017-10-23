using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FSuper
    {
        public List<FNodoExpresion> Parametros;

        public Simbolo Padre;

        public FSuper(List<FNodoExpresion> Parametros)
        {
            this.Parametros = Parametros;
            this.Padre = null;
        }
    }
}
