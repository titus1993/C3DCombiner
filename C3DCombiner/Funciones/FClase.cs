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
        public Archivo ArchivoPadre;

        public Simbolo Padre;

        public FClase(String Nombre, String Herencia, Ambito Ambito)
        {
            this.Nombre = Nombre;
            this.Herencia = Herencia;
            this.Ambito = Ambito;
            this.Padre = null;
        }

        public String Generar3DconMain()
        {
            String cadena = "";

            foreach (Simbolo sim in Ambito.TablaSimbolo)
            {
                if (!sim.Rol.Equals(Constante.DECLARACION))
                {
                    cadena += sim.Generar3DConMain();
                }
            }

            cadena += "void " + this.Nombre +"_constructor(){\n}\n\n";

            String init = Generar3DInit();
            init += cadena;
            return init;
        }

        public String Generar3D()
        {
            String cadena = "";

            foreach (Simbolo sim in Ambito.TablaSimbolo)
            {
                if (!sim.Rol.Equals(Constante.DECLARACION))
                {
                    cadena += sim.Generar3D();
                }
            }

            cadena += "void " + this.Nombre + "_constructor(){\n}\n\n";

            String init = Generar3DInit();
            init += cadena;
            return init;
        }

        public String Generar3DInit()
        {

            String temp = TitusTools.GetTemp();
            String init = "void init_" + this.Nombre + "(){\n";
            init += "\t\t" + temp + " = H;\n";
            int sizeherencia = Ambito.Tamaño;

            String init3 = "";

            foreach (Simbolo sim in Ambito.TablaSimbolo)
            {
                if (sim.Rol.Equals(Constante.DECLARACION))
                {
                    FDeclaracion decla = (FDeclaracion)sim.Valor;
                    init3 += decla.Generar3DInit(temp, 0);
                }
            }

            //buscamos los init de los hereda

            if (!Herencia.Equals(""))
            {
                
                String herencia = Herencia;
                while (herencia != "")
                {
                    Simbolo clase = Padre.BuscarClase(herencia, ArchivoPadre);
                    if (clase != null)
                    {
                        FClase nuevaclase = (FClase)clase.Valor;
                        foreach (Simbolo sim in nuevaclase.Ambito.TablaSimbolo)
                        {
                            if (sim.Rol.Equals(Constante.DECLARACION))
                            {
                                FDeclaracion decla = (FDeclaracion)sim.Valor;
                                init3 += decla.Generar3DInit(temp, sizeherencia);
                            }
                        }
                        sizeherencia += nuevaclase.Ambito.Tamaño;
                        herencia = nuevaclase.Herencia;
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro la clase para heredar " + herencia, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
                        break;
                    }
                }
            }

            String init2 = "\t\t" + "H = H + " + sizeherencia.ToString() + ";\n";

            init += init2 + init3;

            init += "}\n\n";
            return init;
        }
    }
}
