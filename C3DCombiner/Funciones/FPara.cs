using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FPara
    {

        public Simbolo AccionAnterior;
        public Ambito Ambito;
        public FNodoExpresion Condicion;
        public FNodoExpresion AccionSiguiente;

        public Simbolo Padre;

        public FPara(Simbolo AccionAnterior, FNodoExpresion Condicion, FNodoExpresion AccionSiguiente, Ambito Ambito)
        {
            this.AccionAnterior = AccionAnterior;
            this.Condicion = Condicion;
            this.AccionSiguiente = AccionSiguiente;
            this.Ambito = Ambito;
            this.Padre = null;
        }
    }
} 
