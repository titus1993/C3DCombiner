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
    }
}
