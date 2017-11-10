using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    static class FCasteo
    {
        public static String Casteo(String Destino, Nodo3D origen)
        {
            String resultado = Constante.TErrorSemantico;
            switch (Destino)
            {
                case Constante.TEntero:
                    switch (origen.Tipo)
                    {
                        
                    }
                    break;
            }
            return resultado;
        }
    }
}
