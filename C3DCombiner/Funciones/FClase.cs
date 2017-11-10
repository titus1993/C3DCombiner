using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FClase
    {
        public String Herencia = "";
        public Ambito Ambito;
        public String Nombre = "";

        public Simbolo Padre;

        public FClase(String Nombre, String Herencia, Ambito Ambito)
        {
            this.Nombre = Nombre;
            this.Herencia = Herencia;
            this.Ambito = Ambito;
            this.Padre = null;
        }


        public String Generar3D()
        {
            String cadena = "";
            String temp = TitusTools.GetTemp();
            String init = "void init_" + this.Nombre + "(){\n";
            //init += "\t\t" + temp + " = H;\n";
            //init += "\t\t" + "H = H + " + Ambito.Tamaño.ToString() + ";\n";
            foreach (Simbolo sim in Ambito.TablaSimbolo)
            {
                if (!sim.Rol.Equals(Constante.DECLARACION))
                {
                    cadena += sim.Generar3D();
                }
                else
                {
                    FDeclaracion decla = (FDeclaracion)sim.Valor;
                    init += decla.Generar3DInit(temp);
                }
            }

            init += "}\n\n";
            init += cadena;
            return init;
        }
    }
}
