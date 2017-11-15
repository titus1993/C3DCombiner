using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FHacer
    {
        public Ambito Ambito;
        public FNodoExpresion Condicion;

        public Simbolo Padre;

        public FHacer(FNodoExpresion Condicion, Ambito Ambito)
        {
            this.Condicion = Condicion;
            this.Ambito = Ambito;
            this.Padre = null;
        }

        public String Generar3D()
        {
            String cadena = "\t\t//Comienza hacer mientras\n";

            Nodo3D cond = Condicion.Generar3D();

            if (cond.Tipo == Constante.TBooleano)
            {
                if (cond.V != "" && cond.F != "")
                {
                    cadena += "\t" + cond.V + ":\n";
                    foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                    {
                        cadena += sim.Generar3D();
                    }
                    cadena += cond.Codigo;
                }
                else
                {
                    cond.V = TitusTools.GetEtq();
                    cond.F = TitusTools.GetEtq();

                    cadena += "\t" + cond.V + ":\n";
                    foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                    {
                        cadena += sim.Generar3D();
                    }
                    cadena += cond.Codigo;
                    cadena += "\t\t" + "if " + cond.Valor + " == 1 goto " + cond.V + ";\n";
                    cadena += "\t\t" + "goto " + cond.F + ";\n";
                }

                cadena += "\t" + cond.F + "://Termina hacer mientras\n";

                cadena = cadena.Replace("§salir§;", "goto " + cond.F + ";\n");
                cadena = cadena.Replace("§continuar§;", "goto " + cond.V + ";\n");
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "El ciclo hacer mientras esperaba un tipo booleano no un tipo " + cond.Tipo, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
            }

            return cadena;
        }
    }
}
