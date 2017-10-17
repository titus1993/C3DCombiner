using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Ejecucion
{
    class Simbolo
    {
        public int Fila, Columna, Posicion, Tamaño;
        public String Nombre, Rol, Tipo, Visibilidad;
        public Ambito Ambito = null;
        public Simbolo Hermano = null;
        public Object Valor = null;


        public Simbolo(String visibilidad, String tipo, String nombre, String rol, int fila, int columna, Ambito ambito, Object valor)
        {
            this.Visibilidad = visibilidad;
            this.Tipo = tipo;
            this.Nombre = nombre;
            this.Rol = rol;
            this.Fila = fila;
            this.Columna = columna;
            this.Ambito = ambito;
            this.Valor = valor;
            this.Tamaño = this.Ambito.Tamaño;

        }
    }
}
