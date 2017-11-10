using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FAsignacion
    {
        public Ambito Ambito;
        public String Tipo;
        public FLlamadaObjeto Nombre;
        public FNodoExpresion Operacion;
        public FNodoExpresion Valor;

        public Simbolo Padre;

        public FAsignacion(String tipo, Ambito ambito, FNodoExpresion valor, Object nombre)
        {
            this.Tipo = tipo;
            this.Ambito = ambito;
            this.Valor = valor;
            if (this.Tipo.Equals(Constante.TAumento) || this.Tipo.Equals(Constante.TDecremento))
            {
                this.Operacion = (FNodoExpresion)nombre;
            }
            else
            {
                this.Nombre = (FLlamadaObjeto)nombre;
            }
            this.Padre = null;
        }

        public String Generar3D()
        {
            String cadena = "";
            return cadena;
        }
    }
}
