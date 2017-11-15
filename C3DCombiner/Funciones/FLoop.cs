using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FLoop
    {
        public Ambito Ambito;

        public Simbolo Padre;

        public FLoop(Ambito Ambito)
        {
            this.Ambito = Ambito;
            this.Padre = null;
        }

        public String Generar3D()
        {
            String cadena = "\t\t//Comienza loop\n";


            String V = TitusTools.GetEtq();
            String F = TitusTools.GetEtq();

            cadena += "\t" + V + ":\n";
            foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
            {
                cadena += sim.Generar3D();
            }            
            cadena += "\t\t" + "goto " + V + ";\n";
            
            cadena += "\t" + F + "://Termina loop\n";

            cadena = cadena.Replace("§salir§;", "goto " + F + ";\n");
            cadena = cadena.Replace("§continuar§;", "goto " + V + ";\n");

            return cadena;
        }
    }
}
