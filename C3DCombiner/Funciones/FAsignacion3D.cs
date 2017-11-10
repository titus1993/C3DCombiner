using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FAsignacion3D
    {
        public String Tipo;
        public FNodoExpresion Acceso;
        public FNodoExpresion Valor;
        public String Temporal;

        public FAsignacion3D(String tipo, FNodoExpresion acceso, FNodoExpresion valor, String tempo)
        {
            this.Tipo = tipo;
            this.Acceso = acceso;
            this.Valor = valor;
            this.Temporal = tempo;
        }


        public void Ejecutar()
        {
            switch (Tipo)
            {                

                case Constante.TH:
                    AsignarH();
                    break;

                case Constante.TP:
                    AsignarP();
                    break;

                case Constante.TStack:
                    AsignarStack();
                    break;

                case Constante.THeap:
                    AsignarHeap();
                    break;

                default:
                    AsignarTemporal();
                    break;
            }
        }

        public void AsignarTemporal()
        {
            Variable variable = Tabla3D.BuscarVariable(Temporal);

            if (variable != null)
            {
                FNodoExpresion valor = this.Valor.ResolverExpresion();
                if (!TitusTools.HayErrores())
                {
                    if (valor.Tipo.Equals(Constante.TEntero) || valor.Tipo.Equals(Constante.TDecimal))
                    {
                        variable.Valor = valor;
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "Se esperaba un valor numerico." , TitusTools.GetRuta(), this.Valor.Fila, this.Valor.Columna);
                    }
                }
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontroo el temporal " + this.Acceso.Nombre, TitusTools.GetRuta(), this.Acceso.Fila, this.Acceso.Columna);
            }
        }

        public void AsignarH()
        {
            FNodoExpresion valor = this.Valor.ResolverExpresion();
            if (!TitusTools.HayErrores())
            {
                if (valor.Tipo.Equals(Constante.TEntero))
                {
                     Tabla3D.H = valor.Entero;
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "Se esperaba un valor numerico.", TitusTools.GetRuta(), this.Valor.Fila, this.Valor.Columna);
                }
            }
        }

        public void AsignarP()
        {
            FNodoExpresion valor = this.Valor.ResolverExpresion();
            if (!TitusTools.HayErrores())
            {
                if (valor.Tipo.Equals(Constante.TEntero))
                {
                    Tabla3D.P = valor.Entero;
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "Se esperaba un valor numerico.", TitusTools.GetRuta(), this.Valor.Fila, this.Valor.Columna);
                }
            }
        }

        public void AsignarHeap()
        {
            FNodoExpresion acceso = this.Acceso.ResolverExpresion();
            FNodoExpresion valor = this.Valor.ResolverExpresion();

            if (!TitusTools.HayErrores())
            {
                if (acceso.Tipo.Equals(Constante.TEntero))
                {
                    Tabla3D.InsertarHeap(acceso.Entero, valor);
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "Se esperaba un valor entero para acceder al heap.", TitusTools.GetRuta(), this.Acceso.Fila, this.Acceso.Columna);
                }
            }
        }

        public void AsignarStack()
        {
            FNodoExpresion acceso = this.Acceso.ResolverExpresion();
            FNodoExpresion valor = this.Valor.ResolverExpresion();

            if (!TitusTools.HayErrores())
            {
                if (acceso.Tipo.Equals(Constante.TEntero))
                {
                    Tabla3D.InsertarStack(acceso.Entero, valor);
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "Se esperaba un valor entero para acceder al heap.", TitusTools.GetRuta(), this.Acceso.Fila, this.Acceso.Columna);
                }
            }
        }
    }
}
