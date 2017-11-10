using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FImprimir
    {
        public FNodoExpresion Valor;
        public Simbolo Padre;

        public FImprimir(FNodoExpresion Valor)
        {
            this.Valor = Valor;
        }

        public String Generar3D()
        {
            String cadena = "";

            Nodo3D val = Valor.Generar3D();

            cadena += val.Codigo;
            switch (val.Tipo)
            {                
                case Constante.TCadena:
                    {
                        String temp = TitusTools.GetTemp();
                        String etq = TitusTools.GetEtq();

                        cadena += "\t" + etq + "://comienza imprimir\n";
                        cadena += "\t\t" + temp + " = " + "Heap[" + val.Valor + "];\n";
                        cadena += "\t\t" + "print(\"%c\", " + temp + ");\n";
                        cadena += "\t\t" + val.Valor + " = " + val.Valor + " + 1 ;\n";
                        cadena += "\t\t" + "if " + temp + " != 0 goto " + etq + ";//Termina imprimir\n";
                    }
                    break;

                case Constante.TCaracter:
                    {
                        cadena += "\t\t" + "print(\"%c\", " + val.Valor + ");//Termina imprimir\n";
                    }
                    break;

                case Constante.TEntero:
                    {
                        cadena += "\t\t" + "print(\"%d\", " + val.Valor + ");//Termina imprimir\n";
                    }
                    break;

                case Constante.TDecimal:
                    {
                        cadena += "\t\t" + "print(\"%f\", " + val.Valor + ");//Termina imprimir\n";
                    }
                    break;

                default:
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede imprimir un tipo " + val.Tipo, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
                    break;

            }

            return cadena;
        }
    }
}
