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
            String cadena = "\t\t//Comienza ciclo X\n";
            String retorno = TitusTools.GetEtq();


            FNodoExpresion or = new FNodoExpresion(Condicion1, Condicion2, Constante.TOr, Constante.TOr, Condicion1.Fila, Condicion1.Columna, null);
            FNodoExpresion and = new FNodoExpresion(Condicion1, Condicion2, Constante.TAnd, Constante.TAnd, Condicion1.Fila, Condicion1.Columna, null);

            Nodo3D cond1 = or.Generar3D();
            Nodo3D cond2 = and.Generar3D();
            if (!TitusTools.HayErrores())
            {
                cadena += cond1.Codigo;
                cadena += "\t" + retorno + ":\n";
                cadena += cond2.Codigo;
                cadena += "\t" + cond1.V + ":\n";
                cadena += "\t" + cond2.V + ":\n";

                foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                {
                    cadena += sim.Generar3D();
                }

                cadena += "\t\t" + "goto " + retorno + ";\n";
                cadena += "\t" + cond1.F + ":\n";
                cadena += "\t" + cond2.F + ":\n";
                
            }

            cadena += "\t\t//Termina ciclo X\n";

            cadena = cadena.Replace("§salir§;", "goto " + cond1.F + ";\n");
            cadena = cadena.Replace("§continuar§;", "goto " + retorno + ";\n");

            return cadena;
        }
    }
}
