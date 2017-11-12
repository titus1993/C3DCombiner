using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FLlamadaObjeto
    {
        public FLlamadaObjeto Hijo;
        public String Tipo, Nombre;
        public int Fila, Columna;
        public FLlamadaMetodo LlamadaMetodo;
        public FLlamadaArreglo LlamadaArreglo;
        public FLlamadaMetodoArrelgoTree LlamadaMetodoArreglo;

        public Simbolo Padre;

        public FLlamadaObjeto(String Tipo, String nombre, int fila, int columna, Object valor)
        {
            this.Tipo = Tipo;
            this.Nombre = nombre;
            this.Fila = fila;
            this.Columna = columna;
            this.LlamadaMetodo = null;
            this.LlamadaArreglo = null;
            this.Padre = null;


            if (Tipo.Equals(Constante.LLAMADA_ARREGLO))
            {
                this.LlamadaArreglo = (FLlamadaArreglo)valor;
            }
            else if (Tipo.Equals(Constante.LLAMADA_METODO))
            {
                this.LlamadaMetodo = (FLlamadaMetodo)valor;
            }
            else if (Tipo.Equals(Constante.LLAMADA_METODO_ARREGLO))
            {
                this.LlamadaMetodoArreglo = (FLlamadaMetodoArrelgoTree)valor;
            }

            this.Hijo = null;
        }

        public void InsertarHijo(FLlamadaObjeto hijo)
        {
            if (this.Hijo == null)
            {
                this.Hijo = hijo;
            }
            else
            {
                this.Hijo.InsertarHijo(hijo);
            }
        }


        public void SetPadre(Simbolo simbolo)
        {
            this.Padre = simbolo;
            switch (this.Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    this.LlamadaArreglo.setPadre(simbolo);
                    break;

                case Constante.LLAMADA_METODO:
                    this.LlamadaMetodo.setPadre(simbolo);
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    this.LlamadaMetodoArreglo.setPadre(simbolo);
                    break;

            }

            if (this.Hijo != null)
            {
                this.Hijo.SetPadre(simbolo);
            }
        }


        public Nodo3D Generar3D()
        {
            Nodo3D cadena = new Nodo3D();

            switch (Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    cadena = Generar3DArreglo();
                    break;


                case Constante.LLAMADA_METODO:
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    break;

                case Constante.TSelf:
                    break;

                case Constante.TSuper:
                    break;

                default:
                    cadena = Generar3DVariable();
                    break;
            }

            return cadena;
        }

        public Nodo3D Generar3DHijo(Simbolo padre, String temporal)
        {
            Nodo3D cadena = new Nodo3D();

            switch (Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    cadena = Generar3DArregloHijo(padre, temporal);
                    break;


                case Constante.LLAMADA_METODO:
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    break;

                case Constante.TSelf:
                    break;

                case Constante.TSuper:
                    break;

                default:
                    cadena = Generar3DVariableHijo(padre, temporal);
                    break;
            }

            return cadena;
        }


        public Nodo3D Generar3DArreglo()
        {
            Nodo3D nodo = new Nodo3D();
            nodo = LlamadaArreglo.Generar3D();

            if (Hijo != null)
            {
                if (!nodo.Tipo.Equals(Constante.TEntero) && !nodo.Tipo.Equals(Constante.TDecimal) && !nodo.Tipo.Equals(Constante.TCaracter) && !nodo.Tipo.Equals(Constante.TCadena) && !nodo.Tipo.Equals(Constante.TBooleano))
                {
                    //nodo.Tipo = sim.Tipo;//el simbolo trae el tipo de la variable 
                    Simbolo papa = Padre.BuscarClasePadre();//obtenemos quien es la clase padre para obtener a que archivo pertenece
                    FClase clasepapa = (FClase)papa.Valor;//castemos la clase
                    Simbolo padre = papa.BuscarClase(nodo.Tipo, clasepapa.ArchivoPadre);//buscamos la clase para saber su estructura

                    if (padre != null)
                    {
                        Nodo3D hijo = Hijo.Generar3DHijo(padre, nodo.Valor);

                        nodo.Valor = hijo.Valor;
                        nodo.Codigo += hijo.Codigo;
                        nodo.F = hijo.F;
                        nodo.V = hijo.V;
                        nodo.Tipo = hijo.Tipo;
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No exite la clase " + nodo.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                    }


                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder a la variable " + LlamadaArreglo.Nombre + " de tipo " + nodo.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                }
            }

            return nodo;
        }

        public Nodo3D Generar3DArregloHijo(Simbolo padre, String temporal)
        {
            Nodo3D nodo = new Nodo3D();
            nodo = LlamadaArreglo.Generar3DHijo(padre,temporal);

            if (Hijo != null)
            {
                if (!nodo.Tipo.Equals(Constante.TEntero) && !nodo.Tipo.Equals(Constante.TDecimal) && !nodo.Tipo.Equals(Constante.TCaracter) && !nodo.Tipo.Equals(Constante.TCadena) && !nodo.Tipo.Equals(Constante.TBooleano))
                {
                    //nodo.Tipo = sim.Tipo;//el simbolo trae el tipo de la variable 
                    Simbolo papa = Padre.BuscarClasePadre();//obtenemos quien es la clase padre para obtener a que archivo pertenece
                    FClase clasepapa = (FClase)papa.Valor;//castemos la clase
                    Simbolo padre2 = papa.BuscarClase(nodo.Tipo, clasepapa.ArchivoPadre);//buscamos la clase para saber su estructura

                    if (padre2 != null)
                    {
                        Nodo3D hijo = Hijo.Generar3DHijo(padre2, nodo.Valor);

                        nodo.Valor = hijo.Valor;
                        nodo.Codigo += hijo.Codigo;
                        nodo.F = hijo.F;
                        nodo.V = hijo.V;
                        nodo.Tipo = hijo.Tipo;
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No exite la clase " + nodo.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                    }


                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder a la variable " + LlamadaArreglo.Nombre + " de tipo " + nodo.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                }
            }

            return nodo;
        }


        public Nodo3D Generar3DVariable()
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = Padre.BuscarVariable(this.Nombre);


            if (sim != null)
            {
                if (Hijo == null)
                {
                    if (sim.Rol.Equals(Constante.DECLARACION))
                    {
                        FDeclaracion decla = (FDeclaracion)sim.Valor;
                        if (decla.Dimensiones.Count == 0)
                        {
                            nodo.Tipo = sim.Tipo;
                        }
                        else
                        {
                            nodo.Tipo = "arreglo " + sim.Tipo;
                        }

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
                    }
                    else
                    {
                        FParametro decla = (FParametro)sim.Valor;
                        if (decla.Dimensiones == 0)
                        {
                            nodo.Tipo = sim.Tipo;
                        }
                        else
                        {
                            nodo.Tipo = "arreglo " + sim.Tipo;
                        }
                        String pos = TitusTools.GetTemp();
                        nodo.Valor = TitusTools.GetTemp();
                        nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                        nodo.Codigo += "\t\t" + nodo.Valor + " = Stack[" + pos + "];\n";

                    }


                }
                else//si tiene hijo 
                {
                    if (!sim.Tipo.Equals(Constante.TEntero) && !sim.Tipo.Equals(Constante.TDecimal) && !sim.Tipo.Equals(Constante.TCaracter) && !sim.Tipo.Equals(Constante.TCadena) && !sim.Tipo.Equals(Constante.TBooleano))
                    {
                        if (sim.Rol.Equals(Constante.DECLARACION))//si es declaracoin
                        {
                            FDeclaracion decla = (FDeclaracion)sim.Valor;
                            if (decla.Dimensiones.Count == 0)
                            {
                                //nodo.Tipo = sim.Tipo;//el simbolo trae el tipo de la variable 
                                Simbolo papa = Padre.BuscarClasePadre();//obtenemos quien es la clase padre para obtener a que archivo pertenece
                                FClase clasepapa = (FClase)papa.Valor;//castemos la clase
                                Simbolo padre = papa.BuscarClase(sim.Tipo, clasepapa.ArchivoPadre);//buscamos la clase para saber su estructura

                                if (padre != null)
                                {
                                    if (sim.Ambito.Nombre.Equals("§global§"))
                                    {
                                        String pos = TitusTools.GetTemp();
                                        String heap = TitusTools.GetTemp();
                                        String temp = TitusTools.GetTemp();
                                        nodo.Codigo += "\t\t" + pos + " = Stack[P];\n";
                                        nodo.Codigo += "\t\t" + heap + " = " + pos + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                                        nodo.Codigo += "\t\t" + temp + " = Heap[" + heap + "];\n";

                                        Nodo3D hijo = Hijo.Generar3DHijo(padre, temp);

                                        nodo.Valor = hijo.Valor;
                                        nodo.Codigo += hijo.Codigo;
                                        nodo.F = hijo.F;
                                        nodo.V = hijo.V;
                                        nodo.Tipo = hijo.Tipo;
                                    }
                                    else
                                    {
                                        String pos = TitusTools.GetTemp();
                                        String temp = TitusTools.GetTemp();
                                        nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                                        nodo.Codigo += "\t\t" + temp + " = Stack[" + pos + "];\n";

                                        Nodo3D hijo = Hijo.Generar3DHijo(padre, temp);

                                        nodo.Valor = hijo.Valor;
                                        nodo.Codigo += hijo.Codigo;
                                        nodo.F = hijo.F;
                                        nodo.V = hijo.V;
                                        nodo.Tipo = hijo.Tipo;
                                    }
                                }
                                else
                                {
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No exite la clase " + sim.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                }
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder al arreglo " + decla.Nombre + " como objeto.", TitusTools.GetRuta(), this.Fila, this.Columna);
                            }
                        }
                        else//si es parametro
                        {
                            FParametro decla = (FParametro)sim.Valor;
                            if (decla.Dimensiones == 0)
                            {
                                //nodo.Tipo = sim.Tipo;//el simbolo trae el tipo de la variable 
                                Simbolo papa = Padre.BuscarClasePadre();//obtenemos quien es la clase padre para obtener a que archivo pertenece
                                FClase clasepapa = (FClase)papa.Valor;//castemos la clase
                                Simbolo padre = papa.BuscarClase(sim.Tipo, clasepapa.ArchivoPadre);//buscamos la clase para saber su estructura

                                if (padre != null)
                                {
                                    String pos = TitusTools.GetTemp();
                                    String temp = TitusTools.GetTemp();
                                    nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";\n";
                                    nodo.Codigo += "\t\t" + temp + " = Stack[" + pos + "];\n";

                                    Nodo3D hijo = Hijo.Generar3DHijo(padre, temp);

                                    nodo.Valor = hijo.Valor;
                                    nodo.Codigo += hijo.Codigo;
                                    nodo.F = hijo.F;
                                    nodo.V = hijo.V;
                                    nodo.Tipo = hijo.Tipo;
                                }
                                else
                                {
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No exite la clase " + sim.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                                }
                            }
                            else
                            {
                                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder al arreglo " + decla.Nombre + " como objeto.", TitusTools.GetRuta(), this.Fila, this.Columna);
                            }
                        }
                    }
                    else
                    {
                        TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder a la variable " + sim.Nombre + " de tipo " + sim.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                    }
                }

            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, TitusTools.GetRuta(), this.Fila, this.Columna);
            }

            return nodo;
        }



        private Nodo3D Generar3DVariableHijo(Simbolo padre, String temporal)
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = padre.BuscarVariable(this.Nombre);

            if (sim != null)
            {
                if (sim.Visibilidad.Equals(Constante.TPublico))
                {
                    if (Hijo == null)
                    {
                        if (sim.Rol.Equals(Constante.DECLARACION))
                        {
                            FDeclaracion decla = (FDeclaracion)sim.Valor;
                            if (decla.Dimensiones.Count == 0)
                            {
                                nodo.Tipo = sim.Tipo;
                            }
                            else
                            {
                                nodo.Tipo = "arreglo " + sim.Tipo;
                            }

                            if (sim.Ambito.Nombre.Equals("§global§"))
                            {
                                String heap = TitusTools.GetTemp();
                                nodo.Valor = TitusTools.GetTemp();
                                nodo.Codigo += "\t\t" + heap + " = " + temporal + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                                nodo.Codigo += "\t\t" + nodo.Valor + " = Heap[" + heap + "];//valor de la variable " + sim.Nombre + "\n";
                            }
                        }
                    }
                    else//si tiene hijo 
                    {
                        if (!sim.Tipo.Equals(Constante.TEntero) && !sim.Tipo.Equals(Constante.TDecimal) && !sim.Tipo.Equals(Constante.TCaracter) && !sim.Tipo.Equals(Constante.TCadena) && !sim.Tipo.Equals(Constante.TBooleano))
                        {
                            if (sim.Rol.Equals(Constante.DECLARACION))//si es declaracoin
                            {
                                FDeclaracion decla = (FDeclaracion)sim.Valor;
                                if (decla.Dimensiones.Count == 0)//comprobamos que no sea una declaracion
                                {
                                    //nodo.Tipo = sim.Tipo;//el simbolo trae el tipo de la variable 
                                    Simbolo papa = padre.BuscarClasePadre();//obtenemos quien es la clase padre para obtener a que archivo pertenece
                                    FClase clasepapa = (FClase)papa.Valor;//castemos la clase
                                    Simbolo papito = papa.BuscarClase(sim.Tipo, clasepapa.ArchivoPadre);//buscamos la clase para saber su estructura

                                    if (sim.Ambito.Nombre.Equals("§global§"))
                                    {
                                        String heap = TitusTools.GetTemp();
                                        String temp = TitusTools.GetTemp();
                                        nodo.Codigo += "\t\t" + heap + " = " + temporal + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                                        nodo.Codigo += "\t\t" + temp + " = Heap[" + heap + "];//valor de la variable " + sim.Nombre + "\n";

                                        Nodo3D hijo = Hijo.Generar3DHijo(papito, temp);

                                        nodo.Valor = hijo.Valor;
                                        nodo.Codigo += hijo.Codigo;
                                        nodo.F = hijo.F;
                                        nodo.V = hijo.V;
                                        nodo.Tipo = hijo.Tipo;
                                    }
                                }
                                else
                                {
                                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder al arreglo " + decla.Nombre + " como objeto.", TitusTools.GetRuta(), this.Fila, this.Columna);
                                }
                            }
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder a la variable " + sim.Nombre + " de tipo " + sim.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
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
