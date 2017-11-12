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
            this.Valor = (FNodoExpresion)valor;
        }


        public String Generar3D(int pos)
        {
            pos += Padre.Posicion;//sumamos la posicion donde se van a generar las variables en el stack
            String cadena = "";

            if (Dimensiones.Count == 0)//no es arreglo
            {
                if (Valor != null)
                {
                    Nodo3D val = Valor.Generar3D();
                    switch (Tipo)
                    {
                        case Constante.TEntero://asginacion a una variable de tipo entero
                            switch (val.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        cadena += val.Codigo;
                                        if (val.V == "" && val.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String temp = TitusTools.GetTemp();
                                            
                                            cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                        }
                                        else
                                        {

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cadena += "\t" + val.V + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 1;\n";
                                            cadena += "\t\t" + "goto " + salida + ";\n";
                                            cadena += "\t" + val.F + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 0;\n";
                                            cadena += "\t" + salida + ":\n";

                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Stack[" + temp + "] = " + auxtemp + ";\n";
                                        }
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        case Constante.TDecimal://asginacion a una variable de tipo decimal
                            switch (val.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        cadena += val.Codigo;
                                        if (val.V == "" && val.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                        }
                                        else
                                        {

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cadena += "\t" + val.V + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 1;\n";
                                            cadena += "\t\t" + "goto " + salida + ";\n";
                                            cadena += "\t" + val.F + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 0;\n";
                                            cadena += "\t" + salida + ":\n";

                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Stack[" + temp + "] = " + auxtemp + ";\n";
                                        }
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        case Constante.TCaracter://asginacion a una variable de tipo caracter
                            switch (val.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        case Constante.TCadena://asginacion a una variable de tipo cadena
                            switch (val.Tipo)
                            {
                                case Constante.TCaracter:
                                    {
                                        cadena += val.Codigo;
                                        String tem = TitusTools.GetTemp();
                                        cadena += "\t\t" + tem + " = H;\n";
                                        cadena += "\t\t" + "Heap[H] = " + val.Valor + ";\n";
                                        cadena += "\t\t" + "H = H + 1;\n";
                                        cadena += "\t\t" + "Heap[H] = 0;\n";
                                        cadena += "\t\t" + "H = H + 1;\n";

                                        String temp = TitusTools.GetTemp();
                                        cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Stack[" + temp + "] = " + tem + ";\n";
                                    }
                                    break;

                                case Constante.TCadena:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        case Constante.TBooleano://asginacion a una variable de tipo booleano
                            switch (val.Tipo)
                            {
                                case Constante.TBooleano:
                                    {
                                        cadena += val.Codigo;
                                        if (val.V == "" && val.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                                        }
                                        else
                                        {

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cadena += "\t" + val.V + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 1;\n";
                                            cadena += "\t\t" + "goto " + salida + ";\n";
                                            cadena += "\t" + val.F + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 0;\n";
                                            cadena += "\t" + salida + ":\n";

                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Stack[" + temp + "] = " + auxtemp + ";\n";
                                        }
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        default://asginacion a una variable de tipo objeto
                            if (Tipo == val.Tipo)
                            {
                                cadena += val.Codigo;
                                String temp = TitusTools.GetTemp();
                                cadena += "\t\t" + temp + " = P + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                cadena += "\t\t" + "Stack[" + temp + "] = " + val.Valor + ";\n";
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                            }
                            break;
                    }
                }                
            }
            else//es arreglo
            {
                if (Valor != null)
                {
                    
                }
                else
                {
                    String arr = TitusTools.GetTemp();
                    String cont = TitusTools.GetTemp();
                    cadena += "\t\t" + arr + " = P + " + pos.ToString() + ";//Declaracion de arreglo " + this.Nombre + "\n";
                    cadena += "\t\t" + "Stack[" + arr + "]" + " = H;\n";
                    cadena += "\t\t" + cont + "= 1;\n";
                    cadena += "\t\t" + "Heap[H] = " + Dimensiones.Count.ToString() + ";//Guardamos dimensiones\n";
                    cadena += "\t\t" + "H = H + 1;\n";
                    int i = 1;
                    foreach (FNodoExpresion dimension in Dimensiones)
                    {
                        Nodo3D size = dimension.Generar3D();

                        if (!TitusTools.HayErrores())
                        {
                            if (size.Tipo == Constante.TEntero)
                            {
                                cadena += size.Codigo;
                                cadena += "\t\t" + cont + " = " + cont + " * " + size.Valor + ";\n";
                                cadena += "\t\t" + "Heap[H] = " + size.Valor + ";//Guardamos el tamaño de la dimension " + i.ToString() + "\n";
                                cadena += "\t\t" + "H = H + 1;\n";
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede iniciar una dimension de un arreglo con un tipo de dato  " + size.Tipo + ", se esperaba un valor entero.", TitusTools.GetRuta(), Fila, Columna);
                            }
                        }
                        i++;
                    }
                    cadena += "\t\t" + "H = H + " + cont + ";//termina declaracion de arreglo\n";
                }
                
            }

            return cadena;
        }

        public String Generar3DInit(String tempH, int nueva_pos)
        {
            int pos = Padre.Posicion + nueva_pos;//sumamos la posicion donde se van a generar las variables en el Heap
            String cadena = "";

            if (Dimensiones.Count == 0)//no es arreglo
            {
                if (Valor != null)
                {
                    Nodo3D val = Valor.Generar3D();
                    switch (Tipo)
                    {
                        case Constante.TEntero://asginacion a una variable de tipo entero
                            switch (val.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        cadena += val.Codigo;
                                        if (val.V == "" && val.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                        }
                                        else
                                        {

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cadena += "\t" + val.V + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 1;\n";
                                            cadena += "\t\t" + "goto " + salida + ";\n";
                                            cadena += "\t" + val.F + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 0;\n";
                                            cadena += "\t" + salida + ":\n";

                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                        }
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        case Constante.TDecimal://asginacion a una variable de tipo decimal
                            switch (val.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        cadena += val.Codigo;
                                        if (val.V == "" && val.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                        }
                                        else
                                        {

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cadena += "\t" + val.V + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 1;\n";
                                            cadena += "\t\t" + "goto " + salida + ";\n";
                                            cadena += "\t" + val.F + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 0;\n";
                                            cadena += "\t" + salida + ":\n";

                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                        }
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        case Constante.TCaracter://asginacion a una variable de tipo caracter
                            switch (val.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        case Constante.TCadena://asginacion a una variable de tipo cadena
                            switch (val.Tipo)
                            {
                                case Constante.TCaracter:
                                    {
                                        cadena += val.Codigo;
                                        String tem = TitusTools.GetTemp();
                                        cadena += "\t\t" + tem + " = H;\n";
                                        cadena += "\t\t" + "Heap[H] = " + val.Valor + ";\n";
                                        cadena += "\t\t" + "H = H + 1;\n";
                                        cadena += "\t\t" + "Heap[H] = 0;\n";
                                        cadena += "\t\t" + "H = H + 1;\n";

                                        String temp = TitusTools.GetTemp();
                                        cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                case Constante.TCadena:
                                    {
                                        String temp = TitusTools.GetTemp();
                                        cadena += val.Codigo;
                                        cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                        cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        case Constante.TBooleano://asginacion a una variable de tipo booleano
                            switch (val.Tipo)
                            {
                                case Constante.TBooleano:
                                    {
                                        cadena += val.Codigo;
                                        if (val.V == "" && val.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                        }
                                        else
                                        {

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cadena += "\t" + val.V + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 1;\n";
                                            cadena += "\t\t" + "goto " + salida + ";\n";
                                            cadena += "\t" + val.F + ":\n";
                                            cadena += "\t\t" + auxtemp + " = 0;\n";
                                            cadena += "\t" + salida + ":\n";

                                            String temp = TitusTools.GetTemp();

                                            cadena += "\t\t" + temp + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                            cadena += "\t\t" + "Heap[" + temp + "] = " + val.Valor + ";\n";
                                        }
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                                    break;
                            }
                            break;

                        default://asginacion a una variable de tipo objeto
                            if (Tipo == Valor.Tipo)
                            {

                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un tipo " + val.Tipo + " a una variable tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                            }
                            break;
                    }
                }
            }
            else//es arreglo
            {
                if (Valor != null)
                {

                }
                else
                {
                    String arr = TitusTools.GetTemp();
                    String cont = TitusTools.GetTemp();
                    cadena += "\t\t" + arr + " = " + tempH + " + " + pos.ToString() + ";//Declaracion de arreglo " + this.Nombre + "\n";
                    cadena += "\t\t" + cont + "= 1;\n";
                    cadena += "\t\t" + "Heap[H] = " + Dimensiones.Count.ToString() + ";//Guardamos dimensiones\n";
                    cadena += "\t\t" + "H = H + 1;\n";
                    int i = 1;
                    foreach (FNodoExpresion dimension in Dimensiones)
                    {
                        Nodo3D size = dimension.Generar3D();

                        if (!TitusTools.HayErrores())
                        {
                            if (size.Tipo == Constante.TEntero)
                            {
                                cadena += size.Codigo;
                                cadena += "\t\t" + cont + " = " + cont + " * " + size.Valor + ";\n";
                                cadena += "\t\t" + "Heap[H] = " + size.Valor + ";//Guardamos el tamaño de la dimension " + i.ToString()  + "\n";
                                cadena += "\t\t" + "H = H + 1;\n";
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede iniciar una dimension de un arreglo con un tipo de dato  " + size.Tipo + ", se esperaba un valor entero.", TitusTools.GetRuta(), Fila, Columna);
                            }
                        }
                        i++;
                    }
                    cadena += "\t\t" + "H = H + " + cont + ";//termina declaracion de arreglo\n";
                }

            }

            return cadena;
        }

    }
}
