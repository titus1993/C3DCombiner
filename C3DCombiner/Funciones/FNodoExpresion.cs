﻿using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FNodoExpresion
    {
        public Simbolo Padre;
        public FNodoExpresion Hermano;

        public FNodoExpresion Izquierda, Derecha;
        public String Tipo, Nombre;
        public int Fila, Columna;
        public int Entero;
        public double Decimal;
        public char Caracter;
        public String Cadena;
        public Boolean Bool;
        public FLlamadaObjeto LlamadaObjeto;
        public FNuevo Nuevo;

        public FNodoExpresion(FNodoExpresion nodo)
        {
            this.Izquierda = nodo.Izquierda;
            this.Derecha = nodo.Derecha;
            this.Bool = nodo.Bool;
            this.Cadena = nodo.Cadena;
            this.Caracter = nodo.Caracter;
            this.Columna = nodo.Columna;
            this.Decimal = nodo.Decimal;
            this.Entero = nodo.Entero;
            this.Fila = nodo.Fila;
            this.Nombre = nodo.Nombre;
            this.LlamadaObjeto = nodo.LlamadaObjeto;
            this.Tipo = nodo.Tipo;
            this.Nuevo = nodo.Nuevo;
        }


        public void SetPadre(Simbolo simbolo)
        {
            this.Padre = simbolo;
            if (this.Izquierda != null)
            {
                this.Izquierda.SetPadre(simbolo);
            }

            if (Derecha != null)
            {
                this.Derecha.SetPadre(simbolo);
            }

            switch (this.Tipo)
            {

                case Constante.LLAMADA_OBJETO:
                    {
                        this.LlamadaObjeto.SetPadre(simbolo);
                    }
                    break;

                case Constante.TNuevo:
                    {
                        this.Nuevo.setPadre(simbolo);
                    }
                    break;
            }

        }
        public FNodoExpresion(FNodoExpresion izq, FNodoExpresion der, String tipo, String nombre, int fila, int columna, Object valor)
        {
            this.Izquierda = izq;
            this.Derecha = der;
            this.Tipo = tipo;
            this.Nombre = nombre;
            this.Fila = fila;
            this.Columna = columna;

            switch (tipo)
            {
                case Constante.TEntero:
                    this.Entero = Int32.Parse(valor.ToString());
                    this.Cadena = valor.ToString();
                    break;

                case Constante.TDecimal:
                    this.Decimal = Double.Parse(valor.ToString());
                    this.Cadena = valor.ToString();
                    break;

                case Constante.TCaracter:
                    this.Caracter = valor.ToString()[0];
                    this.Cadena = valor.ToString();
                    this.Entero = this.Caracter;
                    break;

                case Constante.TCadena:
                    this.Cadena = (String)valor;
                    break;

                case Constante.TBooleano:
                    this.Cadena = valor.ToString();
                    if (this.Cadena.Equals(Constante.TTrue))
                    {
                        this.Bool = true;
                        this.Entero = 1;
                        this.Decimal = 1;
                    }
                    else
                    {
                        this.Bool = false;
                        this.Entero = 0;
                        this.Decimal = 0;
                    }
                    break;

                case Constante.LLAMADA_OBJETO:
                    {
                        this.LlamadaObjeto = (FLlamadaObjeto)valor;
                    }
                    break;

                case Constante.TNuevo:
                    {
                        this.Nuevo = (FNuevo)valor;
                    }
                    break;
            }
        }



        public Nodo3D Generar3D()
        {
            return Generar3D(this);
        }


        private Nodo3D Generar3D(FNodoExpresion nodo)
        {
            Nodo3D codigo3d = new Nodo3D();

            switch (nodo.Tipo)
            {
                case Constante.TMas:
                    codigo3d = Suma3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TMenos:
                    if (nodo.Izquierda != null)
                    {
                        codigo3d = Resta3D(nodo.Izquierda, nodo.Derecha);
                    }
                    else
                    {
                        codigo3d = Resta3D(nodo.Derecha);
                    }
                    break;

                case Constante.TPor:
                    codigo3d = Multiplicacion3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TDivision:
                    codigo3d = Division3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TPotencia:
                    codigo3d = Potencia3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TPotenciaOCL:
                    codigo3d = Potencia3D(nodo.Izquierda, nodo.Derecha);
                    break;

                /*case Constante.TAumento:
                    codigo3d = Aumento(izq);
                    break;

                case Constante.TDecremento:
                    codigo3d = Disminucion(izq);
                    break;*/

                case Constante.TMayor:
                    codigo3d = Relacional3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TMenor:
                    codigo3d = Relacional3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TMayorIgual:
                    codigo3d = Relacional3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TMenorIgual:
                    codigo3d = Relacional3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TIgualacion:
                    codigo3d = Relacional3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TDiferenciacion:
                    codigo3d = Relacional3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TOr:
                    codigo3d = Or3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TXor:
                    codigo3d = Xor3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TAnd:
                    codigo3d = And3D(nodo.Izquierda, nodo.Derecha);
                    break;

                case Constante.TNot:
                    codigo3d = Not3D(nodo.Derecha);
                    break;

                case Constante.TEntero:
                    {
                        String cad = "";
                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal
                        cad = "\t\t" + codigo3d.Valor + " = " + nodo.Entero.ToString() + ";//entero " + nodo.Entero.ToString() + "\n";//asignamos el valor al temporal
                        codigo3d.Codigo = cad;//asignamos la cadena al nodo de retorno
                        codigo3d.Tipo = nodo.Tipo;//asignamos el tipo de dato
                    }
                    break;

                case Constante.TDecimal:
                    {
                        String cad = "";
                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal
                        cad = "\t\t" + codigo3d.Valor + " = " + nodo.Decimal.ToString() + ";//decimal " + nodo.Decimal.ToString() + "\n";//asignamos el valor al temporal
                        codigo3d.Codigo = cad;//asignamos la cadena al nodo de retorno
                        codigo3d.Tipo = nodo.Tipo;//asignamos el tipo de dato
                    }
                    break;

                case Constante.TCaracter:
                    {
                        String cad = "";
                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal
                        int ascii = nodo.Caracter;
                        cad += "\t\t" + codigo3d.Valor + " = " + ascii.ToString() + ";//caracter " + nodo.Caracter.ToString() + "\n";//asignamos el valor al temporal
                        codigo3d.Codigo = cad;//asignamos la cadena al nodo de retorno
                        codigo3d.Tipo = nodo.Tipo;//asignamos el tipo de dato
                    }
                    break;

                case Constante.TCadena:
                    {
                        var cad = "";
                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal
                        cad += "\t\t" + codigo3d.Valor + " = H;//cadena " + nodo.Cadena + "\n";//obtenemos el valor del heap donde guardaremos la cadena

                        for (int i = 0; i < nodo.Cadena.Length; i++)
                        {
                            int ascii = nodo.Cadena[i];
                            cad += "\t\t" + "Heap[H] = " + ascii.ToString() + ";\n";//asignamos el valor de cada caracter al temporal
                            cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                        }
                        int asc = ("\0")[0];
                        cad += "\t\t" + "Heap[H] = " + asc.ToString() + ";\n";
                        cad += "\t\t" + "H = H + 1;\n";
                        codigo3d.Codigo = cad;//asignamos la cadena al nodo de retorno
                        codigo3d.Tipo = nodo.Tipo;//asignamos el tipo de dato
                    }
                    break;

                case Constante.TBooleano:
                    {
                        String cad = "";
                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal

                        codigo3d.Tipo = nodo.Tipo;//asignamos el tipo de dato
                        if (nodo.Bool)
                        {
                            cad = "\t\t" + codigo3d.Valor + " = 1;//booleano " + nodo.Bool.ToString() + "\n";
                        }
                        else
                        {
                            cad = "\t\t" + codigo3d.Valor + " = 0;//booleano " + nodo.Bool.ToString() + "\n";
                        }
                        codigo3d.Codigo = cad;
                        codigo3d.Tipo = nodo.Tipo;
                    }
                    break;

            }
            return codigo3d;
        }

        private Nodo3D IntToCadena(String tnum)
        {
            String cadena = "";
            Nodo3D codigo3d = new Nodo3D();

            String t0 = TitusTools.GetTemp();
            String t1 = TitusTools.GetTemp();
            String t2 = TitusTools.GetTemp();
            String t3 = TitusTools.GetTemp();
            String t4 = TitusTools.GetTemp();
            String t5 = TitusTools.GetTemp();
            String t6 = TitusTools.GetTemp();
            String t7 = TitusTools.GetTemp();

            String e1 = TitusTools.GetEtq();
            String e2 = TitusTools.GetEtq();
            String e3 = TitusTools.GetEtq();
            String e4 = TitusTools.GetEtq();
            String e5 = TitusTools.GetEtq();
            String e6 = TitusTools.GetEtq();
            String e7 = TitusTools.GetEtq();
            String e8 = TitusTools.GetEtq();
            String e9 = TitusTools.GetEtq();
            String e10 = TitusTools.GetEtq();
            String e11 = TitusTools.GetEtq();
            String e12 = TitusTools.GetEtq();

            //codigo 3d
            cadena += "\t\t" + t0 + " = " + tnum + ";//Convirtiendo entero a cadena\n"; // temporal que guarda el numero a convertir en cadena
            cadena += "\t\t" + t1 + " = " + "1;\n"; //temporal que guardara si es negativo o positivo
            cadena += "\t\t" + "if " + t0 + " >= 0 goto " + e1 + ";\n"; // si es negativo guardamos el -1
            cadena += "\t\t" + t1 + " = -1;\n";
            cadena += "\t\t" + t0 + " = " + t0 + " * -1;\n";
            cadena += "\t" + e1 + ":\n";
            cadena += "\t\t" + t2 + " = 1;\n"; // temporal con el que sabremos el tamaño del numero  
            cadena += "\t" + e3 + ":\n";
            cadena += "\t\t" + t3 + " = 1;\n"; // temporal que llevara el contador de 9
            cadena += "\t" + e4 + ":\n";
            cadena += "\t\t" + t4 + " = " + t2 + " * " + t3 + ";\n";
            cadena += "\t\t" + "if " + t3 + " > 10 goto " + e5 + ";\n";
            cadena += "\t\t" + "if " + t0 + " < " + t4 + " goto " + e2 + ";\n";
            cadena += "\t\t" + t3 + " = " + t3 + " + 1;\n";
            cadena += "\t\t" + "goto " + e4 + ";\n";
            cadena += "\t" + e5 + ":\n";
            cadena += "\t\t" + t2 + " = " + t2 + " * 10;\n";
            cadena += "\t\t" + "goto " + e3 + ";\n";
            ////////////////////////////////////////////////////////////////
            cadena += "\t" + e2 + "://comenzamos a guardar el numero en el heap\n";
            cadena += "\t\t" + t5 + " = H;\n"; // temporal que guardara la posicion del heap donde creamos el numero
            cadena += "\t\t" + "if " + t1 + " == 1 goto " + e6 + ";\n";
            cadena += "\t\t" + "Heap[H] = 45;\n";
            cadena += "\t\t" + "H = H + 1;\n";
            cadena += "\t" + e6 + ":\n";
            cadena += "\t\t" + t3 + " = " + t3 + " - 1;\n";
            cadena += "\t\t" + t6 + " = 0;\n";
            cadena += "\t\t" + t7 + "= 48;\n";
            cadena += "\t" + e7 + ":\n";
            cadena += "\t\t" + "if " + t6 + " == " + t3 + " goto " + e8 + ";\n";
            cadena += "\t\t" + t6 + " = " + t6 + " + 1;\n";
            cadena += "\t\t" + t7 + " = " + t7 + " + 1;\n";
            cadena += "\t\t" + "goto " + e7 + ";\n";

            cadena += "\t" + e8 + ":\n";//aqui guarda ascii
            cadena += "\t\t" + "Heap[H] = " + t7 + ";\n";
            cadena += "\t\t" + "H = H + 1;\n";


            cadena += "\t\t" + "if " + t2 + " == 1 goto " + e9 + ";\n";
            cadena += "\t\t" + t4 + " = " + t2 + " * " + t3 + ";\n";
            cadena += "\t\t" + t0 + " = " + t0 + " - " + t4 + ";\n";
            cadena += "\t\t" + t2 + " = " + t2 + " / 10;\n";

            cadena += "\t" + e10 + ":\n";
            cadena += "\t\t" + t3 + " = 1;\n"; // temporal que llevara el contador de 9
            cadena += "\t" + e11 + ":\n";
            cadena += "\t\t" + t4 + " = " + t2 + " * " + t3 + ";\n";
            cadena += "\t\t" + "if " + t3 + " > 10 goto " + e12 + ";\n";
            cadena += "\t\t" + "if " + t0 + " < " + t4 + " goto " + e6 + ";\n";
            cadena += "\t\t" + t3 + " = " + t3 + " + 1;\n";
            cadena += "\t\t" + "goto " + e11 + ";\n";
            cadena += "\t" + e12 + ":\n";
            cadena += "\t\t" + t2 + " = " + t2 + " * 10;\n";
            cadena += "\t\t" + "goto " + e10 + ";\n";

            cadena += "\t" + e9 + ":\n";
            cadena += "\t\t" + "Heap[H] = 0;\n";

            codigo3d.Codigo = cadena;
            codigo3d.Tipo = Constante.TCadena;
            codigo3d.Valor = t5;
            return codigo3d;
        }

        private Nodo3D Suma3D(FNodoExpresion izq, FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxizq = izq.Generar3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {

                switch (auxizq.Tipo)
                {
                    case Constante.TEntero:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TCadena:
                                    {
                                        String cad = "";

                                        Nodo3D toString = IntToCadena(auxizq.Valor);

                                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal

                                        cad += "\t\t" + codigo3d.Valor + " = H;//entero + cadena\n";//obtenemos el valor del heap donde guardaremos la concatenacion

                                        //cadena 1
                                        String auxpool = TitusTools.GetTemp();

                                        String auxetqif = TitusTools.GetEtq();
                                        String auxetqifsalida = TitusTools.GetEtq();

                                        String auxpool2 = TitusTools.GetTemp();

                                        cad += "\t\t" + auxpool + " = " + toString.Valor + ";\n";
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t" + auxetqif + ":\n";
                                        cad += "\t\t" + "if " + auxpool2 + " == 0 goto " + auxetqifsalida + ";\n";
                                        cad += "\t\t" + "Heap[H] = " + auxpool2 + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                                        cad += "\t\t" + auxpool + " = " + auxpool + " + 1;\n";//obtenemos el nuevo valor del heap
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t\t" + "goto " + auxetqif + ";\n";// salto para volver a evaluar el pool
                                        cad += "\t" + auxetqifsalida + ":\n";//etq de salida por si el if es verdadero

                                        //cadena2
                                        auxpool = TitusTools.GetTemp();

                                        auxetqif = TitusTools.GetEtq();
                                        auxetqifsalida = TitusTools.GetEtq();

                                        auxpool2 = TitusTools.GetTemp();

                                        cad += "\t\t" + auxpool + " = " + auxder.Valor + ";\n";
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t" + auxetqif + ":\n";
                                        cad += "\t\t" + "if " + auxpool2 + " == 0 goto " + auxetqifsalida + ";\n";
                                        cad += "\t\t" + "Heap[H] = " + auxpool2 + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                                        cad += "\t\t" + auxpool + " = " + auxpool + " + 1;\n";//obtenemos el nuevo valor del heap
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t\t" + "goto " + auxetqif + ";\n";// salto para volver a evaluar el pool
                                        cad += "\t" + auxetqifsalida + ":\n";//etq de salida por si el if es verdadero
                                        cad += "\t\t" + "Heap[H] = 0;\n";
                                        cad += "\t\t" + "H = H + 1;\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + toString.Codigo + cad;//asignamos la cadena al nodo de retorno
                                        codigo3d.Tipo = Constante.TCadena;//asignamos el tipo de dato
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            var cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TEntero;
                                        }
                                        else
                                        {
                                            var cad = "";

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TEntero;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " + " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TDecimal:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCadena:
                                    {

                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                        else
                                        {
                                            String cad = "";

                                            String auxtemp = TitusTools.GetTemp();
                                            String salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " + " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TCaracter:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCadena:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal

                                        cad += "\t\t" + codigo3d.Valor + " = H;//caracter + cadena\n";//obtenemos el valor del heap donde guardaremos la concatenacion

                                        //cadena 1
                                        String auxpool = TitusTools.GetTemp();

                                        String auxetqif = TitusTools.GetEtq();
                                        String auxetqifsalida = TitusTools.GetEtq();

                                        String auxpool2 = TitusTools.GetTemp();

                                        cad += "\t\t" + "Heap[H] = " + auxizq.Valor + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool

                                        //cadena2

                                        cad += "\t\t" + auxpool + " = " + auxder.Valor + ";\n";
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t" + auxetqif + ":\n";
                                        cad += "\t\t" + "if " + auxpool2 + " == 0 goto " + auxetqifsalida + ";\n";
                                        cad += "\t\t" + "Heap[H] = " + auxpool2 + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                                        cad += "\t\t" + auxpool + " = " + auxpool + " + 1;\n";//obtenemos el nuevo valor del heap
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t\t" + "goto " + auxetqif + ";\n";// salto para volver a evaluar el pool
                                        cad += "\t" + auxetqifsalida + ":\n";//etq de salida por si el if es verdadero
                                        cad += "\t\t" + "Heap[H] = 0;\n";
                                        cad += "\t\t" + "H = H + 1;\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;//asignamos la cadena al nodo de retorno
                                        codigo3d.Tipo = Constante.TCadena;//asignamos el tipo de dato                                        
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " + " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TCadena:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        Nodo3D toString = IntToCadena(auxder.Valor);
                                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal

                                        cad += "\t" + codigo3d.Valor + " = H;//cadena + entero\n";//obtenemos el valor del heap donde guardaremos la concatenacion

                                        //cadena 1
                                        String auxpool = TitusTools.GetTemp();

                                        String auxetqif = TitusTools.GetEtq();
                                        String auxetqifsalida = TitusTools.GetEtq();

                                        String auxpool2 = TitusTools.GetTemp();

                                        cad += "\t\t" + auxpool + " = " + auxizq.Valor + ";\n";
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t" + auxetqif + ":\n";
                                        cad += "\t\t" + "if " + auxpool2 + " == 0 goto " + auxetqifsalida + ";\n";
                                        cad += "\t\t" + "Heap[H] = " + auxpool2 + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                                        cad += "\t\t" + auxpool + " = " + auxpool + " + 1;\n";//obtenemos el nuevo valor del heap
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t\t" + "goto " + auxetqif + ";\n";// salto para volver a evaluar el pool
                                        cad += "\t\t" + auxetqifsalida + ":\n";//etq de salida por si el if es verdadero
                                        cad += "\t\t" + "Heap[H] = 0;\n";
                                        cad += "\t\t" + "H = H + 1;\n";



                                        //cadena2


                                        auxpool = TitusTools.GetTemp();

                                        auxetqif = TitusTools.GetEtq();
                                        auxetqifsalida = TitusTools.GetEtq();

                                        auxpool2 = TitusTools.GetTemp();

                                        cad += "\t\t" + auxpool + " = " + toString.Valor + ";\n";
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t" + auxetqif + ":\n";
                                        cad += "\t\t" + "if " + auxpool2 + " == 0 goto " + auxetqifsalida + ";\n";
                                        cad += "\t\t" + "Heap[H] = " + auxpool2 + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                                        cad += "\t\t" + auxpool + " = " + auxpool + " + 1;\n";//obtenemos el nuevo valor del heap
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t\t" + "goto " + auxetqif + ";\n";// salto para volver a evaluar el pool
                                        cad += "\t" + auxetqifsalida + ":\n";//etq de salida por si el if es verdadero

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + toString.Codigo + cad;//asignamos la cadena al nodo de retorno
                                        codigo3d.Tipo = Constante.TCadena;//asignamos el tipo de dato
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {

                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal

                                        cad += "\t\t" + codigo3d.Valor + " = H;//cadena + caracter\n";//obtenemos el valor del heap donde guardaremos la concatenacion

                                        //cadena 1
                                        String auxpool = TitusTools.GetTemp();

                                        String auxetqif = TitusTools.GetEtq();
                                        String auxetqifsalida = TitusTools.GetEtq();

                                        String auxpool2 = TitusTools.GetTemp();

                                        cad += "\t\t" + auxpool + " = " + auxizq.Valor + ";\n";
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t" + auxetqif + ":\n";
                                        cad += "\t\t" + "if " + auxpool2 + " == 0 goto " + auxetqifsalida + ";\n";
                                        cad += "\t\t" + "Heap[H] = " + auxpool2 + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                                        cad += "\t\t" + auxpool + " = " + auxpool + " + 1;\n";//obtenemos el nuevo valor del heap
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t\t" + "goto " + auxetqif + ";\n";// salto para volver a evaluar el pool
                                        cad += "\t" + auxetqifsalida + ":\n";//etq de salida por si el if es verdadero

                                        //cadena2



                                        cad += "\t\t" + "Heap[H] = " + auxder.Valor + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                                        cad += "\t\t" + "Heap[H] = 0;\n";
                                        cad += "\t\t" + "H = H + 1;\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;//asignamos la cadena al nodo de retorno
                                        codigo3d.Tipo = Constante.TCadena;//asignamos el tipo de dato   
                                    }
                                    break;

                                case Constante.TCadena:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();//obtenemos el temporal

                                        cad += "\t\t" + codigo3d.Valor + " = H;//cadena + cadena\n";//obtenemos el valor del heap donde guardaremos la concatenacion

                                        //cadena 1
                                        String auxpool = TitusTools.GetTemp();

                                        String auxetqif = TitusTools.GetEtq();
                                        String auxetqifsalida = TitusTools.GetEtq();

                                        String auxpool2 = TitusTools.GetTemp();

                                        cad += "\t\t" + auxpool + " = " + auxizq.Valor + ";\n";
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t" + auxetqif + ":\n";
                                        cad += "\t\t" + "if " + auxpool2 + " == 0 goto " + auxetqifsalida + ";\n";
                                        cad += "\t\t" + "Heap[H] = " + auxpool2 + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                                        cad += "\t\t" + auxpool + " = " + auxpool + " + 1;\n";//obtenemos el nuevo valor del heap
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t\t" + "goto " + auxetqif + ";\n";// salto para volver a evaluar el pool
                                        cad += "\t" + auxetqifsalida + ":\n";//etq de salida por si el if es verdadero
                                        cad += "\t\t" + "Heap[H] = 0;\n";
                                        cad += "\t\t" + "H = H + 1;\n";



                                        //cadena2


                                        auxpool = TitusTools.GetTemp();

                                        auxetqif = TitusTools.GetEtq();
                                        auxetqifsalida = TitusTools.GetEtq();

                                        auxpool2 = TitusTools.GetTemp();

                                        cad += "\t\t" + auxpool + " = " + auxder.Valor + ";\n";
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t" + auxetqif + ":\n";
                                        cad += "\t\t" + "if " + auxpool2 + " == 0 goto " + auxetqifsalida + ";\n";
                                        cad += "\t\t" + "Heap[H] = " + auxpool2 + ";\n";//asignamos el valor de cada caracter al temporal
                                        cad += "\t\t" + "H = H + 1;\n";//asignamos la siguiente posicion del pool
                                        cad += "\t\t" + auxpool + " = " + auxpool + " + 1;\n";//obtenemos el nuevo valor del heap
                                        cad += "\t\t" + auxpool2 + " = " + "Heap[" + auxpool + "];\n";
                                        cad += "\t\t" + "goto " + auxetqif + ";\n";// salto para volver a evaluar el pool
                                        cad += "\t" + auxetqifsalida + ":\n";//etq de salida por si el if es verdadero

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;//asignamos la cadena al nodo de retorno
                                        codigo3d.Tipo = Constante.TCadena;//asignamos el tipo de dato
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " + " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TBooleano:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad += auxder.Codigo;
                                        cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad += auxder.Codigo;
                                        cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        var cad = "";

                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        if (auxder.V == "" && auxder.F == "")
                                        {
                                            cad += auxder.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxder.Codigo;
                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxder.Valor = auxtemp;
                                        }

                                        //agregar codigo para
                                        String auxtemp2 = TitusTools.GetTemp();
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        String auxetq = TitusTools.GetEtq();
                                        cad += "\t\t" + auxtemp2 + " = " + auxizq.Valor + " + " + auxder.Valor + ";\n";
                                        cad += "\t\t" + codigo3d.Valor + " = 1\n";
                                        cad += "\t\tif " + auxtemp2 + " > 0 then goto " + auxetq + "\n";
                                        cad += "\t\t" + codigo3d.Valor + " = 0\n";
                                        cad += "\t" + auxetq + ":\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " + " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    default:
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " + " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                        }
                        break;
                }
            }
            return codigo3d;
        }

        private Nodo3D Resta3D(FNodoExpresion izq, FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxizq = izq.Generar3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {

                switch (auxizq.Tipo)
                {
                    case Constante.TEntero:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;


                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            var cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TEntero;
                                        }
                                        else
                                        {
                                            var cad = "";

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TEntero;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " - " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TDecimal:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                        else
                                        {
                                            String cad = "";

                                            String auxtemp = TitusTools.GetTemp();
                                            String salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " - " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TCaracter:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;


                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " - " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TBooleano:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad += auxder.Codigo;
                                        cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad += auxder.Codigo;
                                        cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " - " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " - " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    default:
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " - " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                        }
                        break;
                }
            }
            return codigo3d;
        }

        private Nodo3D Resta3D(FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {
                switch (auxder.Tipo)
                {
                    case Constante.TEntero:
                        {
                            String cad = "";
                            codigo3d.Valor = TitusTools.GetTemp();
                            cad = "\t\t" + codigo3d.Valor + " =  - " + auxder.Valor + ";\n";
                            codigo3d.Codigo = auxder.Codigo + cad;
                            codigo3d.Tipo = Constante.TEntero;
                        }
                        break;

                    case Constante.TDecimal:
                        {
                            String cad = "";
                            codigo3d.Valor = TitusTools.GetTemp();
                            cad = "\t\t" + codigo3d.Valor + " =  - " + auxder.Valor + ";\n";
                            codigo3d.Codigo = auxder.Codigo + cad;
                            codigo3d.Tipo = Constante.TDecimal;
                        }
                        break;

                    case Constante.TCaracter:
                        {
                            String cad = "";
                            codigo3d.Valor = TitusTools.GetTemp();
                            cad = "\t\t" + codigo3d.Valor + " =  - " + auxder.Valor + ";\n";
                            codigo3d.Codigo = auxder.Codigo + cad;
                            codigo3d.Tipo = Constante.TEntero;
                        }
                        break;


                    case Constante.TBooleano:
                        {
                            if (auxder.V == "" && auxder.F == "")
                            {//si trae etiquetas viene de una relacional si no es un bool nativo
                                var cad = "";
                                codigo3d.Valor = TitusTools.GetTemp();
                                cad = "\t\t" + codigo3d.Valor + " =  - " + auxder.Valor + ";\n";
                                codigo3d.Codigo = auxder.Codigo + cad;
                                codigo3d.Tipo = Constante.TEntero;
                            }
                            else
                            {
                                var cad = "";

                                var auxtemp = TitusTools.GetTemp();
                                var salida = TitusTools.GetEtq();

                                cad += "\t" + auxder.V + ":\n";
                                cad += "\t\t" + auxtemp + " = 1;\n";
                                cad += "\t\t" + "goto " + salida + ";\n";
                                cad += "\t" + auxder.F + ":\n";
                                cad += "\t\t" + auxtemp + " = 0;\n";
                                cad += "\t" + salida + ":\n";

                                codigo3d.Valor = TitusTools.GetTemp();
                                cad += "\t\t" + codigo3d.Valor + " =  - " + auxtemp + ";\n";
                                codigo3d.Codigo = auxder.Codigo + cad;
                                codigo3d.Tipo = Constante.TEntero;
                            }
                        }
                        break;

                    default:
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede  - " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                        }
                        break;
                }
            }
            return codigo3d;
        }

        private Nodo3D Multiplicacion3D(FNodoExpresion izq, FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxizq = izq.Generar3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {

                switch (auxizq.Tipo)
                {
                    case Constante.TEntero:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            var cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TEntero;
                                        }
                                        else
                                        {
                                            var cad = "";

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TEntero;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " * " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TDecimal:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                        else
                                        {
                                            String cad = "";

                                            String auxtemp = TitusTools.GetTemp();
                                            String salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " * " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TCaracter:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;


                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " * " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TBooleano:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad += auxder.Codigo;
                                        cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad += auxder.Codigo;
                                        cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " * " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        var cad = "";

                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        if (auxder.V == "" && auxder.F == "")
                                        {
                                            cad += auxder.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxder.Codigo;
                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxder.Valor = auxtemp;
                                        }

                                        //agregar codigo para
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        String auxetq = TitusTools.GetEtq();
                                        cad += "\t\t" + codigo3d.Valor + " = 1\n";
                                        cad += "\t\tif " + auxizq.Valor + " > " + auxder.Valor + " then goto " + auxetq + "\n";
                                        cad += "\t\t" + codigo3d.Valor + " = 0\n";
                                        cad += "\t" + auxetq + ":\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " * " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    default:
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " * " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                        }
                        break;
                }
            }
            return codigo3d;
        }

        private Nodo3D Division3D(FNodoExpresion izq, FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxizq = izq.Generar3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {

                switch (auxizq.Tipo)
                {
                    case Constante.TEntero:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            var cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                        else
                                        {
                                            var cad = "";

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " / " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TDecimal:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                        else
                                        {
                                            String cad = "";

                                            String auxtemp = TitusTools.GetTemp();
                                            String salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " / " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TCaracter:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;


                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " / " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TBooleano:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad += auxder.Codigo;
                                        cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        if (auxizq.V == "" && auxizq.F == "")
                                        {
                                            cad += auxizq.Codigo;
                                        }
                                        else
                                        {
                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += auxizq.Codigo;
                                            cad += "\t" + auxizq.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxizq.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            auxizq.Valor = auxtemp;
                                        }
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad += auxder.Codigo;
                                        cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " / " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " / " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    default:
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " / " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                        }
                        break;
                }
            }
            return codigo3d;
        }

        private Nodo3D Potencia3D(FNodoExpresion izq, FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxizq = izq.Generar3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {

                switch (auxizq.Tipo)
                {
                    case Constante.TEntero:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TEntero;
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            var cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TEntero;
                                        }
                                        else
                                        {
                                            var cad = "";

                                            var auxtemp = TitusTools.GetTemp();
                                            var salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TEntero;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " ^ " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    case Constante.TDecimal:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";
                                        codigo3d.Valor = TitusTools.GetTemp();
                                        cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxder.Valor + ";\n";
                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        codigo3d.Tipo = Constante.TDecimal;
                                    }
                                    break;

                                case Constante.TBooleano:
                                    {
                                        if (auxder.V == "" && auxder.F == "")
                                        {//si trae etiquetas viene de una relacional si no es un bool nativo
                                            String cad = "";
                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad = "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxder.Valor + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                        else
                                        {
                                            String cad = "";

                                            String auxtemp = TitusTools.GetTemp();
                                            String salida = TitusTools.GetEtq();

                                            cad += "\t" + auxder.V + ":\n";
                                            cad += "\t\t" + auxtemp + " = 1;\n";
                                            cad += "\t\t" + "goto " + salida + ";\n";
                                            cad += "\t" + auxder.F + ":\n";
                                            cad += "\t\t" + auxtemp + " = 0;\n";
                                            cad += "\t" + salida + ":\n";

                                            codigo3d.Valor = TitusTools.GetTemp();
                                            cad += "\t\t" + codigo3d.Valor + " = " + auxizq.Valor + " ^ " + auxtemp + ";\n";
                                            codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                            codigo3d.Tipo = Constante.TDecimal;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " ^ " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    }
                                    break;
                            }
                        }
                        break;

                    default:
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " ^ " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                        }
                        break;
                }
            }
            return codigo3d;
        }

        private Nodo3D Relacional3D(FNodoExpresion izq, FNodoExpresion der)
        {
            var codigo3d = new Nodo3D();
            var auxizq = izq.Generar3D();
            var auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {
                switch (auxizq.Tipo)
                {
                    case Constante.TEntero:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;

                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " " + this.Tipo + " " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    break;
                            }
                        }
                        break;

                    case Constante.TDecimal:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " " + this.Tipo + " " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    break;
                            }
                        }
                        break;

                    case Constante.TCaracter:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TEntero:
                                    {
                                        String cad = "";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TDecimal:
                                    {
                                        String cad = "";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCaracter:
                                    {
                                        String cad = "";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCadena:
                                    {
                                        String cad = "";

                                        String etqcad1 = TitusTools.GetEtq();
                                        String etqcad2 = TitusTools.GetEtq();

                                        String tempascii2 = TitusTools.GetTemp();
                                        String tempcad2 = TitusTools.GetTemp();

                                        cad += "\t\t" + tempascii2 + " = 0 ;\n";
                                        cad += "\t" + etqcad1 + ":\n";
                                        cad += "\t\t" + tempcad2 + " = Heap[" + auxder.Valor + "];\n";
                                        cad += "\t\t" + "if " + tempcad2 + " == 0 goto " + etqcad2 + ";\n";
                                        cad += "\t\t" + tempascii2 + " = " + tempascii2 + " + " + tempcad2 + ";\n";
                                        cad += "\t\t" + auxder.Valor + " = " + auxder.Valor + " + 1;\n";
                                        cad += "\t\t" + "goto " + etqcad1 + ";\n";
                                        cad += "\t" + etqcad2 + ":\n";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + auxizq.Valor + " " + this.Tipo + " " + tempascii2 + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " " + this.Tipo + " " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    break;
                            }
                        }
                        break;

                    case Constante.TCadena:
                        {
                            switch (auxder.Tipo)
                            {
                                case Constante.TCaracter:
                                    {
                                        String cad = "";

                                        String tempascii1 = TitusTools.GetTemp();
                                        String tempcad1 = TitusTools.GetTemp();
                                        String etqcad1 = TitusTools.GetEtq();
                                        String etqcad2 = TitusTools.GetEtq();

                                        cad += "\t\t" + tempascii1 + " = 0 ;\n";
                                        cad += "\t" + etqcad1 + ":\n";
                                        cad += "\t\t" + tempcad1 + " = Heap[" + auxizq.Valor + "];\n";
                                        cad += "\t\t" + "if " + tempcad1 + " == 0 goto " + etqcad2 + ";\n";
                                        cad += "\t\t" + tempascii1 + " = " + tempascii1 + " + " + tempcad1 + ";\n";
                                        cad += "\t\t" + auxizq.Valor + " = " + auxizq.Valor + " + 1;\n";
                                        cad += "\t\t" + "goto " + etqcad1 + ";\n";
                                        cad += "\t" + etqcad2 + ":\n";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + tempascii1 + " " + this.Tipo + " " + auxder.Valor + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                case Constante.TCadena:
                                    {
                                        String cad = "";

                                        String tempascii1 = TitusTools.GetTemp();
                                        String tempcad1 = TitusTools.GetTemp();
                                        String etqcad1 = TitusTools.GetEtq();
                                        String etqcad2 = TitusTools.GetEtq();

                                        cad += "\t\t" + tempascii1 + " = 0 ;\n";
                                        cad += "\t" + etqcad1 + ":\n";
                                        cad += "\t\t" + tempcad1 + " = Heap[" + auxizq.Valor + "];\n";
                                        cad += "\t\t" + "if " + tempcad1 + " == 0 goto " + etqcad2 + ";\n";
                                        cad += "\t\t" + tempascii1 + " = " + tempascii1 + " + " + tempcad1 + ";\n";
                                        cad += "\t\t" + auxizq.Valor + " = " + auxizq.Valor + " + 1;\n";
                                        cad += "\t\t" + "goto " + etqcad1 + ";\n";
                                        cad += "\t" + etqcad2 + ":\n";

                                        String tempascii2 = TitusTools.GetTemp();
                                        String tempcad2 = TitusTools.GetTemp();
                                        etqcad1 = TitusTools.GetEtq();
                                        etqcad2 = TitusTools.GetEtq();

                                        cad += "\t\t" + tempascii2 + " = 0 ;\n";
                                        cad += "\t" + etqcad1 + ":\n";
                                        cad += "\t\t" + tempcad2 + " = Heap[" + auxder.Valor + "];\n";
                                        cad += "\t\t" + "if " + tempcad2 + " == 0 goto " + etqcad2 + ";\n";
                                        cad += "\t\t" + tempascii2 + " = " + tempascii2 + " + " + tempcad2 + ";\n";
                                        cad += "\t\t" + auxder.Valor + " = " + auxder.Valor + " + 1;\n";
                                        cad += "\t\t" + "goto " + etqcad1 + ";\n";
                                        cad += "\t" + etqcad2 + ":\n";

                                        codigo3d.V = TitusTools.GetEtq();
                                        codigo3d.F = TitusTools.GetEtq();

                                        cad += "\t\t" + "if " + tempascii1 + " " + this.Tipo + " " + tempascii2 + " goto " + codigo3d.V + ";\n";
                                        cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                                        codigo3d.Codigo = auxizq.Codigo + auxder.Codigo + cad;
                                        
                                        codigo3d.Tipo = Constante.TBooleano;
                                    }
                                    break;

                                default:
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " " + this.Tipo + " " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                    break;
                            }
                        }
                        break;
                }
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " " + this.Tipo + " " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
            }
            return codigo3d;
        }

        private Nodo3D Or3D(FNodoExpresion izq, FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxizq = izq.Generar3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {
                if (auxizq.Tipo == Constante.TBooleano && auxder.Tipo == Constante.TBooleano)
                {
                    var cad = "";

                    if (auxizq.V == "" && auxizq.F == "")
                    {
                        auxizq.V = TitusTools.GetEtq();
                        auxizq.F = TitusTools.GetEtq();

                        cad += auxizq.Codigo;

                        cad += "\t\t" + "if " + auxizq.Valor + " == 1 goto " + auxizq.V + ";\n";
                        cad += "\t\t" + "goto " + auxizq.F + ";\n";
                    }
                    else
                    {
                        cad += auxizq.Codigo;
                    }

                    cad += "\t" + auxizq.F + ":\n";


                    if (auxder.V == "" && auxder.F == "")
                    {
                        auxder.V = TitusTools.GetEtq();
                        auxder.F = TitusTools.GetEtq();

                        cad += auxder.Codigo;
                        cad += "\t\t" + "if " + auxder.Valor + " == 1 goto " + auxder.V + ";\n";
                        cad += "\t\t" + "goto " + auxder.F + ";\n";
                    }
                    else
                    {
                        cad += auxder.Codigo;
                    }

                    codigo3d.V = auxizq.V + ":\n\t" + auxder.V;
                    codigo3d.F = auxder.F;

                    codigo3d.Codigo = cad;
                    codigo3d.Tipo = Constante.TBooleano;
                }
                else
                {
                    TitusTools.InsertarError("Semantico", "No se puede " + auxizq.Tipo + " " + this.Tipo + " " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                }
            }            
            return codigo3d;
        }

        private Nodo3D And3D(FNodoExpresion izq, FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxizq = izq.Generar3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {
                if (auxizq.Tipo == Constante.TBooleano && auxder.Tipo == Constante.TBooleano)
                {
                    var cad = "";

                    if (auxizq.V == "" && auxizq.F == "")
                    {
                        auxizq.V = TitusTools.GetEtq();
                        auxizq.F = TitusTools.GetEtq();

                        cad += auxizq.Codigo;

                        cad += "\t\t" + "if " + auxizq.Valor + " == 1 goto " + auxizq.V + ";\n";
                        cad += "\t\t" + "goto " + auxizq.F + ";\n";
                    }
                    else
                    {
                        cad += auxizq.Codigo;
                    }

                    cad += "\t" + auxizq.V + ":\n";


                    if (auxder.V == "" && auxder.F == "")
                    {
                        auxder.V = TitusTools.GetEtq();
                        auxder.F = TitusTools.GetEtq();

                        cad += auxder.Codigo;
                        cad += "\t\t" + "if " + auxder.Valor + " == 1 goto " + auxder.V + ";\n";
                        cad += "\t\t" + "goto " + auxder.F + ";\n";
                    }
                    else
                    {
                        cad += auxder.Codigo;
                    }

                    codigo3d.V = auxder.V;
                    codigo3d.F = auxizq.F + ":\n\t" + auxder.F;

                    codigo3d.Codigo = cad;
                    codigo3d.Tipo = Constante.TBooleano;
                }
                else
                {
                    TitusTools.InsertarError("Semantico", "No se puede " + auxizq.Tipo + " " + this.Tipo + " " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                }
            }
            return codigo3d;
        }

        private Nodo3D Not3D(FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {
                if (auxder.Tipo == Constante.TBooleano)
                {
                    String cad = "";
                    
                    if (auxder.V == "" && auxder.F == "")
                    {
                        auxder.V = TitusTools.GetEtq();
                        auxder.F = TitusTools.GetEtq();

                        cad += auxder.Codigo;
                        cad += "\t\t" + "if " + auxder.Valor + " == 1 goto " + auxder.V + ";\n";
                        cad += "\t\t" + "goto " + auxder.F + ";\n";
                    }
                    else
                    {
                        cad += auxder.Codigo;
                    }

                    codigo3d.V = auxder.F;
                    codigo3d.F = auxder.V;

                    codigo3d.Codigo = cad;
                    codigo3d.Tipo = Constante.TBooleano;
                }
                else
                {
                    TitusTools.InsertarError("Semantico", "No se puede " + this.Tipo + " " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                }
            }
            return codigo3d;
        }

        private Nodo3D Xor3D(FNodoExpresion izq, FNodoExpresion der)
        {
            Nodo3D codigo3d = new Nodo3D();
            Nodo3D auxizq = izq.Generar3D();
            Nodo3D auxder = der.Generar3D();

            if (!TitusTools.HayErrores())
            {
                if (auxizq.Tipo == Constante.TBooleano && auxder.Tipo == Constante.TBooleano)
                {
                    String cad = "";
                    String etq1v = TitusTools.GetEtq();
                    String etq1f = TitusTools.GetEtq();
                    codigo3d.V = TitusTools.GetEtq();
                    codigo3d.F = TitusTools.GetEtq();
                    String etq2f = TitusTools.GetEtq();
                    String temp1 = TitusTools.GetTemp();
                    String temp2 = TitusTools.GetTemp();

                    if (auxizq.V == "" && auxizq.F == "")
                    {
                        auxizq.V = TitusTools.GetEtq();
                        auxizq.F = TitusTools.GetEtq();

                        cad += auxizq.Codigo;
                        cad += "\t\t" + "if " + auxizq.Valor + " == 1 goto " + auxizq.V + ";\n";
                        cad += "\t\t" + "goto " + auxizq.F + ";\n";
                    }
                    else
                    {
                        cad += auxizq.Codigo;
                    }

                    cad += "\t\t" + auxizq.V + ":\n";
                    cad += "\t\t" + temp1 + " = 1;\n";
                    cad += "\t\t" + "goto " + etq1v + ";\n";
                    cad += "\t\t" + auxizq.F + ":\n";
                    cad += "\t\t" + temp1 + " = 0;\n";
                    cad += "\t" + etq1v + ":\n";




                    if (auxder.V == "" && auxder.F == "")
                    {
                        auxder.V = TitusTools.GetEtq();
                        auxder.F = TitusTools.GetEtq();

                        cad += auxder.Codigo;
                        cad += "\t\t" + "if " + auxder.Valor + " == 1 goto " + auxder.V + ";\n";
                        cad += "\t\t" + "goto " + auxder.F + ";\n";
                    }
                    else
                    {
                        cad += auxder.Codigo;
                    }

                    cad += "\t\t" + auxder.V + ":\n";
                    cad += "\t\t" + temp2 + " = 1;\n";
                    cad += "\t\t" + "goto " + etq1f + ";\n";
                    cad += "\t\t" + auxder.F + ":\n";
                    cad += "\t\t" + temp2 + " = 0;\n";
                    cad += "\t" + etq1f + ":\n";

                    cad += "\t\t" + "if " + temp1 + " != " + temp2 + " goto " + codigo3d.V + ";\n";
                    cad += "\t\t" + "goto " + codigo3d.F + ";\n";

                    codigo3d.Codigo = cad;
                    codigo3d.Tipo = Constante.TBooleano;
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede " + auxizq.Tipo + " " + this.Tipo + " " + auxder.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                }
            }
            
            return codigo3d;
        }

        public FNodoExpresion ResolverExpresion()
        {
            return ResolverExpresion(this);
        }

        private FNodoExpresion ResolverExpresion(FNodoExpresion nodo)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, nodo.Fila, nodo.Columna, null);
            FNodoExpresion izq = nodo.Izquierda;
            FNodoExpresion der = nodo.Derecha;
            if (nodo.Izquierda != null)
            {
                izq = nodo.Izquierda.ResolverExpresion();
            }
            if (nodo.Derecha != null)
            {
                der = nodo.Derecha.ResolverExpresion();
            }
            switch (nodo.Tipo)
            {
                case Constante.TMas:
                    aux = Suma(izq, der);
                    break;

                case Constante.TMenos:
                    if (nodo.Izquierda != null)
                    {
                        aux = Resta(izq, der);
                    }
                    else
                    {
                        aux = Resta(der);
                    }
                    break;

                case Constante.TPor:
                    aux = Multiplicacion(izq, der);
                    break;

                case Constante.TDivision:
                    aux = Division(izq, der);
                    break;

                case Constante.TPotencia:
                    aux = Potencia(izq, der);
                    break;

                case Constante.TPotenciaOCL:
                    aux = Potencia(izq, der);
                    break;

                case Constante.TAumento:
                    aux = Aumento(izq);
                    break;

                case Constante.TDecremento:
                    aux = Disminucion(izq);
                    break;

                case Constante.TMayor:
                    aux = Mayor(izq, der);
                    break;

                case Constante.TMenor:
                    aux = Menor(izq, der);
                    break;

                case Constante.TMayorIgual:
                    aux = MayorIgual(izq, der);
                    break;

                case Constante.TMenorIgual:
                    aux = MenorIgual(izq, der);
                    break;

                case Constante.TIgualacion:
                    aux = Igual(izq, der);
                    break;

                case Constante.TDiferenciacion:
                    aux = Diferente(izq, der);
                    break;

                case Constante.TOr:
                    aux = Or(izq, der);
                    break;

                case Constante.TXor:
                    aux = Xor(izq, der);
                    break;

                case Constante.TAnd:
                    aux = And(izq, der);
                    break;

                case Constante.TNot:
                    aux = Not(der);
                    break;

                case Constante.TEntero:
                    aux = new FNodoExpresion(nodo);
                    break;

                case Constante.TDecimal:
                    aux = new FNodoExpresion(nodo);
                    break;

                case Constante.TCaracter:
                    aux = new FNodoExpresion(nodo);
                    break;

                case Constante.TCadena:
                    aux = new FNodoExpresion(nodo);
                    break;

                case Constante.TBooleano:
                    aux = new FNodoExpresion(nodo);
                    break;

            }
            return aux;
        }

        public FNodoExpresion Suma(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero + der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + der.Caracter);

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Entero.ToString() + der.Cadena);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + der.Caracter);

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Decimal.ToString() + der.Cadena);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter + der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter + der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Caracter.ToString() + der.Caracter.ToString());

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Caracter.ToString() + der.Cadena);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCadena:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Cadena + der.Entero.ToString());
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Cadena + der.Decimal.ToString());
                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Cadena + der.Caracter.ToString());

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Fila, Columna, izq.Cadena + der.Cadena);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero + der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Bool || der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede +, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Resta(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero - der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter - der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter - der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero - der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Resta(FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (der.Tipo)
            {
                case Constante.TEntero:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, -der.Entero);
                    break;

                case Constante.TDecimal:
                    aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, -der.Decimal);

                    break;

                case Constante.TCaracter:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, -der.Caracter);

                    break;

                case Constante.TBooleano:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, -der.Entero);
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede -, " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }
            return aux;
        }

        public FNodoExpresion Multiplicacion(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero * der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero * der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero * der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero * der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede *, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal * der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal * der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal * der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal * der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede *, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter * der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter * der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede *, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero * der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero * der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Bool && der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede *, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Division(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede /, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal / der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal / der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal / der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal / der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede /, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter / der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Caracter / der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede /, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Entero / der.Decimal);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede /, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Potencia(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            double p = Math.Pow(izq.Entero, der.Entero);
                            int v = (int)p;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, v);

                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Entero, der.Decimal));

                            break;

                        case Constante.TCaracter:
                            double pc = Math.Pow(izq.Entero, der.Entero);
                            int vc = (int)pc;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, vc);

                            break;

                        case Constante.TBooleano:
                            double pb = Math.Pow(izq.Entero, der.Entero);
                            int vb = (int)pb;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, vb);
                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ^, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Decimal, der.Entero));
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Decimal, der.Decimal));

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Decimal, der.Caracter));

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Decimal, der.Entero));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ^, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            double pe = Math.Pow(izq.Entero, der.Entero);
                            int ve = (int)pe;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, ve);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Caracter, der.Decimal));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ^, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            double p = Math.Pow(izq.Entero, der.Entero);
                            int v = (int)p;
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, v);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, Math.Pow(izq.Entero, der.Decimal));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ^, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Aumento(FNodoExpresion izq)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero + 1);
                    break;

                case Constante.TDecimal:
                    aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal + 1);
                    break;

                case Constante.TCaracter:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter + 1);
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ++, " + izq.Tipo, "Aqui va ruta", Fila, Columna);
                    break;

            }
            return aux;
        }

        public FNodoExpresion Disminucion(FNodoExpresion izq)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero - 1);
                    break;

                case Constante.TDecimal:
                    aux = new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Fila, Columna, izq.Decimal - 1);
                    break;

                case Constante.TCaracter:
                    aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Caracter - 1);
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ++, " + izq.Tipo, "Aqui va ruta", Fila, Columna);
                    break;

            }
            return aux;
        }

        public FNodoExpresion Mayor(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero > der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal > der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal > der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal > der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal > der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter > der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter > der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter > der.Caracter);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero > der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Menor(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero < der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede <, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal < der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal < der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal < der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal < der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede <, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter < der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter < der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter < der.Caracter);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero < der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede <, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Igual(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero == der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal == der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal == der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal == der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal == der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter == der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter == der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter == der.Caracter);

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter.ToString().Equals(der.Cadena));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCadena:
                    switch (der.Tipo)
                    {
                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Cadena.Equals(der.Cadena));
                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Cadena.Equals(der.Caracter.ToString()));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero == der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Bool == der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion Diferente(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TEntero:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Fila, Columna, izq.Entero != der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TDecimal:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal != der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal != der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal != der.Caracter);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Decimal != der.Entero);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCaracter:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter != der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter != der.Decimal);

                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Caracter != der.Caracter);

                            break;

                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, !izq.Caracter.ToString().Equals(der.Cadena));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ==, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TCadena:
                    switch (der.Tipo)
                    {
                        case Constante.TCadena:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, !izq.Cadena.Equals(der.Cadena));
                            break;

                        case Constante.TCaracter:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, !izq.Cadena.Equals(der.Caracter.ToString()));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TEntero:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Entero);
                            break;

                        case Constante.TDecimal:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Entero != der.Decimal);

                            break;

                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Bool != der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

            }
            return aux;
        }

        public FNodoExpresion MayorIgual(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);
            FNodoExpresion mayor = Mayor(izq, der);
            FNodoExpresion igual = Igual(izq, der);

            switch (mayor.Tipo)
            {
                case Constante.TBooleano:
                    switch (igual.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, mayor.Bool || igual.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede >=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;
            }
            return aux;
        }

        public FNodoExpresion MenorIgual(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);
            FNodoExpresion menor = Menor(izq, der);
            FNodoExpresion igual = Igual(izq, der);

            switch (menor.Tipo)
            {
                case Constante.TBooleano:
                    switch (igual.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, menor.Bool || igual.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede <=, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;
            }
            return aux;
        }

        public FNodoExpresion Or(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Bool || der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ||, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede ||, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }
            return aux;
        }

        public FNodoExpresion And(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, izq.Bool && der.Bool);

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede &&, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede &&, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }
            return aux;
        }

        public FNodoExpresion Not(FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (der.Tipo)
            {
                case Constante.TBooleano:
                    aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, !der.Bool);

                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede !, " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }

            return aux;
        }

        public FNodoExpresion Xor(FNodoExpresion izq, FNodoExpresion der)
        {
            FNodoExpresion aux = new FNodoExpresion(null, null, Constante.TErrorSemantico, Constante.TErrorSemantico, Fila, Columna, null);

            switch (izq.Tipo)
            {
                case Constante.TBooleano:
                    switch (der.Tipo)
                    {
                        case Constante.TBooleano:
                            aux = new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Fila, Columna, (!izq.Bool && der.Bool) || (izq.Bool && !der.Bool));

                            break;

                        default:
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede XOR/??, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                            break;
                    }
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede XOR/??, " + izq.Tipo + " con " + der.Tipo, "Aqui va ruta", Fila, Columna);
                    break;
            }
            return aux;
        }
    }
}
