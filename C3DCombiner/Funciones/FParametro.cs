using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FParametro
    {
        public int Dimensiones;
        public String Tipo;
        public String Nombre;
        public int Fila;
        public int Columna;

        public Simbolo Padre;
            
        public FParametro(String Tipo, String Nombre, int Dimensiones, int Fila, int Columna)
        {
            this.Tipo = Tipo;
            this.Nombre = Nombre;
            this.Dimensiones = Dimensiones;
            this.Fila = Fila;
            this.Columna = Columna;
            this.Padre = null;
        }
    }
}
