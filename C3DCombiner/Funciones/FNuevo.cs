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

            String cadena = "";

            Simbolo simulacion = Padre.BuscarMetodoPadre();

            String tempsimulacion = TitusTools.GetTemp();

            cadena += "\t\t" + tempsimulacion + " = P + " + (simulacion.Ambito.Tamaño + 2).ToString() + ";//simulacion de cambio de ambito \n";

            nodo.Valor = TitusTools.GetTemp();

            nodo.Tipo = this.Nombre;

            cadena += "\t\t" + nodo.Valor + " = H;//posicion del objeto\n";

            cadena += "\t\t" + "init_" + this.Nombre + "();\n";

            String este = TitusTools.GetTemp();
            cadena += "\t\t" + este + " = " + tempsimulacion + " + 0;//metiendo el this\n";
            cadena += "\t\t" + "Stack[" + este + "] = " + nodo.Valor + ";\n";
            List<Nodo3D> resueltas = new List<Nodo3D>();

            int i = 2;
            foreach (FNodoExpresion p in this.Parametros)
            {

                String temporalstack = TitusTools.GetTemp();

                cadena += "\t\t" + temporalstack + " = " + tempsimulacion + " + " + i.ToString() + ";\n";
                Nodo3D r = p.Generar3D();
                if (r.Tipo.Equals(Constante.TBooleano))//conversion siviene de una relacional
                {
                    if (r.V == "" && r.F == "")
                    {//si trae etiquetas viene de una relacional si no es un bool nativo

                    }
                    else
                    {
                        var cad = "";

                        var auxtemp = TitusTools.GetTemp();
                        var salida = TitusTools.GetEtq();

                        cad += "\t" + r.V + ":\n";
                        cad += "\t\t" + auxtemp + " = 1;\n";
                        cad += "\t\t" + "goto " + salida + ";\n";
                        cad += "\t" + r.F + ":\n";
                        cad += "\t\t" + auxtemp + " = 0;\n";
                        cad += "\t" + salida + ":\n";

                        r.Valor = auxtemp;
                        cad += "\t\t" + r.Valor + " =  - " + auxtemp + ";\n";
                        r.Codigo = r.Codigo + cad;
                    }
                }


                cadena += r.Codigo;
                resueltas.Add(r);

                //ahora asignamos al stack el valor resuelto
                cadena += "\t\t" + "Stack[" + temporalstack + "] = " + r.Valor + ";//Asignacino del parametro\n";
                
                i++;
            }

            if (!TitusTools.HayErrores())
            {
                Simbolo sim = Padre.BuscarConstructor(this.Nombre, resueltas);

                cadena += "\t\t" + "P = P + " + (simulacion.Ambito.Tamaño + 2).ToString() + ";//cambio de ambito para llamar al constructor\n";
                if (sim != null)
                {
                    FMetodo m = (FMetodo)sim.Valor;
                    if (m.Parametros.Count == this.Parametros.Count)
                    {
                        cadena += "\t\t" + this.Nombre + "_constructor";
                        foreach (Simbolo p in m.Parametros)
                        {
                            FParametro parametro = (FParametro)p.Valor;
                            cadena += "_" + parametro.Tipo + parametro.Dimensiones.ToString();
                        }

                        cadena += "();\n";
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro el constructor para la clase " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
                    }

                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro el constructor para la clase " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
                }
                cadena += "\t\t" + "P = P - " + (simulacion.Ambito.Tamaño + 2).ToString() + ";//cambio de ambito para llamar al constructor\n";

            }

            nodo.Codigo = cadena;

            return nodo;
        }
    }
}
