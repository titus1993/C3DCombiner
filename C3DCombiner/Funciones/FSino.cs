using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FSino
    {
        public Ambito Ambito;

        public Simbolo Padre;

        public FSino(Ambito Ambito)
        {
            this.Ambito = Ambito;
            this.Padre = null;
        }

        public String Generar3D(String salida)
        {
            String cadena = "\t\t//Comienza sino\n";

            if (!TitusTools.HayErrores())
            {
                foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                {
                    cadena += sim.Generar3D();
                }
                cadena += "\t\t" + "//Termina sino\n";
            }

            return cadena;
        }
    }
}
