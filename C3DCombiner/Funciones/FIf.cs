using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FIf
    {
        public String Tipo,  Etiqueta;
        public FNodoExpresion Condicion;
        public Simbolo Padre;

        public FIf(String tipo, FNodoExpresion condicion, String etiqueta)
        {
            this.Tipo = tipo;
            this.Condicion = condicion;
            this.Etiqueta = etiqueta;
        }


        public String Ejecutar()
        {
            FNodoExpresion aux = Condicion.ResolverExpresion();

            if (!TitusTools.HayErrores())
            {
                if (aux.Tipo.Equals(Constante.TBooleano))
                {
                    if (this.Tipo.Equals(Constante.TIf))
                    {
                        if (aux.Bool)
                        {
                            return Etiqueta;
                        }
                    }
                    else
                    {
                        if (!aux.Bool)
                        {
                            return Etiqueta;
                        }
                    }
                }
            }

            return "";
        }

        public String Generar3DOptimizado()
        {
            String cadena = "";

            if (Tipo.Equals(Constante.TIf))
            {
                cadena += "\t\t" + "if " + Condicion.Generar3DOptimizado() + " goto " + Etiqueta + ";\n";
            }
            else
            {
                cadena += "\t\t" + "ifFalse " + Condicion.Generar3DOptimizado() + " goto " + Etiqueta + ";\n";
            }

            return cadena;
        }
    }
}
