using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FLlamadaMetodo
    {
        public String Nombre;
        public int Fila, Columna;
        public List<FNodoExpresion> Parametros;

        public Simbolo Padre;


        public Simbolo Encontrado;

        public FLlamadaMetodo(String nombre, List<FNodoExpresion> parametros, int fila, int columna)
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

        //acceso a metodos
        public Nodo3D Generar3D()
        {
            Nodo3D nodo = new Nodo3D();

            String cadena = "";

            Simbolo simulacion = Padre.BuscarMetodoPadre();

            String tempsimulacion = TitusTools.GetTemp();

            cadena += "\t\t" + tempsimulacion + " = P + " + (simulacion.Ambito.Tamaño).ToString() + ";//simulacion de cambio de ambito \n";


            nodo.Valor = TitusTools.GetTemp();


            
            cadena += "\t\t" + nodo.Valor + " = P + 0;//posicion del this\n";
            

            
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
                Simbolo sim = Padre.BuscarMetodo(this.Nombre, resueltas);

                if (sim != null)
                {
                    Encontrado = sim;
                    FMetodo m = (FMetodo)sim.Valor;
                    if (m.Parametros.Count == this.Parametros.Count)
                    {

                        String este = TitusTools.GetTemp();
                        cadena += "\t\t" + este + " = " + tempsimulacion + " + 0;//metiendo el this\n";
                        String temp = TitusTools.GetTemp();
                        cadena += "\t\t" + temp + " = Stack[" + nodo.Valor + "];\n";
                        
                        String here = TitusTools.GetTemp();
                        cadena += "\t\t" + here + " = " + temp + " + " + (sim.este).ToString() + ";//calculo si hay herencia\n";//calculo de la herencia
                        cadena += "\t\t" + "Stack[" + este + "] = " + here + ";\n";
                        ////
                        cadena += "\t\t" + "P = P + " + (simulacion.Ambito.Tamaño).ToString() + ";//cambio de ambito para llamar al metodo\n";
                        cadena += "\t\t" + m.GetNombre3D()  + "();\n";
                        cadena += "\t\t" + "P = P - " + (simulacion.Ambito.Tamaño).ToString() + ";//regresando al ambito actual\n";
                        if (!sim.Tipo.Equals(Constante.TVoid))
                        {
                            String retorno = TitusTools.GetTemp();
                            nodo.Valor = TitusTools.GetTemp();
                            cadena += "\t\t" + retorno + " = " + tempsimulacion + " + 1;//Posicion del retorno\n";
                            cadena += "\t\t" + nodo.Valor + " = Stack[" + retorno + "];//obteniendo el valor del retorno\n";
                            if (m.Dimensiones >0)
                            {
                                nodo.Tipo = "arreglo " + m.Tipo;
                            }
                            else
                            {
                                nodo.Tipo = m.Tipo;
                            }
                        }
                        else
                        {
                            nodo.Tipo = Constante.TVoid;
                            nodo.Valor = "";
                        }
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro el metodo " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
                    }

                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro el constructor para la clase " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
                }                
            }

            nodo.Codigo = cadena;

            return nodo;
        }


        public Nodo3D Generar3DHijo(Simbolo padre, String temporal)
        {
            Nodo3D nodo = new Nodo3D();

            String cadena = "";

            Simbolo simulacion = Padre.BuscarMetodoPadre();

            String tempsimulacion = TitusTools.GetTemp();

            cadena += "\t\t" + tempsimulacion + " = P + " + (simulacion.Ambito.Tamaño).ToString() + ";//simulacion de cambio de ambito \n";

                        
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
                Simbolo sim = padre.BuscarMetodo(this.Nombre, resueltas);

                if (sim != null)
                {
                    Encontrado = sim;
                    FMetodo m = (FMetodo)sim.Valor;
                    if (m.Parametros.Count == this.Parametros.Count)
                    {
                        String este = TitusTools.GetTemp();
                        cadena += "\t\t" + este + " = " + tempsimulacion + " + 0;//metiendo el this\n";
                        
                                                
                        String here = TitusTools.GetTemp();
                        cadena += "\t\t" + here + " = " + temporal + " + " + (sim.este).ToString() + ";//calculo si hay herencia\n";//calculo de la herencia
                        cadena += "\t\t" + "Stack[" + este + "] = " + here + ";\n";

                        ///
                        cadena += "\t\t" + "P = P + " + (simulacion.Ambito.Tamaño).ToString() + ";//cambio de ambito para llamar al metodo\n";
                        cadena += "\t\t" + m.GetNombre3D() + "();\n";
                        cadena += "\t\t" + "P = P - " + (simulacion.Ambito.Tamaño).ToString() + ";//regresando al ambito actual\n";
                        if (!sim.Tipo.Equals(Constante.TVoid))
                        {
                            String retorno = TitusTools.GetTemp();
                            nodo.Valor = TitusTools.GetTemp();
                            cadena += "\t\t" + retorno + " = " + tempsimulacion + " + 1;//Posicion del retorno\n";
                            cadena += "\t\t" + nodo.Valor + " = Stack[" + retorno + "];//obteniendo el valor del retorno\n";
                            if (m.Dimensiones > 0)
                            {
                                nodo.Tipo = "arreglo " + m.Tipo;
                            }
                            else
                            {
                                nodo.Tipo = m.Tipo;
                            }
                        }
                        else
                        {
                            nodo.Tipo = Constante.TVoid;
                            nodo.Valor = "";
                        }
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro el metodo " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
                    }

                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro el constructor para la clase " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
                }
            }

            nodo.Codigo = cadena;

            return nodo;
        }

    }
}
