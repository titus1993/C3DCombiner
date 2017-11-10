using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FSi
    {
        public List<FSinoSi> SinoSi;
        public FSino Sino;
        public Ambito Ambito;
        public FNodoExpresion Condicion;

        public Simbolo Padre;

        public FSi(FNodoExpresion condicion, Ambito ambito, List<FSinoSi> sinosi, FSino sino)
        {
            this.Condicion = condicion;
            this.Ambito = ambito;
            this.SinoSi = sinosi;
            this.Sino = sino;
            this.Padre = null;
        }

        public String Generar3D()
        {
            String cadena = "\t\t//Comienza si\n";

            Nodo3D cond = Condicion.Generar3D();

            if (cond.Tipo == Constante.TBooleano)
            {
                String salida = TitusTools.GetEtq();
                //Parte del if
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
                    cadena += "\t\t" + "goto " + cond.F + ":\n";
                    cadena += "\t" + cond.V + ":\n";
                    foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                    {
                        cadena += sim.Generar3D();
                    }                    
                }
                cadena += "\t\t" + "goto " + salida + ";\n";
                cadena += "\t" + cond.F + ":\n";

                //parte de los sinosi
                foreach(FSinoSi sinosi in SinoSi)
                {
                    cadena += sinosi.Generar3D(salida);
                }

                if (Sino != null)
                {
                    cadena += Sino.Generar3D(salida);
                }
                cadena += "\t" + salida + "://Termina si\n";
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "La sentencia si esperaba un tipo booleano no un tipo " + cond.Tipo, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
            }

            return cadena;
        }
    }
}
