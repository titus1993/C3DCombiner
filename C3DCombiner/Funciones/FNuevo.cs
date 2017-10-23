using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FNuevo
    {
        public String Nombre;
        public int Fila, Columna;
        public List<FNodoExpresion> Parametros;

        public Simbolo Padre;

        public FNuevo(String nombre, List<FNodoExpresion> parametros, int fila, int columna)
        {
            this.Nombre = nombre;
            this.Parametros = parametros;
            this.Fila = fila;
            this.Columna = columna;
            this.Padre = null;
        }
    }
}
