using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Ejecucion
{
    class Ambito
    {
        public List<Simbolo> TablaSimbolo;
        public Ambito Padre;
        public String Nombre;
        public int Tamaño;
        public Ambito(String nombre, List<Simbolo> tablasimbolo)
        {
            this.TablaSimbolo = tablasimbolo;
            this.Padre = null;
            this.Nombre = nombre;

            int pos = 0;    
            foreach (Simbolo sim in TablaSimbolo)
            {
                if (sim.Rol.Equals(Constante.DECLARACION))
                {
                    sim.Posicion = pos;
                    pos++;
                    this.Tamaño += sim.Tamaño;
                }
            }


        }

        public Ambito(String nombre)
        {
            this.Nombre = nombre;
            this.TablaSimbolo = new List<Simbolo>();
        }
    }
}
