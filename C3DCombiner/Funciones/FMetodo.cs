using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FMetodo
    {
        public Ambito Ambito;
        public List<Simbolo> Parametros;
        public int Dimensiones;
        public int Fila, Columna;
        public String Tipo, Nombre;
        public String Visibilidad;

        public Simbolo Padre;

        public FMetodo(String visibilidad, String tipo, String nombre, List<Simbolo> parametro, Ambito ambito, int fila, int columna)
        {
            this.Visibilidad = visibilidad;
            this.Ambito = ambito;
            this.Parametros = parametro;
            this.Fila = fila;
            this.Columna = columna;
            this.Tipo = tipo;
            this.Nombre = nombre;
        }

        public FMetodo(String visibilidad, String tipo, int dimensiones, String nombre, List<Simbolo> parametro, Ambito ambito, int fila, int columna)
        {
            this.Visibilidad = visibilidad;
            this.Ambito = ambito;
            this.Parametros = parametro;
            this.Dimensiones = dimensiones;
            this.Fila = fila;
            this.Columna = columna;
            this.Tipo = tipo;
            this.Nombre = nombre;
        }


        public String Generar3D()
        {
            String cadena = "";

            cadena += "void " + GetNombre3D() + "(){\n";

            foreach (Simbolo simbolo in Ambito.TablaSimbolo)
            {
                cadena += simbolo.Generar3D();
            }

            cadena += "}\n\n";
            return cadena;
        }

        public String GetNombre3D()
        {
            String cadena = "";

            cadena += this.Padre.Padre.Nombre + "_" + this.Nombre + "_" + this.Tipo;

            foreach (Simbolo s in Parametros)
            {
                FParametro d = (FParametro)s.Valor;
                cadena += "_" + s.Tipo + d.Dimensiones.ToString();
            }

            return cadena;
        }


        /***************************** ejecucion de 3d *****************************/

        public void Ejecutar()
        {
            Tabla3D.InsertarAmbito(this.Ambito);
            if (!TitusTools.HayErrores())
            {
                for (int i = 0; i < this.Ambito.TablaSimbolo.Count && i >= 0; i++)
                {
                    Simbolo sim = Ambito.TablaSimbolo.ElementAt(i);
                    if (!TitusTools.HayErrores())
                    {
                        switch (sim.Rol)
                        {
                            case Constante.LLAMADA_METODO:
                                EjecutarLlamadaMetodo(sim);
                                break;

                            case Constante.TGoto:
                                {
                                    int pos = EjecutarGoTo(sim);
                                    if (pos != 0)
                                    {
                                        i += pos - 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                break;

                            case Constante.TIf:
                                {

                                    int pos = EjecutarIf(sim);
                                    if(pos != 0)
                                    {
                                        i += pos - 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                break;

                            case Constante.ASIGNACION:
                                EjecutarAsignacion(sim);
                                break;

                            case Constante.TPrint:
                                EjecutarPrint(sim);
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Tabla3D.SacarAmbito();
        }

        public void EjecutarLlamadaMetodo(Simbolo sim)
        {
            FMetodo metodo = Tabla3D.BuscarMetodo(sim.Nombre);

            if (metodo != null)
            {
                metodo.Ejecutar();
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro el metodo " + sim.Nombre, TitusTools.GetRuta(), sim.Fila, sim.Columna);
            }
        }

        public int EjecutarGoTo(Simbolo sim)
        {
            Boolean estado = false;
            int i = -1;
            Simbolo anterior = sim.Anterior;
            while(anterior != null)
            {
                if (anterior.Rol.Equals(Constante.Etiqueta) && anterior.Nombre.Equals(sim.Nombre))
                {
                    estado = true;

                    break;
                }
                i--;
                anterior = anterior.Anterior;
            }


            if (!estado)
            {
                i = 1;
                Simbolo siguiente = sim.Siguiente;
                while (siguiente != null)
                {
                    if (siguiente.Rol.Equals(Constante.Etiqueta) && siguiente.Nombre.Equals(sim.Nombre))
                    {
                        estado = true;
                        break;
                    }
                    i++;
                    siguiente = siguiente.Siguiente;
                }
            }


            if (estado)
            {
                return i;
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se econtro la etiqueta " + sim.Nombre, TitusTools.GetRuta(), sim.Fila, sim.Columna);
                return 0;
            }
        }

        

        public int EjecutarIf(Simbolo sim)
        {
            FIf si = (FIf)sim.Valor;

            String etq = si.Ejecutar();

            if (!etq.Equals(""))
            {
                Simbolo nuevo = new Simbolo("", Constante.TGoto, etq, Constante.TGoto, 0, 0, new Ambito(etq), null);
                nuevo.Anterior = sim.Anterior;
                nuevo.Siguiente = sim.Siguiente;
                return EjecutarGoTo(nuevo);
            }

            return 1;
        }

        public void EjecutarAsignacion(Simbolo sim)
        {
            FAsignacion3D asig = (FAsignacion3D)sim.Valor;
            asig.Ejecutar();
        }

        public void EjecutarPrint(Simbolo sim)
        {
            FPrint print = (FPrint)sim.Valor;
            print.Ejecutar();
        }
    }
}
