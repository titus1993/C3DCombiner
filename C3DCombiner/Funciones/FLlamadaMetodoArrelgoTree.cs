using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FLlamadaMetodoArrelgoTree
    {
        public String Nombre;
        public int Fila, Columna;
        public List<FNodoExpresion> Parametros;

        public Simbolo Padre;

        public FLlamadaMetodoArrelgoTree(String nombre, List<FNodoExpresion> parametros, int fila, int columna)
        {
            this.Nombre = nombre;
            this.Parametros = parametros;
            this.Fila = fila;
            this.Columna = columna;
            this.Padre = null;
        }

        public void setPadre(Simbolo simbolo)
        {
            this.Padre = simbolo;
            foreach (FNodoExpresion nodo in Parametros)
            {
                nodo.SetPadre(simbolo);
            }
        }

        public Nodo3D Generar3D()
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = Padre.BuscarVariable(this.Nombre);

            if (sim != null)//es un arreglo
            {
                FLlamadaArreglo arr = new FLlamadaArreglo(this.Nombre, Parametros, Fila, Columna);
                arr.setPadre(Padre);

                nodo = arr.Generar3D();
            }
            else// es un metodo
            {
                FLlamadaMetodo metodo = new FLlamadaMetodo(this.Nombre, this.Parametros, Fila, Columna);
                metodo.setPadre(Padre);

                nodo = metodo.Generar3D();

                sim = metodo.Encontrado;
            }

            if (sim == null)
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro un arreglo o metodo para ejecutar", TitusTools.GetRuta(), Fila, Columna);
            }

            return nodo;
        }

        public Nodo3D Generar3DHijo(Simbolo padre, String temporal)
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = padre.BuscarVariable(this.Nombre);

            if (sim != null)//es un arreglo
            {
                FLlamadaArreglo arr = new FLlamadaArreglo(this.Nombre, Parametros, Fila, Columna);
                arr.setPadre(Padre);

                nodo = arr.Generar3DHijo(padre, temporal);
            }
            else// es un metodo
            {
                FLlamadaMetodo metodo = new FLlamadaMetodo(this.Nombre, this.Parametros, Fila, Columna);
                metodo.setPadre(Padre);

                nodo = metodo.Generar3DHijo(padre, temporal);

                sim = metodo.Encontrado;
            }

            if (sim == null)
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro un arreglo o metodo para ejecutar", TitusTools.GetRuta(), Fila, Columna);
            }

            return nodo;
        }



        ///////////////////////////////////codigo de arreglos
        

    }
}
