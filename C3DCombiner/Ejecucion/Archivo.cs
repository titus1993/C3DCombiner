using C3DCombiner.Funciones;
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
            TablaSimbolos.Columns.Add("Nombre");
            TablaSimbolos.Columns.Add("Ambito");
            TablaSimbolos.Columns.Add("Tamaño");
            TablaSimbolos.Columns.Add("Posicion");
        }

        public void InsertarTablaSimbolos(String rol, String tipo, String ambito, String nombre, String tamanio, String posicion)
        {
            TablaSimbolos.Rows.Add(rol, tipo, nombre, ambito, tamanio, posicion);
        }

        public void GenerarTablaSimbolos()
        {
            IniciarTablaSimbolos();
            foreach (Simbolo simbolo in Clases.TablaSimbolo)
            {
                GenerarTablaSimbolos(simbolo, "");
            }
            TitusTools.InsertarTablaSimbolos(this.TablaSimbolos);
        }


        public void GenerarTablaSimbolos(Simbolo simbolo,  String NombreAmbito)
        {
            switch (simbolo.Rol)
            {
                case Constante.TClase:
                    InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());
                    foreach (Simbolo sim in simbolo.Ambito.TablaSimbolo)
                    {                        
                        GenerarTablaSimbolos(sim, simbolo.Nombre);
                    }
                    break;

                case Constante.TMetodo:

                    InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());

                    foreach (Simbolo sim in ((FMetodo)simbolo.Valor).Parametros)
                    {
                        InsertarTablaSimbolos(sim.Rol, sim.Tipo, NombreAmbito + "_" + simbolo.Nombre, sim.Nombre, sim.Tamaño.ToString(), sim.Posicion.ToString());
                    }

                    foreach (Simbolo sim in simbolo.Ambito.TablaSimbolo)
                    {
                        GenerarTablaSimbolos(sim, NombreAmbito + "_" + simbolo.Nombre);
                    }                    

                    break;

                case Constante.TConstructor:

                    InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());

                    foreach (Simbolo sim in ((FMetodo)simbolo.Valor).Parametros)
                    {
                        InsertarTablaSimbolos(sim.Rol, sim.Tipo, NombreAmbito + "_" + simbolo.Rol, sim.Nombre, sim.Tamaño.ToString(), sim.Posicion.ToString());
                    }

                    foreach (Simbolo sim in simbolo.Ambito.TablaSimbolo)
                    {
                        GenerarTablaSimbolos(sim, NombreAmbito + "_" + simbolo.Rol);
                    }

                    break;

                case Constante.TPara:
                    FPara para = (FPara)simbolo.Valor;
                    InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());
                    if (para.AccionAnterior.Rol.Equals(Constante.DECLARACION))
                    {
                        GenerarTablaSimbolos(para.AccionAnterior, NombreAmbito + "_" + simbolo.Rol);
                    }

                    foreach (Simbolo sim in simbolo.Ambito.TablaSimbolo)
                    {
                        GenerarTablaSimbolos(sim, NombreAmbito + "_" + sim.Rol);
                    }
                    break;

                case Constante.TSi:
                    FSi si = (FSi)simbolo.Valor;
                    InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());

                    foreach (Simbolo sim in si.Ambito.TablaSimbolo)
                    {
                        GenerarTablaSimbolos(sim, NombreAmbito + "_" + sim.Rol);
                    }

                    foreach (FSinoSi sinosi in si.SinoSi)
                    {
                        InsertarTablaSimbolos(Constante.TSinoSi, "", NombreAmbito, "", sinosi.Ambito.Tamaño.ToString(), (-1).ToString());
                        foreach (Simbolo sim in sinosi.Ambito.TablaSimbolo)
                        {
                            GenerarTablaSimbolos(sim, NombreAmbito + "_" + Constante.TSinoSi);
                        }
                    }

                    if (si.Sino != null)
                    {
                        InsertarTablaSimbolos(Constante.TSino, "", NombreAmbito, "", si.Sino.Ambito.Tamaño.ToString(), (-1).ToString());
                        foreach (Simbolo sim in simbolo.Ambito.TablaSimbolo)
                        {
                            GenerarTablaSimbolos(sim, NombreAmbito + "_" + Constante.TSino);
                        }
                    }                    
                    break;

                case Constante.TElegir:
                    FElegir elegir = (FElegir)simbolo.Valor;
                    InsertarTablaSimbolos(Constante.TElegir, "", NombreAmbito, "", simbolo.Ambito.Tamaño.ToString(), simbolo.Posicion.ToString());
                    foreach (FCaso caso in elegir.Casos)
                    {
                        InsertarTablaSimbolos(Constante.CASO, "", NombreAmbito + "_" + Constante.TElegir, "", caso.Ambito.Tamaño.ToString(), (-1).ToString());
                        foreach (Simbolo sim in caso.Ambito.TablaSimbolo)
                        {
                            GenerarTablaSimbolos(sim, NombreAmbito + "_" + Constante.TElegir + "_" + Constante.CASO);
                        }
                    }

                    if (elegir.Defecto != null)
                    {
                        InsertarTablaSimbolos(Constante.DEFECTO, "", NombreAmbito + "_" + Constante.TElegir, "", elegir.Defecto.Ambito.Tamaño.ToString(), (-1).ToString());
                        foreach (Simbolo sim in elegir.Defecto.Ambito.TablaSimbolo)
                        {
                            GenerarTablaSimbolos(sim, NombreAmbito + "_" + Constante.TElegir + "_" + Constante.DEFECTO);
                        }
                    }
                    break;

                default:
                    if (simbolo.Rol.Equals(Constante.DECLARACION))
                    {
                        InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());
                    }
                    else
                    {
                        if (simbolo.Tamaño > 0)
                        {
                            InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());
                            foreach (Simbolo sim in simbolo.Ambito.TablaSimbolo)
                            {
                                GenerarTablaSimbolos(sim, NombreAmbito + "_" + simbolo.Rol);
                            }
                        }
                    }
                    break;
            }
        }


        public void Ejecutar()
        {
            GenerarTablaSimbolos();
            TitusTools.Codigo3D.Text = Generar3D();
        }

        public String Generar3D()
        {
            String cadena = "";
            foreach (Simbolo sim in Clases.TablaSimbolo)
            {
                cadena += Generar3D(sim);
            }
            return cadena;
        }


        private String Generar3D(Simbolo simbolo)
        {
            String cadena = "";
            switch (simbolo.Rol)
            {
                case Constante.TClase:
                    {
                        foreach (Simbolo sim in simbolo.Ambito.TablaSimbolo)
                        {
                            cadena += Generar3D(sim);
                        }
                        break;
                    }
                case Constante.DECLARACION:
                    {
                        FDeclaracion declaracion = (FDeclaracion)simbolo.Valor;
                        cadena += declaracion.Generar3D();                        
                    }
                    break;
            }
            


            return cadena;
        }

    }
}
