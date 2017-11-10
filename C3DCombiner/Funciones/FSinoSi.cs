using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FSinoSi
    {

        public Ambito Ambito;
        public FNodoExpresion Condicion;

        public Simbolo Padre;

        public FSinoSi(FNodoExpresion Condicion, Ambito Ambito)
        {
            this.Condicion = Condicion;
            this.Ambito = Ambito;
            this.Padre = null;
        }

        public String Generar3D(String salida)
        {
            String cadena = "\t\t//Comienza sino si\n";

            Nodo3D cond = Condicion.Generar3D();

            if (!TitusTools.HayErrores())
            {
                if (cond.Tipo == Constante.TBooleano)
                {
                    cadena += cond.Codigo;
                    if (cond.V != "" && cond.F != "")
                    {
                        cadena += "\t" + cond.V + ":\n";
                        foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                        {
                            cadena += sim.Generar3D();
                        }
                    }
                    else
                    {
                        cond.V = TitusTools.GetEtq();
                        cond.F = TitusTools.GetEtq();

                        cadena += "\t\t" + "if " + cond.Valor + " == 1 goto " + cond.V + ";\n";
                        cadena += "\t\t" + "goto " + cond.F + ";\n";
                        cadena += "\t" + cond.V + ":\n";
                        foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                        {
                            cadena += sim.Generar3D();
                        }
                    }

                    cadena += "\t\t" + "goto " + salida + ";\n";
                    cadena += "\t" + cond.F + ":\n";
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "La sentencia sino si esperaba un tipo booleano no un tipo " + cond.Tipo, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
                }
            }            

            return cadena;
        }
    }
}
