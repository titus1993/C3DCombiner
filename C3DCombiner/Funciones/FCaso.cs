using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FCaso
    {
        public FNodoExpresion Valor;
        public Ambito Ambito;

        public Simbolo Padre;

        public FCaso(FNodoExpresion valor, Ambito ambito)
        {
            this.Valor = valor;
            this.Ambito = ambito;
        }
        public FCaso(Ambito ambito)
        {
            this.Ambito = ambito;
        }
    }
}
