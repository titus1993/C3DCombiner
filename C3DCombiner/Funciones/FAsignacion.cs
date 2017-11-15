using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FAsignacion
    {
        public Ambito Ambito;
        public String Tipo;
        public FLlamadaObjeto Nombre;
        public FNodoExpresion Operacion;
        public FNodoExpresion Valor;

        public Simbolo Padre;

        public FAsignacion(String tipo, Ambito ambito, FNodoExpresion valor, Object nombre)
        {
            this.Tipo = tipo;
            this.Ambito = ambito;
            this.Valor = valor;
            if (this.Tipo.Equals(Constante.TAumento) || this.Tipo.Equals(Constante.TDecremento))
            {
                this.Operacion = (FNodoExpresion)nombre;
            }
            else
            {
                this.Nombre = (FLlamadaObjeto)nombre;
            }
            this.Padre = null;
        }

        public String Generar3D()
        {
            String cadena = "\t//comienza asignacion\n";
            if (Tipo.Equals(Constante.LLAMADA_OBJETO))
            {
                Nodo3D izq = Nombre.Generar3DAsginacion();
                Nodo3D val = Valor.Generar3D();
                cadena += izq.Codigo;
                cadena += val.Codigo;
                if (!TitusTools.HayErrores())
                {
                    switch (izq.Tipo)
                    {
                        case Constante.TEntero:
                            {
                                switch (val.Tipo)
                                {
                                    case Constante.TEntero:
                                        {
                                            cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
                                        }
                                        break;

                                    case Constante.TCaracter:
                                        {
                                            cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
                                        }
                                        break;

                                    case Constante.TBooleano:
                                        {
                                            if (val.V == "" && val.F == "")
                                            {//si trae etiquetas viene de una relacional si no es un bool nativo
                                                cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
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

                                                cadena += "\t\t" + izq.Valor + " = " +auxtemp + ";\n";
                                            }
                                        }
                                        break;

                                    default:
                                        {
                                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede  asignar un valor " + val.Tipo + " a una variable " + izq.Tipo, TitusTools.GetRuta(), this.Padre.Fila, this.Padre.Columna);
                                        }
                                        break;

                                }
                            }
                            break;

                        case Constante.TDecimal:
                            {
                                switch (val.Tipo)
                                {
                                    case Constante.TEntero:
                                        {
                                            cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
                                        }
                                        break;

                                    case Constante.TDecimal:
                                        {
                                            cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
                                        }
                                        break;

                                    case Constante.TCaracter:
                                        {
                                            cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
                                        }
                                        break;

                                    case Constante.TBooleano:
                                        {
                                            if (val.V == "" && val.F == "")
                                            {//si trae etiquetas viene de una relacional si no es un bool nativo
                                                cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
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

                                                cadena += "\t\t" + izq.Valor + " = " + auxtemp + ";\n";
                                            }
                                        }
                                        break;

                                    default:
                                        {
                                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede  asignar un valor " + val.Tipo + " a una variable " + izq.Tipo, TitusTools.GetRuta(), this.Padre.Fila, this.Padre.Columna);
                                        }
                                        break;

                                }
                            }
                            break;

                        case Constante.TCaracter:
                            {
                                switch (val.Tipo)
                                {
                                    case Constante.TEntero:
                                        {
                                            cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
                                        }
                                        break;

                                    case Constante.TCaracter:
                                        {
                                            cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
                                        }
                                        break;

                                    default:
                                        {
                                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede  asignar un valor " + val.Tipo + " a una variable " + izq.Tipo, TitusTools.GetRuta(), this.Padre.Fila, this.Padre.Columna);
                                        }
                                        break;

                                }
                            }
                            break;

                        case Constante.TCadena:
                            {
                                switch (val.Tipo)
                                {
                                    case Constante.TCadena:
                                        {
                                            cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
                                        }
                                        break;

                                    default:
                                        {
                                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede  asignar un valor " + val.Tipo + " a una variable " + izq.Tipo, TitusTools.GetRuta(), this.Padre.Fila, this.Padre.Columna);
                                        }
                                        break;

                                }
                            }
                            break;

                        case Constante.TBooleano:
                            {
                                switch (val.Tipo)
                                {

                                    case Constante.TBooleano:
                                        {
                                            if (val.V == "" && val.F == "")
                                            {//si trae etiquetas viene de una relacional si no es un bool nativo
                                                cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
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

                                                cadena += "\t\t" + izq.Valor + " = " + auxtemp + ";\n";
                                            }
                                        }
                                        break;

                                    default:
                                        {
                                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede  asignar un valor " + val.Tipo + " a una variable " + izq.Tipo, TitusTools.GetRuta(), this.Padre.Fila, this.Padre.Columna);
                                        }
                                        break;

                                }
                            }
                            break;

                        default:
                            {
                                if (izq.Tipo.Equals(val.Tipo))
                                {
                                    cadena += "\t\t" + izq.Valor + " = " + val.Valor + ";\n";
                                }
                                else
                                {
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede  asignar un valor " + val.Tipo + " a una variable " + izq.Tipo, TitusTools.GetRuta(), this.Padre.Fila, this.Padre.Columna);
                                }
                            }
                            break;

                    }
                }
            }
            else
            {
                if (this.Operacion.Tipo.Equals(Constante.LLAMADA_OBJETO))
                {
                    Nodo3D nodo = this.Operacion.LlamadaObjeto.Generar3DAsginacion();
                    if (nodo.Tipo.Equals(Constante.TEntero) || nodo.Tipo.Equals(Constante.TDecimal) || nodo.Tipo.Equals(Constante.TCaracter))
                    {
                        cadena += nodo.Codigo;
                        String temp = TitusTools.GetTemp();
                        String tempcosa = TitusTools.GetTemp();
                        cadena += "\t\t" + temp + " = " + nodo.Valor + ";\n";
                        if (Tipo.Equals(Constante.TAumento))
                        {
                            cadena += "\t\t" + tempcosa + " = " + temp + " + 1;\n";
                        }
                        else
                        {
                            cadena += "\t\t" + tempcosa + " = " + temp + " - 1;\n";
                        }
                        cadena += "\t\t" + nodo.Valor + " = " + tempcosa + ";\n";                        
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "Solo se puede hacer aumento o decremento a una variable", TitusTools.GetRuta(), this.Padre.Fila, this.Padre.Columna);
                }
            }
            cadena += "\t//termina asignacion\n";
            return cadena;
        }
    }
}
