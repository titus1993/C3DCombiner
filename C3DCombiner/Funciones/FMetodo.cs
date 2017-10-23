using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FMetodo
    {
        public Ambito Ambito;
        public List<Simbolo> Parametros;
        public int Dimensiones;
        public int Fila, Columna;
        public String Tipo, Nombre;
        public String Visibilidad;

        public Simbolo Padre;

        public FMetodo(String visibilidad, String tipo, String nombre, List<Simbolo> parametro, Ambito ambito, int fila, int columna)
        {
            this.Visibilidad = visibilidad;
            this.Ambito = ambito;
            this.Parametros = parametro;
            this.Fila = fila;
            this.Columna = columna;
            this.Tipo = tipo;
            this.Nombre = nombre;
        }

        public FMetodo(String visibilidad, String tipo, int dimensiones, String nombre, List<Simbolo> parametro, Ambito ambito, int fila, int columna)
        {
            this.Visibilidad = visibilidad;
            this.Ambito = ambito;
            this.Parametros = parametro;
            this.Dimensiones = dimensiones;
            this.Fila = fila;
            this.Columna = columna;
            this.Tipo = tipo;
            this.Nombre = nombre;
        }
    }
}
