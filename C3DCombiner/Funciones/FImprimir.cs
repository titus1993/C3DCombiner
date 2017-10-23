using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FImprimir
    {
        public FNodoExpresion Valor;
        public Simbolo Padre;

        public FImprimir(FNodoExpresion Valor)
        {
            this.Valor = Valor;
        }
    }
}
