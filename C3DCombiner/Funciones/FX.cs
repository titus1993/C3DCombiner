using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FX
    {
        public Ambito Ambito;
        public FNodoExpresion Condicion1;
        public FNodoExpresion Condicion2;

        public Simbolo Padre;

        public FX(FNodoExpresion Condicion1, FNodoExpresion Condicion2, Ambito Ambito)
        {
            this.Condicion1 = Condicion1;
            this.Condicion2 = Condicion2;
            this.Ambito = Ambito;
            this.Padre = null;
        }

        public String Generar3D()
        {
            String cadena = "\t\t//Comienza mientras\n";
            String retorno = TitusTools.GetEtq();

            Nodo3D cond = Condicion1.Generar3D();
            if (!TitusTools.HayErrores())
            {
                if (cond.Tipo == Constante.TBooleano)
                {
                    cadena += "\t" + retorno + ":\n";
                    cadena += cond.Codigo;
                    if (cond.V == "" && cond.F == "")
                    {
                        cond.V = TitusTools.GetEtq();
                        cond.F = TitusTools.GetEtq();

                        cadena += "\t\t" + "if " + cond.Valor + " == 1 goto " + cond.V + ";\n";
                        cadena += "\t\t" + "goto " + cond.F + ";\n";

                    }
                    cadena += "\t" + cond.V + ":\n";

                    foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                    {
                        cadena += sim.Generar3D();
                    }

                    cadena += "\t\t" + "goto " + retorno + ";\n";
                    cadena += "\t" + cond.F + "://Termina mientras\n";
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "El ciclo while esperaba un tipo booleano no un tipo " + cond.Tipo, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
                }
            }

            return cadena;
        }
    }
}
