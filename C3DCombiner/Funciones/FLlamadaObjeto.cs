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
            }
            else if (Tipo.Equals(Constante.LLAMADA_METODO_ARREGLO))
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


        public void SetPadre(Simbolo simbolo)
        {
            this.Padre = simbolo;
            switch (this.Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    this.LlamadaArreglo.setPadre(simbolo);
                    break;

                case Constante.LLAMADA_METODO:
                    this.LlamadaMetodo.setPadre(simbolo);
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    this.LlamadaMetodoArreglo.setPadre(simbolo);
                    break;

            }

            if (this.Hijo != null)
            {
                this.Hijo.SetPadre(simbolo);
            }
        }


        public Nodo3D Generar3D()
        {
            Nodo3D cadena = new Nodo3D();

            switch (Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    break;


                case Constante.LLAMADA_METODO:
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    break;

                case Constante.TSelf:
                    break;

                case Constante.TSuper:
                    break;

                default:
                    cadena = Generar3DVariable();
                    break;
            }

            return cadena;
        }


        public Nodo3D Generar3DVariable()
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = Padre.BuscarVariable(this.Nombre);


            if (sim != null)
            {
                if (Hijo == null)
                {
                    if (sim.Rol.Equals(Constante.DECLARACION))
                    {
                        FDeclaracion decla = (FDeclaracion)sim.Valor;
                        if (decla.Dimensiones.Count == 0)
                        {
                            nodo.Tipo = sim.Tipo;
                        }
                        else
                        {
                            nodo.Tipo = "arreglo " + sim.Tipo;
                        }

                        if (sim.Ambito.Nombre.Equals("§global§"))
                        {
                            String pos = TitusTools.GetTemp();
                            String heap = TitusTools.GetTemp();
                            nodo.Valor = TitusTools.GetTemp();
                            nodo.Codigo += "\t\t" + pos + " = Stack[P];\n";
                            nodo.Codigo += "\t\t" + heap + " = " + pos + " + " + sim.Posicion.ToString() + ";\n";
                            nodo.Codigo += "\t\t" + nodo.Valor + " = Heap[" + heap + "];\n";
                        }
                        else
                        {
                            String pos = TitusTools.GetTemp();
                            nodo.Valor = TitusTools.GetTemp();
                            nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";\n";
                            nodo.Codigo += "\t\t" + nodo.Valor + " = Stack[" + pos + "];\n";
                        }
                    }
                    else
                    {
                        FParametro decla = (FParametro)sim.Valor;
                        if (decla.Dimensiones == 0)
                        {
                            nodo.Tipo = sim.Tipo;
                        }
                        else
                        {
                            nodo.Tipo = "arreglo " + sim.Tipo;
                        }
                        String pos = TitusTools.GetTemp();
                        nodo.Valor = TitusTools.GetTemp();
                        nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";\n";
                        nodo.Codigo += "\t\t" + nodo.Valor + " = Stack[" + pos + "];\n";

                    }


                }

            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
            }

            return nodo;
        }
    }
}
