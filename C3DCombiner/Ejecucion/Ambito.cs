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
        public String Nombre;
        public int Tamaño;
        public Ambito(String nombre, List<Simbolo> tablasimbolo)
        {
            this.TablaSimbolo = tablasimbolo;
            this.Nombre = nombre;

            Simbolo Hermano = null;

            foreach (Simbolo s in tablasimbolo)
            {
                s.Hermano = Hermano;
                Hermano = s;
            }


        }

        public Ambito(String nombre)
        {
            this.Nombre = nombre;
            this.TablaSimbolo = new List<Simbolo>();
        }
    }
}
