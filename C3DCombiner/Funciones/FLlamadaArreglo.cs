using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FLlamadaArreglo
    {
        public String Nombre;
        public int Fila, Columna;
        public List<FNodoExpresion> Dimensiones;

        public Simbolo Padre;

        public FLlamadaArreglo(String nombre, List<FNodoExpresion> dimensiones, int fila, int columna)
        {
            this.Nombre = nombre;
            this.Dimensiones = dimensiones;
            this.Fila = fila;
            this.Columna = columna;
            this.Padre = null;
        }

        public void setPadre(Simbolo simbolo)
        {
            this.Padre = simbolo;
            foreach (FNodoExpresion nodo in Dimensiones)
            {
                nodo.SetPadre(simbolo);
            }
        }

        //acceso a arreglos
        public Nodo3D Generar3D()
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = Padre.BuscarVariable(this.Nombre);


            if (sim != null)
            {

                if (sim.Rol.Equals(Constante.DECLARACION))
                {
                    FDeclaracion decla = (FDeclaracion)sim.Valor;

                    if (decla.Dimensiones.Count == this.Dimensiones.Count)
                    {
                        nodo.Tipo = sim.Tipo;

                        if (sim.Ambito.Nombre.Equals("§global§"))
                        {
                            String pos = TitusTools.GetTemp();
                            String heap = TitusTools.GetTemp();
                            nodo.Valor = TitusTools.GetTemp();
                            nodo.Codigo += "\t\t" + pos + " = Stack[P];\n";
                            nodo.Codigo += "\t\t" + heap + " = " + pos + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                            nodo.Codigo += "\t\t" + nodo.Valor + " = Heap[" + heap + "];\n";
                        }
                        else
                        {
                            String pos = TitusTools.GetTemp();
                            nodo.Valor = TitusTools.GetTemp();
                            nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                            nodo.Codigo += "\t\t" + nodo.Valor + " = Stack[" + pos + "];\n";
                        }

                        String tempAcceso = TitusTools.GetTemp();
                        String cadenaArreglo = "";
                        String etqError = TitusTools.GetEtq();
                        String etqSalida = TitusTools.GetEtq();
                        cadenaArreglo += "\t\t" + tempAcceso + " = Heap[" + nodo.Valor + "];//acceso a las dimensiones\n";
                        cadenaArreglo += "\t\t" + "ifFalse " + tempAcceso + " == " + this.Dimensiones.Count.ToString() + " goto " + etqError + ";\n";
                        cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";

                        String mapeo = TitusTools.GetTemp();
                        
                        String dsize = TitusTools.GetTemp();

                        String indice = TitusTools.GetTemp();

                        cadenaArreglo += "\t\t" + mapeo + " = 0;\n";
                        int i = 0;
                        foreach (FNodoExpresion d in Dimensiones)
                        {
                            Nodo3D dtemp = d.Generar3D();
                            if (dtemp.Tipo.Equals(Constante.TEntero) && !TitusTools.HayErrores())
                            {
                                cadenaArreglo += dtemp.Codigo;
                                cadenaArreglo += "\t\t" + dsize + " = Heap[" + nodo.Valor + "];\n";
                                cadenaArreglo += "\t\t" + indice + " = " + dsize + " - 1;\n";
                                cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";
                                cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " >= 0 goto " + etqError + ";\n";
                                cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " <= " + indice + " goto " + etqError + ";\n";
                                if (i > 0)
                                {
                                    cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " * " + dsize + ";\n";
                                }

                                cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " + " + dtemp.Valor + ";\n";
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "Solo se puede acceder al arreglo " + this.Nombre + " con un tipo entero no un tipo " + dtemp.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                            }

                            i++;
                        }

                        cadenaArreglo += "\t\t" + "goto " + etqSalida + ";\n";
                        cadenaArreglo += "\t" + etqError + ":\n";
                        cadenaArreglo += "\t\t" + "Error(1);\n";
                        cadenaArreglo += "\t" + etqSalida + ":\n";
                        cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + " + mapeo + ";//posicion lexicografica\n";
                        String val = TitusTools.GetTemp();
                        cadenaArreglo += "\t\t" + val + " = Heap[" + nodo.Valor + "];\n";

                        nodo.Valor = val;
                        nodo.Codigo += cadenaArreglo;

                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "El arreglo " + this.Nombre + ", tiene " + decla.Dimensiones.Count.ToString() + " dimensiones no " + this.Dimensiones.Count.ToString(), TitusTools.GetRuta(), this.Fila, this.Columna);
                    }
                }
                else//si es parametro no trae tamaño en sus dimensiones solo dimensiones
                {
                    FParametro decla = (FParametro)sim.Valor;

                    if (decla.Dimensiones == this.Dimensiones.Count)
                    {
                        nodo.Tipo = sim.Tipo;

                        String pos = TitusTools.GetTemp();
                        nodo.Valor = TitusTools.GetTemp();
                        nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                        nodo.Codigo += "\t\t" + nodo.Valor + " = Stack[" + pos + "];\n";

                        String tempAcceso = TitusTools.GetTemp();
                        String cadenaArreglo = "";
                        String etqError = TitusTools.GetEtq();
                        String etqSalida = TitusTools.GetEtq();
                        cadenaArreglo += "\t\t" + tempAcceso + " = Heap[" + nodo.Valor + "];//acceso a las dimensiones\n";
                        cadenaArreglo += "\t\t" + "ifFalse " + tempAcceso + " == " + this.Dimensiones.Count.ToString() + " goto " + etqError + ";\n";
                        cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";

                        String mapeo = TitusTools.GetTemp();

                        String dsize = TitusTools.GetTemp();

                        String indice = TitusTools.GetTemp();

                        cadenaArreglo += "\t\t" + mapeo + " = 0;\n";
                        int i = 0;
                        foreach (FNodoExpresion d in Dimensiones)
                        {
                            Nodo3D dtemp = d.Generar3D();
                            if (dtemp.Tipo.Equals(Constante.TEntero) && !TitusTools.HayErrores())
                            {
                                cadenaArreglo += dtemp.Codigo;
                                cadenaArreglo += "\t\t" + dsize + " = Heap[" + nodo.Valor + "];\n";
                                cadenaArreglo += "\t\t" + indice + " = " + dsize + " - 1;\n";
                                cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";
                                cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " >= 0 goto " + etqError + ";\n";
                                cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " <= " + indice + " goto " + etqError + ";\n";
                                if (i > 0)
                                {
                                    cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " * " + dsize + ";\n";
                                }

                                cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " + " + dtemp.Valor + ";\n";
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "Solo se puede acceder al arreglo " + this.Nombre + " con un tipo entero no un tipo " + dtemp.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                            }

                            i++;
                        }

                        cadenaArreglo += "\t\t" + "goto " + etqSalida + ";\n";
                        cadenaArreglo += "\t" + etqError + ":\n";
                        cadenaArreglo += "\t\t" + "Error(1);\n";
                        cadenaArreglo += "\t" + etqSalida + ":\n";
                        cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + " + mapeo + ";//posicion lexicografica\n";
                        String val = TitusTools.GetTemp();
                        cadenaArreglo += "\t\t" + val + " = Heap[" + nodo.Valor + "];\n";

                        nodo.Valor = val;
                        nodo.Codigo += cadenaArreglo;
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "El arreglo " + this.Nombre + ", tiene " + decla.Dimensiones.ToString() + " dimensiones no " + this.Dimensiones.Count.ToString(), TitusTools.GetRuta(), this.Fila, this.Columna);
                    }
                }

            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
            }

            return nodo;
        }


        public Nodo3D Generar3DHijo(Simbolo padre, String temporal)
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = padre.BuscarVariable(this.Nombre);


            if (sim != null)
            {
                if (sim.Visibilidad.Equals(Constante.TPublico))
                {
                    if (sim.Rol.Equals(Constante.DECLARACION))
                    {
                        FDeclaracion decla = (FDeclaracion)sim.Valor;

                        if (decla.Dimensiones.Count == this.Dimensiones.Count)
                        {
                            nodo.Tipo = sim.Tipo;

                            if (sim.Ambito.Nombre.Equals("§global§"))
                            {
                                String heap = TitusTools.GetTemp();
                                nodo.Valor = TitusTools.GetTemp();
                                nodo.Codigo += "\t\t" + heap + " = " + temporal + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                                nodo.Codigo += "\t\t" + nodo.Valor + " = Heap[" + heap + "];\n";
                            }

                            String tempAcceso = TitusTools.GetTemp();
                            String cadenaArreglo = "";
                            String etqError = TitusTools.GetEtq();
                            String etqSalida = TitusTools.GetEtq();
                            cadenaArreglo += "\t\t" + tempAcceso + " = Heap[" + nodo.Valor + "];//acceso a las dimensiones\n";
                            cadenaArreglo += "\t\t" + "ifFalse " + tempAcceso + " == " + this.Dimensiones.Count.ToString() + " goto " + etqError + ";\n";
                            cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";

                            String mapeo = TitusTools.GetTemp();

                            String dsize = TitusTools.GetTemp();

                            String indice = TitusTools.GetTemp();

                            cadenaArreglo += "\t\t" + mapeo + " = 0;\n";
                            int i = 0;
                            foreach (FNodoExpresion d in Dimensiones)
                            {
                                Nodo3D dtemp = d.Generar3D();
                                if (dtemp.Tipo.Equals(Constante.TEntero) && !TitusTools.HayErrores())
                                {
                                    cadenaArreglo += dtemp.Codigo;
                                    cadenaArreglo += "\t\t" + dsize + " = Heap[" + nodo.Valor + "];\n";
                                    cadenaArreglo += "\t\t" + indice + " = " + dsize + " - 1;\n";
                                    cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";
                                    cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " >= 0 goto " + etqError + ";\n";
                                    cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " <= " + indice + " goto " + etqError + ";\n";
                                    if (i > 0)
                                    {
                                        cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " * " + dsize + ";\n";
                                    }

                                    cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " + " + dtemp.Valor + ";\n";
                                }
                                else
                                {
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "Solo se puede acceder al arreglo " + this.Nombre + " con un tipo entero no un tipo " + dtemp.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                }

                                i++;
                            }

                            cadenaArreglo += "\t\t" + "goto " + etqSalida + ";\n";
                            cadenaArreglo += "\t" + etqError + ":\n";
                            cadenaArreglo += "\t\t" + "Error(1);\n";
                            cadenaArreglo += "\t" + etqSalida + ":\n";
                            cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + " + mapeo + ";//posicion lexicografica\n";
                            String val = TitusTools.GetTemp();
                            cadenaArreglo += "\t\t" + val + " = Heap[" + nodo.Valor + "];\n";

                            nodo.Valor = val;
                            nodo.Codigo += cadenaArreglo;

                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "El arreglo " + this.Nombre + ", tiene " + decla.Dimensiones.Count.ToString() + " dimensiones no " + this.Dimensiones.Count.ToString(), TitusTools.GetRuta(), this.Fila, this.Columna);
                        }
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder a una variable que no es publica " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
                }                
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
            }

            return nodo;
        }

        //asignacion
        public Nodo3D Generar3DAsignacion()
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = Padre.BuscarVariable(this.Nombre);


            if (sim != null)
            {

                if (sim.Rol.Equals(Constante.DECLARACION))
                {
                    FDeclaracion decla = (FDeclaracion)sim.Valor;

                    if (decla.Dimensiones.Count == this.Dimensiones.Count)
                    {
                        nodo.Tipo = sim.Tipo;

                        if (sim.Ambito.Nombre.Equals("§global§"))
                        {
                            String pos = TitusTools.GetTemp();
                            String heap = TitusTools.GetTemp();
                            nodo.Valor = TitusTools.GetTemp();
                            nodo.Codigo += "\t\t" + pos + " = Stack[P];\n";
                            nodo.Codigo += "\t\t" + heap + " = " + pos + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                            nodo.Codigo += "\t\t" + nodo.Valor + " = Heap[" + heap + "];\n";
                        }
                        else
                        {
                            String pos = TitusTools.GetTemp();
                            nodo.Valor = TitusTools.GetTemp();
                            nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                            nodo.Codigo += "\t\t" + nodo.Valor + " = Stack[" + pos + "];\n";
                        }

                        String tempAcceso = TitusTools.GetTemp();
                        String cadenaArreglo = "";
                        String etqError = TitusTools.GetEtq();
                        String etqSalida = TitusTools.GetEtq();
                        cadenaArreglo += "\t\t" + tempAcceso + " = Heap[" + nodo.Valor + "];//acceso a las dimensiones\n";
                        cadenaArreglo += "\t\t" + "ifFalse " + tempAcceso + " == " + this.Dimensiones.Count.ToString() + " goto " + etqError + ";\n";
                        cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";

                        String mapeo = TitusTools.GetTemp();

                        String dsize = TitusTools.GetTemp();

                        String indice = TitusTools.GetTemp();

                        cadenaArreglo += "\t\t" + mapeo + " = 0;\n";
                        int i = 0;
                        foreach (FNodoExpresion d in Dimensiones)
                        {
                            Nodo3D dtemp = d.Generar3D();
                            if (dtemp.Tipo.Equals(Constante.TEntero) && !TitusTools.HayErrores())
                            {
                                cadenaArreglo += dtemp.Codigo;
                                cadenaArreglo += "\t\t" + dsize + " = Heap[" + nodo.Valor + "];\n";
                                cadenaArreglo += "\t\t" + indice + " = " + dsize + " - 1;\n";
                                cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";
                                cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " >= 0 goto " + etqError + ";\n";
                                cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " <= " + indice + " goto " + etqError + ";\n";
                                if (i > 0)
                                {
                                    cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " * " + dsize + ";\n";
                                }

                                cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " + " + dtemp.Valor + ";\n";
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "Solo se puede acceder al arreglo " + this.Nombre + " con un tipo entero no un tipo " + dtemp.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                            }

                            i++;
                        }

                        cadenaArreglo += "\t\t" + "goto " + etqSalida + ";\n";
                        cadenaArreglo += "\t" + etqError + ":\n";
                        cadenaArreglo += "\t\t" + "Error(1);\n";
                        cadenaArreglo += "\t" + etqSalida + ":\n";
                        cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + " + mapeo + ";//posicion lexicografica\n";
                       
                        nodo.Valor = "Heap[" + nodo.Valor + "]";
                        
                        nodo.Codigo += cadenaArreglo;

                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "El arreglo " + this.Nombre + ", tiene " + decla.Dimensiones.Count.ToString() + " dimensiones no " + this.Dimensiones.Count.ToString(), TitusTools.GetRuta(), this.Fila, this.Columna);
                    }
                }
                else//si es parametro no trae tamaño en sus dimensiones solo dimensiones
                {
                    FParametro decla = (FParametro)sim.Valor;

                    if (decla.Dimensiones == this.Dimensiones.Count)
                    {
                        nodo.Tipo = sim.Tipo;

                        String pos = TitusTools.GetTemp();
                        nodo.Valor = TitusTools.GetTemp();
                        nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                        nodo.Codigo += "\t\t" + nodo.Valor + " = Stack[" + pos + "];\n";

                        String tempAcceso = TitusTools.GetTemp();
                        String cadenaArreglo = "";
                        String etqError = TitusTools.GetEtq();
                        String etqSalida = TitusTools.GetEtq();
                        cadenaArreglo += "\t\t" + tempAcceso + " = Heap[" + nodo.Valor + "];//acceso a las dimensiones\n";
                        cadenaArreglo += "\t\t" + "ifFalse " + tempAcceso + " == " + this.Dimensiones.Count.ToString() + " goto " + etqError + ";\n";
                        cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";

                        String mapeo = TitusTools.GetTemp();

                        String dsize = TitusTools.GetTemp();

                        String indice = TitusTools.GetTemp();

                        cadenaArreglo += "\t\t" + mapeo + " = 0;\n";
                        int i = 0;
                        foreach (FNodoExpresion d in Dimensiones)
                        {
                            Nodo3D dtemp = d.Generar3D();
                            if (dtemp.Tipo.Equals(Constante.TEntero) && !TitusTools.HayErrores())
                            {
                                cadenaArreglo += dtemp.Codigo;
                                cadenaArreglo += "\t\t" + dsize + " = Heap[" + nodo.Valor + "];\n";
                                cadenaArreglo += "\t\t" + indice + " = " + dsize + " - 1;\n";
                                cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";
                                cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " >= 0 goto " + etqError + ";\n";
                                cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " <= " + indice + " goto " + etqError + ";\n";
                                if (i > 0)
                                {
                                    cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " * " + dsize + ";\n";
                                }

                                cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " + " + dtemp.Valor + ";\n";
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "Solo se puede acceder al arreglo " + this.Nombre + " con un tipo entero no un tipo " + dtemp.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                            }

                            i++;
                        }

                        cadenaArreglo += "\t\t" + "goto " + etqSalida + ";\n";
                        cadenaArreglo += "\t" + etqError + ":\n";
                        cadenaArreglo += "\t\t" + "Error(1);\n";
                        cadenaArreglo += "\t" + etqSalida + ":\n";
                        cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + " + mapeo + ";//posicion lexicografica\n";
                        
                        nodo.Valor = "Heap[" + nodo.Valor + "]";
                        
                        nodo.Codigo += cadenaArreglo;
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "El arreglo " + this.Nombre + ", tiene " + decla.Dimensiones.ToString() + " dimensiones no " + this.Dimensiones.Count.ToString(), TitusTools.GetRuta(), this.Fila, this.Columna);
                    }
                }

            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
            }

            return nodo;
        }


        public Nodo3D Generar3DHijoAsignacion(Simbolo padre, String temporal)
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = padre.BuscarVariable(this.Nombre);


            if (sim != null)
            {
                if (sim.Visibilidad.Equals(Constante.TPublico))
                {
                    if (sim.Rol.Equals(Constante.DECLARACION))
                    {
                        FDeclaracion decla = (FDeclaracion)sim.Valor;

                        if (decla.Dimensiones.Count == this.Dimensiones.Count)
                        {
                            nodo.Tipo = sim.Tipo;

                            if (sim.Ambito.Nombre.Equals("§global§"))
                            {
                                String heap = TitusTools.GetTemp();
                                nodo.Valor = TitusTools.GetTemp();
                                nodo.Codigo += "\t\t" + heap + " = " + temporal + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                                nodo.Codigo += "\t\t" + nodo.Valor + " = Heap[" + heap + "];\n";
                            }

                            String tempAcceso = TitusTools.GetTemp();
                            String cadenaArreglo = "";
                            String etqError = TitusTools.GetEtq();
                            String etqSalida = TitusTools.GetEtq();
                            cadenaArreglo += "\t\t" + tempAcceso + " = Heap[" + nodo.Valor + "];//acceso a las dimensiones\n";
                            cadenaArreglo += "\t\t" + "ifFalse " + tempAcceso + " == " + this.Dimensiones.Count.ToString() + " goto " + etqError + ";\n";
                            cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";

                            String mapeo = TitusTools.GetTemp();

                            String dsize = TitusTools.GetTemp();

                            String indice = TitusTools.GetTemp();

                            cadenaArreglo += "\t\t" + mapeo + " = 0;\n";
                            int i = 0;
                            foreach (FNodoExpresion d in Dimensiones)
                            {
                                Nodo3D dtemp = d.Generar3D();
                                if (dtemp.Tipo.Equals(Constante.TEntero) && !TitusTools.HayErrores())
                                {
                                    cadenaArreglo += dtemp.Codigo;
                                    cadenaArreglo += "\t\t" + dsize + " = Heap[" + nodo.Valor + "];\n";
                                    cadenaArreglo += "\t\t" + indice + " = " + dsize + " - 1;\n";
                                    cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + 1;\n";
                                    cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " >= 0 goto " + etqError + ";\n";
                                    cadenaArreglo += "\t\t" + "ifFalse " + dtemp.Valor + " <= " + indice + " goto " + etqError + ";\n";
                                    if (i > 0)
                                    {
                                        cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " * " + dsize + ";\n";
                                    }

                                    cadenaArreglo += "\t\t" + mapeo + " = " + mapeo + " + " + dtemp.Valor + ";\n";
                                }
                                else
                                {
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "Solo se puede acceder al arreglo " + this.Nombre + " con un tipo entero no un tipo " + dtemp.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                }

                                i++;
                            }

                            cadenaArreglo += "\t\t" + "goto " + etqSalida + ";\n";
                            cadenaArreglo += "\t" + etqError + ":\n";
                            cadenaArreglo += "\t\t" + "Error(1);\n";
                            cadenaArreglo += "\t" + etqSalida + ":\n";
                            cadenaArreglo += "\t\t" + nodo.Valor + " = " + nodo.Valor + " + " + mapeo + ";//posicion lexicografica\n";
                            
                            nodo.Valor = "Heap[" + nodo.Valor + "]";
                            
                            nodo.Codigo += cadenaArreglo;

                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "El arreglo " + this.Nombre + ", tiene " + decla.Dimensiones.Count.ToString() + " dimensiones no " + this.Dimensiones.Count.ToString(), TitusTools.GetRuta(), this.Fila, this.Columna);
                        }
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder a una variable que no es publica " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
                }
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
            }

            return nodo;
        }
    }
}
