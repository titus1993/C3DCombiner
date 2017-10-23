using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Ejecucion
{
    class Archivo
    {
        public String Ruta { get; set; }

        public List<String> Imports;

        public DataTable TablaSimbolos = new DataTable();

        public Ambito Clases;

        public Archivo(List<String>Imports, Ambito Clases, String Ruta)
        {
            this.Imports = Imports;
            this.Clases = Clases;
            this.Ruta = Ruta;
        }

        public void IniciarTablaSimbolos()
        {
            TablaSimbolos = new DataTable();
            TablaSimbolos.Columns.Add("Rol");
            TablaSimbolos.Columns.Add("Tipo");
            TablaSimbolos.Columns.Add("Ambito");
            TablaSimbolos.Columns.Add("Nombre");
            TablaSimbolos.Columns.Add("Tamaño");
            TablaSimbolos.Columns.Add("Posicion");
        }

        public void InsertarTablaSimbolos(String rol, String tipo, String ambito, String nombre, String tamanio, String posicion)
        {
            TablaSimbolos.Rows.Add(rol, tipo, ambito, nombre, tamanio, posicion);
        }

        public void GenerarTablaSimbolos()
        {
            IniciarTablaSimbolos();
            foreach (Simbolo simbolo in Clases.TablaSimbolo)
            {
                GenerarTablaSimbolos(simbolo, simbolo.Nombre);
            }
        }


        public void GenerarTablaSimbolos(Simbolo simbolo,  String NombreAmbito)
        {
            String nombre;
            switch (simbolo.Rol)
            {
                case Constante.TMetodo:
                    nombre = NombreAmbito + "_" + simbolo.Tipo + "_" + simbolo.Nombre;
                    break;

            }
        }


    }
}
