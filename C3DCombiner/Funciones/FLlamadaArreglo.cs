using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FLlamadaArreglo
    {
        public String Nombre;
        public int Fila, Columna;
        public List<FNodoExpresion> Dimensiones;

        public Simbolo Padre;

        public FLlamadaArreglo(String nombre, List<FNodoExpresion> dimensiones, int fila, int columna)
        {
            this.Nombre = nombre;
            this.Dimensiones = dimensiones;
            this.Fila = fila;
            this.Columna = columna;
            this.Padre = null;
        }
    }
}
