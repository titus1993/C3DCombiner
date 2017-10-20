using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FElegir
    {
        public FNodoExpresion Condicion;
        public Ambito Ambito;
        public List<FCaso> Casos;
        public FCaso Defecto;

        public Simbolo Padre;

        public FElegir(FNodoExpresion condicion, Ambito ambito,  List<FCaso> casos, FCaso defecto)
        {
            this.Casos = casos;
            this.Defecto = defecto;
            this.Ambito = ambito;
            this.Condicion = condicion;
        }
    }
}
