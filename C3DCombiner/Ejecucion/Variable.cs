using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Ejecucion
{
    class Variable
    {
        public String Rol, Nombre;
        public int Fila, Columna;
        public Ambito Ambito;
        public Object Valor;

        public Variable(String nombre, String rol, int fila, int columna, Ambito ambito, Object valor)
        {
            this.Nombre = nombre;
            this.Rol = rol;
            this.Fila = fila;
            this.Columna = columna;
            this.Ambito = ambito;
            this.Valor = valor;
        }
    }
}
