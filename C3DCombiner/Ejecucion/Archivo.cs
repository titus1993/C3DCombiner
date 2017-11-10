using C3DCombiner.BD;
using C3DCombiner.Funciones;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        public Archivo(List<String> Imports, Ambito Clases, String Ruta)
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


        public void GenerarTablaSimbolos(Simbolo simbolo, String NombreAmbito)
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
                    //InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());
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
                    //InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());

                    foreach (Simbolo sim in si.Ambito.TablaSimbolo)
                    {
                        GenerarTablaSimbolos(sim, NombreAmbito + "_" + sim.Rol);
                    }

                    foreach (FSinoSi sinosi in si.SinoSi)
                    {
                        //InsertarTablaSimbolos(Constante.TSinoSi, "", NombreAmbito, "", sinosi.Ambito.Tamaño.ToString(), (-1).ToString());
                        foreach (Simbolo sim in sinosi.Ambito.TablaSimbolo)
                        {
                            GenerarTablaSimbolos(sim, NombreAmbito + "_" + Constante.TSinoSi);
                        }
                    }

                    if (si.Sino != null)
                    {
                        //InsertarTablaSimbolos(Constante.TSino, "", NombreAmbito, "", si.Sino.Ambito.Tamaño.ToString(), (-1).ToString());
                        foreach (Simbolo sim in simbolo.Ambito.TablaSimbolo)
                        {
                            GenerarTablaSimbolos(sim, NombreAmbito + "_" + Constante.TSino);
                        }
                    }
                    break;

                case Constante.TElegir:
                    FElegir elegir = (FElegir)simbolo.Valor;
                    //InsertarTablaSimbolos(Constante.TElegir, "", NombreAmbito, "", simbolo.Ambito.Tamaño.ToString(), simbolo.Posicion.ToString());
                    foreach (FCaso caso in elegir.Casos)
                    {
                        //InsertarTablaSimbolos(Constante.CASO, "", NombreAmbito + "_" + Constante.TElegir, "", caso.Ambito.Tamaño.ToString(), (-1).ToString());
                        foreach (Simbolo sim in caso.Ambito.TablaSimbolo)
                        {
                            GenerarTablaSimbolos(sim, NombreAmbito + "_" + Constante.TElegir + "_" + Constante.CASO);
                        }
                    }

                    if (elegir.Defecto != null)
                    {
                        //InsertarTablaSimbolos(Constante.DEFECTO, "", NombreAmbito + "_" + Constante.TElegir, "", elegir.Defecto.Ambito.Tamaño.ToString(), (-1).ToString());
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
                            //InsertarTablaSimbolos(simbolo.Rol, simbolo.Tipo, NombreAmbito, simbolo.Nombre, simbolo.Tamaño.ToString(), simbolo.Posicion.ToString());
                            foreach (Simbolo sim in simbolo.Ambito.TablaSimbolo)
                            {
                                GenerarTablaSimbolos(sim, NombreAmbito + "_" + simbolo.Rol);
                            }
                        }
                    }
                    break;
            }
        }


        public void CrearTablasSimbolos()
        {
            foreach (Archivo archivo in TitusTools.Archivos_Importados)
            {
                archivo.GenerarTablaSimbolos();
            }
        }

        public void Ejecutar()
        {
            CrearTablasSimbolos();
            TitusTools.Codigo3D.Text = Generar3D();
        }

        public void GenerarImports()
        {
            foreach (String ruta in Imports)
            {
                if (!TitusTools.ExisteArchivo(ruta))
                {
                    if (ruta.ToLower().Contains("http://"))
                    {
                        ObtenerCodigoArchivos(ruta, 0);
                    }
                    else if (ruta.ToLower().Contains("/"))
                    {
                        if (ruta.ToLower().Contains(".tree"))
                        {
                            ObtenerCodigoArchivos(ruta, 1);
                        }
                        else if (ruta.ToLower().Contains(".olc"))
                        {
                            ObtenerCodigoArchivos(ruta, 2);
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede importar la ruta " + ruta, this.Ruta, 1, 1);
                        }
                    }
                    else
                    {
                        if (ruta.ToLower().Contains(".tree"))
                        {
                            ObtenerCodigoArchivos(ruta, 3);
                        }
                        else if (ruta.ToLower().Contains(".olc"))
                        {
                            ObtenerCodigoArchivos(ruta, 4);
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede importar la ruta " + ruta, this.Ruta, 1, 1);
                        }
                    }
                }
            }
        }

        BaseDatos Base = new BaseDatos();

        public void ObtenerCodigoArchivos(String ruta, int tipo)//0. repo tree, 1. full tree, 2. full olc, 3. tree, 4. olc
        {
            LanguageData Language = null;
            Parser parser = null;
            String codigo = "";
            int Tipo = 0;
            switch (tipo)
            {
                case 0:
                    {
                        if (TitusTools.Usuario.Estado)
                        {
                            if (Base.ExisteRutaRepositorio(ruta))
                            {
                                Language = new LanguageData(new TreeGrammar());
                                parser = new Parser(Language);
                                codigo = Base.GetCodigoRepositorio(ruta);
                                Tipo = 1;
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede importar del repositorio " + ruta, this.Ruta, 1, 1);
                            }
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "Para importar archivos del repositorio debe iniciar sesion, ruta: " + ruta, this.Ruta, 1, 1);
                        }

                    }
                    break;

                case 1:
                    {
                        if (File.Exists(ruta))
                        {
                            Language = new LanguageData(new TreeGrammar());
                            parser = new Parser(Language);
                            codigo = File.ReadAllText(ruta);
                            Tipo = 1;
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede importar la ruta " + ruta, this.Ruta, 1, 1);
                        }
                    }
                    break;

                case 2:
                    {
                        if (File.Exists(ruta))
                        {
                            Language = new LanguageData(new OLCGrammar());
                            parser = new Parser(Language);
                            codigo = File.ReadAllText(ruta);
                            Tipo = 0;
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede importar la ruta " + ruta, this.Ruta, 1, 1);
                        }
                    }
                    break;

                case 3:
                    {
                        String nueva =  Path.GetDirectoryName(this.Ruta);
                        nueva += "\\" + ruta;
                        if (File.Exists(nueva))
                        {
                            Language = new LanguageData(new TreeGrammar());
                            parser = new Parser(Language);
                            codigo = File.ReadAllText(nueva);
                            Tipo = 1;
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede importar el archivo " + ruta, this.Ruta, 1, 1);
                        }
                    }
                    break;

                case 4:
                    {
                        String nueva = Path.GetDirectoryName(this.Ruta);
                        nueva += "\\" + ruta;
                        if (File.Exists(nueva))
                        {
                            Language = new LanguageData(new TreeGrammar());
                            parser = new Parser(Language);
                            codigo = File.ReadAllText(nueva);
                            Tipo = 0;
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede importar el archivo " + ruta, this.Ruta, 1, 1);
                        }
                    }
                    break;

            }

            if (parser != null)
            {
                ParseTree arbol = parser.Parse(codigo);

                if (arbol.Root != null && arbol.ParserMessages.Count == 0)
                {
                    ArbolSintactico Arbol = new ArbolSintactico(arbol.Root, Tipo, ruta);

                }
                else
                {
                    foreach (Irony.LogMessage error in arbol.ParserMessages)
                    {
                        if (error.Message.Contains("Syntax error,"))
                        {
                            TitusTools.InsertarError("Sintactico", error.Message.Replace("Syntax error", " "), this.Ruta, (error.Location.Line + 1), (error.Location.Column + 1));
                        }
                        else if (error.Message.Contains("Invalid character"))
                        {
                            TitusTools.InsertarError("Lexico", error.Message.Replace("Invalid character", "Caracter invalido"), Ruta, (error.Location.Line + 1), (error.Location.Column + 1));
                        }
                        else
                        {
                            TitusTools.InsertarError("Sintactico", error.Message.Replace("Unclosed cooment block", "Comentario de bloque sin cerrar"), Ruta, (error.Location.Line + 1), (error.Location.Column + 1));
                        }

                    }
                }
            }


        }

        public String Generar3D()
        {
            String cadena = "";
            foreach (Simbolo sim in Clases.TablaSimbolo)
            {
                cadena += sim.Generar3D();
            }
            return cadena;
        }




    }
}
