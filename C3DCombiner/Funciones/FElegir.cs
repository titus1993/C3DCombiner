using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FElegir
    {
        public FNodoExpresion Condicion;
        public Ambito Ambito;
        public List<FCaso> Casos;
        public FCaso Defecto;

        public Simbolo Padre;

        public FElegir(FNodoExpresion condicion, Ambito ambito,  List<FCaso> casos, FCaso defecto)
        {
            this.Casos = casos;
            this.Defecto = defecto;
            this.Ambito = ambito;
            this.Condicion = condicion;

            FNodoExpresion aux = null;
            foreach (FCaso caso in Casos)
            {
                if (aux != null)
                {
                    if (aux.Tipo != caso.Valor.Tipo)
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "Los valores de los casos de la sentencia elegir deben ser iguales", TitusTools.GetRuta(), caso.Valor.Fila, caso.Valor.Columna);
                    }
                }
                else
                {
                    aux = caso.Valor;
                }
            }
        }


        public String Generar3D()
        {
            String cadena = "\t\t//Comienza elegir\n";

            String salida = TitusTools.GetEtq();


            Nodo3D cond = Condicion.Generar3D();

            cadena += cond.Codigo;

            foreach (FCaso caso in Casos)
            {
                cadena += caso.Generar3D(cond);
            }

            if (Defecto != null)
            {
                cadena += Defecto.Generar3D();
            }

            cadena += "\t" + salida + "://termina elegir\n";

            return cadena;
        }

        
    }
}
