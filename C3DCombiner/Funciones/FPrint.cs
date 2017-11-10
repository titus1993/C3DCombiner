using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FPrint
    {
        public String Tipo, Temporal;
        public Simbolo Padre;

        public FPrint(String tipo, String temporal)
        {
            this.Tipo = tipo;
            this.Temporal = temporal;
        }

        public void Ejecutar()
        {
            switch (this.Tipo)
            {
                case Constante.PrintChar:
                    ImprimirCaracter();
                    break;

                case Constante.PrintNum:
                    ImprimirEntero();
                    break;

                case Constante.PrintDouble:
                    ImprimirDecimal();
                    break;
            }
        }

        public void ImprimirCaracter()
        {
            Variable v = Tabla3D.BuscarVariable(Temporal);
            if (v != null)
            {
                if (v.Valor != null)
                {
                    FNodoExpresion valor = (FNodoExpresion)v.Valor;
                    if (valor.Tipo.Equals(Constante.TEntero))
                    {
                        if (valor.Entero >= 0)
                        {
                            char c = (char)valor.Entero;                            
                            TitusTools.ImprimirConsola(c.ToString());
                            if (valor.Entero==0)
                            {
                                TitusTools.ImprimirConsola("\n");
                            }
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se pueden imprimir un caracter con un ascii negativo en el temporal " + Temporal, "", Padre.Fila, Padre.Columna);
                        }
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede imprimir un caracter porque el valor no es entero del temporal " + Temporal, "", Padre.Fila, Padre.Columna);
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se ha asignado ningun valor al temporal " + Temporal, "", Padre.Fila, Padre.Columna);
                }
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No existe el temporal " + Temporal, "", Padre.Fila, Padre.Columna);
            }
        }

        public void ImprimirEntero()
        {
            Variable v = Tabla3D.BuscarVariable(Temporal);
            if (v != null)
            {
                if (v.Valor != null)
                {
                    FNodoExpresion valor = (FNodoExpresion)v.Valor;
                    if (valor.Tipo.Equals(Constante.TEntero))
                    {
                        TitusTools.ImprimirConsola(valor.Entero.ToString());
                        TitusTools.ImprimirConsola("\n");
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede imprimir un entero porque el valor no es entero del temporal " + Temporal, "", Padre.Fila, Padre.Columna);
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se ha asignado ningun valor al temporal " + Temporal, "", Padre.Fila, Padre.Columna);
                }
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No existe el temporal " + Temporal, "", Padre.Fila, Padre.Columna);
            }
        }

        public void ImprimirDecimal()
        {
            Variable v = Tabla3D.BuscarVariable(Temporal);
            if (v != null)
            {
                if (v.Valor != null)
                {
                    FNodoExpresion valor = (FNodoExpresion)v.Valor;
                    if (valor.Tipo.Equals(Constante.TDecimal))
                    {
                        TitusTools.ImprimirConsola(valor.Decimal.ToString());
                        TitusTools.ImprimirConsola("\n");
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede imprimir un decimal porque el valor no es decimal del temporal " + Temporal, "", Padre.Fila, Padre.Columna);
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se ha asignado ningun valor al temporal " + Temporal, "", Padre.Fila, Padre.Columna);
                }
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No existe el temporal " + Temporal, "", Padre.Fila, Padre.Columna);
            }
        }
    }
}
