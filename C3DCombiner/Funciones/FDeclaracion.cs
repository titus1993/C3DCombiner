using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FDeclaracion
    {
        public Ambito Ambito;
        public List<FNodoExpresion> Dimensiones;
        public String Tipo, Nombre, Visibilidad;
        public FNodoExpresion Valor;
        public int Fila, Columna;

        public Simbolo Padre;

        public FDeclaracion(String visibilidad, String tipo, String nombre, List<FNodoExpresion> dimensiones, Ambito ambito, int fila, int columna, Object valor)
        {
            this.Visibilidad = visibilidad;
            this.Tipo = tipo;
            this.Nombre = nombre;
            this.Dimensiones = dimensiones;
            this.Ambito = ambito;
            this.Padre = null;
            this.Fila = fila;
            this.Columna = columna;           
        }
    }
}
