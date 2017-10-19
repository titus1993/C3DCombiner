using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FLlamadaObjeto
    {
        public FLlamadaObjeto Hijo;
        public String Tipo, Nombre;
        public int Fila, Columna;
        public FLlamadaMetodo LlamadaMetodo;
        public FLlamadaArreglo LlamadaArreglo;
        public FLlamadaMetodoArrelgoTree LlamadaMetodoArreglo;

        public Simbolo Padre;

        public FLlamadaObjeto(String Tipo, String nombre, int fila, int columna, Object valor)
        {
            this.Tipo = Tipo;
            this.Nombre = nombre;
            this.Fila = fila;
            this.Columna = columna;
            this.LlamadaMetodo = null;
            this.LlamadaArreglo = null;
            this.Padre = null;

            
            if (Tipo.Equals(Constante.LLAMADA_ARREGLO))
            {
                this.LlamadaArreglo = (FLlamadaArreglo)valor;
            }
            else if (Tipo.Equals(Constante.LLAMADA_METODO))
            {
                this.LlamadaMetodo = (FLlamadaMetodo)valor;
            }else if (Tipo.Equals(Constante.LLAMADA_METODO_ARREGLO))
            {
                this.LlamadaMetodoArreglo = (FLlamadaMetodoArrelgoTree)valor;
            }

            this.Hijo = null;
        }

        public void InsertarHijo(FLlamadaObjeto hijo)
        {
            if (this.Hijo == null)
            {
                this.Hijo = hijo;
            }
            else
            {
                this.Hijo.InsertarHijo(hijo);
            }
        }
        
    }
}
