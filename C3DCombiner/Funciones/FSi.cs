using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FSi
    {
        public Ambito Si;
        public List<FSinoSi> SinoSi;
        public FSino Sino;
        public Ambito Ambito;
        public FNodoExpresion Condicion;

        Simbolo Padre;

        public FSi(FNodoExpresion condicion, Ambito ambito, List<FSinoSi> sinosi, FSino sino)
        {
            this.Condicion = condicion;
            this.Ambito = ambito;
            this.SinoSi = sinosi;
            this.Sino = sino;
            this.Padre = null;
        }
    }
}
