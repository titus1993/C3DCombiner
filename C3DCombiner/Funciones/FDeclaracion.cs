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
                if (Valor != null)
                {
                    Nodo3D val = Valor.Generar3D();

                    if (!TitusTools.HayErrores())
                    {
                        if (val.Tipo == "arreglo " + this.Tipo)
                        {
                            cadena += val.Codigo;
                            String etqerror = TitusTools.GetEtq();
                            String etqsalida = TitusTools.GetEtq();

                            String contador = TitusTools.GetTemp();
                            String contador2 = TitusTools.GetTemp();
                            String val1 = TitusTools.GetTemp();
                            String val2 = TitusTools.GetTemp();
                            cadena += "\t\t" + contador + " = Stack[" + arr + "];\n";
                            cadena += "\t\t" + contador2 + " = " + val.Valor + ";\n";
                            cadena += "\t\t" + val1 + " = Heap[" + contador + "];//acceso a la declaracion del arreglo\n";
                            cadena += "\t\t" + val2 + " = Heap[" + contador2 + "];\n"; 
                            cadena += "\t\t" + "ifFalse " + val1 + " == " + val2 + " goto " + etqerror + ";\n";
                            String ciclo = TitusTools.GetTemp();
                            String tope = TitusTools.GetTemp();
                            cadena += "\t\t" + ciclo + " = 0;\n";
                            cadena += "\t\t" + tope + " = " + val1 + ";\n";
                            String recursivo = TitusTools.GetEtq();
                            cadena += "\t" + recursivo + ":\n";
                            cadena += "\t\t" + "ifFalse " + ciclo + " < " + tope + " goto " + etqsalida + ";\n";
                            cadena += "\t\t" + val1 + " = Heap[" + contador + "];//acceso a tamanio dimension\n";
                            cadena += "\t\t" + val2 + " = Heap[" + contador2 + "];//acceso a tamanio dimension 2\n";
                            cadena += "\t\t" + contador + " = " + contador + " + 1;\n";
                            cadena += "\t\t" + contador2 + " = " + contador2 + " + 1;\n";
                            cadena += "\t\t" + ciclo + " = " + ciclo + " + 1;\n";
                            cadena += "\t\t" + "if " + val1 + " == " + val2 + " goto " + recursivo + ";\n";
                            cadena += "\t\t" + "goto " + etqerror + ";\n";
                            cadena += "\t\t" + "goto " + etqsalida + ";\n"; 
                            cadena += "\t" + etqerror + ":\n";
                            cadena += "\t\t" + "Error(2);\n";
                            cadena += "\t" + etqsalida + ":\n";

                            //validacio de dimensiones
                            cadena += "\t\t" + arr + " = P + " + pos.ToString() + ";//Declaracion de arreglo " + this.Nombre + "\n";
                            cadena += "\t\t" + "Stack[" + arr + "]" + " = " + val.Valor + ";\n";
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un " + val.Tipo + " a un arreglo de tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                        }
                    }

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
                                            cadena += "\t\t" + "Heap[" + temp + "] = " + auxtemp + ";\n";
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
                                            cadena += "\t\t" + "Heap[" + temp + "] = " + auxtemp + ";\n";
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
                                            cadena += "\t\t" + "Heap[" + temp + "] = " + auxtemp + ";\n";
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
                                String heap = TitusTools.GetTemp();
                                String posheap = TitusTools.GetTemp();
                                cadena += "\t\t" + temp + " = P + 0;//Posicion del this\n";
                                cadena += "\t\t" + heap +  " = Stack[" + temp + "];\n";
                                cadena += "\t\t" + posheap + " = " + heap + " + " + pos.ToString() + ";//Declaracion de la variable " + this.Nombre + "\n";
                                cadena += "\t\t" + "Heap[" + posheap + "] = " + val.Valor + ";\n";
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
                String stack = TitusTools.GetTemp();
                String posheap = TitusTools.GetTemp();
                String arr = TitusTools.GetTemp();
                String cont = TitusTools.GetTemp();
                cadena += "\t\t" + stack + " = P + 0;\n";
                cadena += "\t\t" + posheap + " = Stack[" + stack + "];\n";
                cadena += "\t\t" + arr + " = " + posheap + " + " + pos.ToString() + ";//Declaracion de arreglo " + this.Nombre + "\n";
                cadena += "\t\t" + "Heap[" + arr + "]" + " = H;\n";
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
                if (Valor != null)
                {
                    Nodo3D val = Valor.Generar3D();

                    if (!TitusTools.HayErrores())
                    {
                        if (val.Tipo == "arreglo " + this.Tipo)
                        {
                            cadena += val.Codigo;
                            String etqerror = TitusTools.GetEtq();
                            String etqsalida = TitusTools.GetEtq();

                            String contador = TitusTools.GetTemp();
                            String contador2 = TitusTools.GetTemp();
                            String val1 = TitusTools.GetTemp();
                            String val2 = TitusTools.GetTemp();
                            cadena += "\t\t" + contador + " = Heap[" + arr + "];\n";
                            cadena += "\t\t" + contador2 + " = " + val.Valor + ";\n";
                            cadena += "\t\t" + val1 + " = Heap[" + contador + "];//acceso a la declaracion del arreglo\n";
                            cadena += "\t\t" + val2 + " = Heap[" + contador2 + "];\n";
                            cadena += "\t\t" + "ifFalse " + val1 + " == " + val2 + " goto " + etqerror + ";\n";
                            String ciclo = TitusTools.GetTemp();
                            String tope = TitusTools.GetTemp();
                            cadena += "\t\t" + ciclo + " = 0;\n";
                            cadena += "\t\t" + tope + " = " + val1 + ";\n";
                            String recursivo = TitusTools.GetEtq();
                            cadena += "\t" + recursivo + ":\n";
                            cadena += "\t\t" + "ifFalse " + ciclo + " < " + tope + " goto " + etqsalida + ";\n";
                            cadena += "\t\t" + val1 + " = Heap[" + contador + "];//acceso a tamanio dimension\n";
                            cadena += "\t\t" + val2 + " = Heap[" + contador2 + "];//acceso a tamanio dimension 2\n";
                            cadena += "\t\t" + contador + " = " + contador + " + 1;\n";
                            cadena += "\t\t" + contador2 + " = " + contador2 + " + 1;\n";
                            cadena += "\t\t" + ciclo + " = " + ciclo + " + 1;\n";
                            cadena += "\t\t" + "if " + val1 + " == " + val2 + " goto " + recursivo + ";\n";
                            cadena += "\t\t" + "goto " + etqerror + ";\n";
                            cadena += "\t\t" + "goto " + etqsalida + ";\n";
                            cadena += "\t" + etqerror + ":\n";
                            cadena += "\t\t" + "Error(2);\n";
                            cadena += "\t" + etqsalida + ":\n";

                            //validacio de dimensiones
                            cadena += "\t\t" + "Heap[" + arr + "]" + " = " + val.Valor + ";//Declaracion de arreglo global" + this.Nombre + "\n";
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede asignar un " + val.Tipo + " a un arreglo de tipo " + this.Tipo, TitusTools.GetRuta(), Fila, Columna);
                        }
                    }

                }

            }

            return cadena;
        }

    }
}
