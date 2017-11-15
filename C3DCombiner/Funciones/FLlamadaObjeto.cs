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

        //3d super acceso
        public Nodo3D Generar3DSuper(String temporal)
        {
            Nodo3D cadena = new Nodo3D();

            switch (Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    cadena = Generar3DArregloSuper(temporal);
                    break;


                case Constante.LLAMADA_METODO:
                    cadena = Generar3DMetodoSuper(temporal);
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    cadena = Generar3DMetodoArregloSuper(temporal);
                    break;

                default:
                    cadena = Generar3DVariableSuper(temporal);

                    break;
            }

            return cadena;
        }

        //3d super asignacion
        public Nodo3D Generar3DSuperAsginacion(String temporal)
        {
            Nodo3D cadena = new Nodo3D();

            switch (Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    cadena = Generar3DArregloSuperAsignacion(temporal);
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    cadena = Generar3DMetodoArregloSuperAsignacion(temporal);
                    break;

                case Constante.LLAMADA_METODO:
                    cadena = Generar3DMetodoSuperAsignacion(temporal);
                    break;

                default:
                    cadena = Generar3DVariableSuperAsignacion(temporal);
                    break;
            }

            return cadena;
        }

        //3d para acceso
        public Nodo3D Generar3D()
        {
            Nodo3D cadena = new Nodo3D();

            switch (Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    cadena = Generar3DArreglo();
                    break;


                case Constante.LLAMADA_METODO:
                    cadena = Generar3DMetodo();
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    cadena = Generar3DMetodoArreglo();
                    break;

                case Constante.TSelf:
                    cadena = Generar3DEste();
                    break;

                case Constante.TEste:
                    cadena = Generar3DEste();
                    break;

                case Constante.TSuper:
                    cadena = Generar3DSuperAcceso();
                    break;

                default:
                    if (Nombre.Equals(Constante.TEste) || Nombre.Equals(Constante.TSelf))
                    {
                        cadena = Generar3DEste();
                    }
                    else
                    {
                        cadena = Generar3DVariable();
                    }
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
                    cadena = Generar3DMetodoHijo(padre, temporal);
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    cadena = Generar3DMetodoArregloHijo(padre, temporal);
                    break;

                default:
                    cadena = Generar3DVariableHijo(padre, temporal);
                    break;
            }

            return cadena;
        }

        private Nodo3D Generar3DSuperAcceso()
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo padre = this.Padre.BuscarClasePadre();

            FClase clasepadre = (FClase)padre.Valor;

            if (!clasepadre.Herencia.Equals(""))
            {


                Simbolo simhere = padre.BuscarClase(clasepadre.Herencia, clasepadre.ArchivoPadre);

                if (simhere != null)
                {
                    String posstack = TitusTools.GetTemp();
                    nodo.Codigo += "\t\t" + posstack + " = Stack[P];\n";
                    String nuevapos = TitusTools.GetTemp();
                    nodo.Codigo += "\t\t" + nuevapos + " = " + posstack + " + " + clasepadre.Ambito.Tamaño.ToString() + ";//posicion en el this de la herencia\n";

                    if (Hijo != null)
                    {
                        Hijo.SetPadre(simhere);
                        Hijo.Padre = simhere;
                        Nodo3D hijo = Hijo.Generar3DSuper(nuevapos);

                        nodo.Valor = hijo.Valor;
                        nodo.Codigo += hijo.Codigo;
                        nodo.F = hijo.F;
                        nodo.V = hijo.V;
                        nodo.Tipo = hijo.Tipo;
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No exite ninguna clase llamada " + clasepadre.Herencia, TitusTools.GetRuta(), Fila, Columna);
                }
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede utiliza la instruccion super porque no existe ninguna herencia", TitusTools.GetRuta(), Fila, Columna);
            }

            return nodo;
        }

        private Nodo3D Generar3DSuperAsignacion()
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo padre = this.Padre.BuscarClasePadre();

            FClase clasepadre = (FClase)padre.Valor;

            if (!clasepadre.Herencia.Equals(""))
            {


                Simbolo simhere = padre.BuscarClase(clasepadre.Herencia, clasepadre.ArchivoPadre);

                if (simhere != null)
                {
                    String posstack = TitusTools.GetTemp();
                    nodo.Codigo += "\t\t" + posstack + " = Stack[P];\n";
                    String nuevapos = TitusTools.GetTemp();
                    nodo.Codigo += "\t\t" + nuevapos + " = " + posstack + " + " + clasepadre.Ambito.Tamaño.ToString() + ";//posicion en el this de la herencia\n";

                    if (Hijo != null)
                    {
                        Hijo.SetPadre(simhere);
                        Hijo.Padre = simhere;
                        Nodo3D hijo = Hijo.Generar3DSuperAsginacion(nuevapos);

                        nodo.Valor = hijo.Valor;
                        nodo.Codigo += hijo.Codigo;
                        nodo.F = hijo.F;
                        nodo.V = hijo.V;
                        nodo.Tipo = hijo.Tipo;
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No exite ninguna clase llamada " + clasepadre.Herencia, TitusTools.GetRuta(), Fila, Columna);
                }
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede utiliza la instruccion super porque no existe ninguna herencia", TitusTools.GetRuta(), Fila, Columna);
            }

            return nodo;
        }

        private Nodo3D Generar3DEste()
        {
            Nodo3D nodo = new Nodo3D();

            if (Hijo != null)
            {

                Nodo3D hijo = Hijo.Generar3D();
                nodo.Valor = hijo.Valor;
                nodo.Codigo += hijo.Codigo;
                nodo.F = hijo.F;
                nodo.V = hijo.V;
                nodo.Tipo = hijo.Tipo;
            }
            else
            {
                Simbolo padre = this.Padre.BuscarClasePadre();
                String posStack = TitusTools.GetTemp();
                nodo.Valor = TitusTools.GetTemp();
                nodo.Codigo += "\t\t" + posStack + " = P + 0;//Posicion del this\n";
                nodo.Codigo += "\t\t" + nodo.Valor + " = Stack[" + posStack + "];//valor del this\n";
                nodo.Tipo = padre.Nombre;
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
            nodo = LlamadaArreglo.Generar3DHijo(padre, temporal);

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

        public Nodo3D Generar3DMetodo()
        {
            Nodo3D nodo = new Nodo3D();
            nodo = LlamadaMetodo.Generar3D();

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
        public Nodo3D Generar3DMetodoHijo(Simbolo padre, String temporal)
        {
            Nodo3D nodo = new Nodo3D();
            nodo = LlamadaMetodo.Generar3DHijo(padre, temporal);

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

        public Nodo3D Generar3DMetodoArreglo()
        {
            Nodo3D nodo = LlamadaMetodoArreglo.Generar3D();
            return nodo;
        }

        public Nodo3D Generar3DMetodoArregloHijo(Simbolo padre, String temporal)
        {
            Nodo3D nodo = LlamadaMetodoArreglo.Generar3DHijo(padre, temporal);
            return nodo;
        }

        //3d para asignaciones
        public Nodo3D Generar3DAsginacion()
        {
            Nodo3D cadena = new Nodo3D();

            switch (Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    cadena = Generar3DArregloAsignacion();
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    cadena = Generar3DMetodoArregloAsignacion();
                    break;

                case Constante.LLAMADA_METODO:
                    cadena = Generar3DMetodoAsignacion();
                    break;

                case Constante.TSelf:
                    cadena = Generar3DEsteAsignacion();
                    break;

                case Constante.TEste:
                    cadena = Generar3DEsteAsignacion();
                    break;

                case Constante.TSuper:
                    cadena = Generar3DSuperAsignacion();
                    break;

                default:
                    if (Nombre.Equals(Constante.TEste) || Nombre.Equals(Constante.TSelf))
                    {
                        cadena = Generar3DEsteAsignacion();

                    }
                    else
                    {
                        cadena = Generar3DVariableAsignacion();
                    }
                    break;
            }

            return cadena;
        }


        public Nodo3D Generar3DHijoAsignacion(Simbolo padre, String temporal)
        {
            Nodo3D cadena = new Nodo3D();

            switch (Tipo)
            {
                case Constante.LLAMADA_ARREGLO:
                    cadena = Generar3DArregloHijoAsignacion(padre, temporal);
                    break;


                case Constante.LLAMADA_METODO:
                    cadena = Generar3DMetodoHijoAsignacion(padre, temporal);
                    break;

                case Constante.LLAMADA_METODO_ARREGLO:
                    cadena = Generar3DMetodoArregloHijoAsignacion(padre, temporal);
                    break;

                default:
                    cadena = Generar3DVariableHijoAsignacion(padre, temporal);
                    break;
            }

            return cadena;
        }

        private Nodo3D Generar3DEsteAsignacion()
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo padre = this.Padre.BuscarClasePadre();



            if (Hijo != null)
            {
                Nodo3D hijo = Hijo.Generar3DAsginacion();

                nodo.Valor = hijo.Valor;
                nodo.Codigo += hijo.Codigo;
                nodo.F = hijo.F;
                nodo.V = hijo.V;
                nodo.Tipo = hijo.Tipo;
            }
            else
            {
                String posStack = TitusTools.GetTemp();
                nodo.Valor = TitusTools.GetTemp();
                nodo.Codigo += "\t\t" + posStack + " = P + 0;//Posicion del this\n";
                nodo.Codigo += "\t\t" + nodo.Valor + " = Stack[" + posStack + "];//valor del this\n";
                nodo.Tipo = padre.Nombre;
            }

            return nodo;
        }
        
        public Nodo3D Generar3DVariableAsignacion()
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
                            nodo.Codigo += "\t\t" + pos + " = Stack[P];\n";
                            nodo.Codigo += "\t\t" + heap + " = " + pos + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                            nodo.Valor = "Heap[" + heap + "]";
                        }
                        else
                        {
                            String pos = TitusTools.GetTemp();
                            nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                            nodo.Valor = "Stack[" + pos + "]";
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
                        nodo.Codigo += "\t\t" + pos + " = P + " + (sim.Posicion + 2).ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                        nodo.Valor = "Stack[" + pos + "]";

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

                                        Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre, temp);

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

                                        Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre, temp);

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

                                    Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre, temp);

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

        private Nodo3D Generar3DVariableHijoAsignacion(Simbolo padre, String temporal)
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
                                nodo.Codigo += "\t\t" + heap + " = " + temporal + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                                nodo.Valor = "Heap[" + heap + "]";
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

                                        Nodo3D hijo = Hijo.Generar3DHijoAsignacion(papito, temp);

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

        public Nodo3D Generar3DArregloAsignacion()
        {
            Nodo3D nodo = new Nodo3D();


            if (Hijo != null)
            {
                nodo = LlamadaArreglo.Generar3D();
                if (!nodo.Tipo.Equals(Constante.TEntero) && !nodo.Tipo.Equals(Constante.TDecimal) && !nodo.Tipo.Equals(Constante.TCaracter) && !nodo.Tipo.Equals(Constante.TCadena) && !nodo.Tipo.Equals(Constante.TBooleano))
                {
                    //nodo.Tipo = sim.Tipo;//el simbolo trae el tipo de la variable 
                    Simbolo papa = Padre.BuscarClasePadre();//obtenemos quien es la clase padre para obtener a que archivo pertenece
                    FClase clasepapa = (FClase)papa.Valor;//castemos la clase
                    Simbolo padre = papa.BuscarClase(nodo.Tipo, clasepapa.ArchivoPadre);//buscamos la clase para saber su estructura

                    if (padre != null)
                    {
                        Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre, nodo.Valor);

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
            else
            {
                nodo = LlamadaArreglo.Generar3DAsignacion();
            }

            return nodo;
        }

        public Nodo3D Generar3DArregloHijoAsignacion(Simbolo padre, String temporal)
        {
            Nodo3D nodo = new Nodo3D();


            if (Hijo != null)
            {
                nodo = LlamadaArreglo.Generar3DHijo(padre, temporal);
                if (!nodo.Tipo.Equals(Constante.TEntero) && !nodo.Tipo.Equals(Constante.TDecimal) && !nodo.Tipo.Equals(Constante.TCaracter) && !nodo.Tipo.Equals(Constante.TCadena) && !nodo.Tipo.Equals(Constante.TBooleano))
                {
                    //nodo.Tipo = sim.Tipo;//el simbolo trae el tipo de la variable 
                    Simbolo papa = Padre.BuscarClasePadre();//obtenemos quien es la clase padre para obtener a que archivo pertenece
                    FClase clasepapa = (FClase)papa.Valor;//castemos la clase
                    Simbolo padre2 = papa.BuscarClase(nodo.Tipo, clasepapa.ArchivoPadre);//buscamos la clase para saber su estructura

                    if (padre2 != null)
                    {
                        Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre2, nodo.Valor);

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
            else
            {
                nodo = LlamadaArreglo.Generar3DHijoAsignacion(padre, temporal);
            }

            return nodo;
        }

        public Nodo3D Generar3DMetodoAsignacion()
        {
            Nodo3D nodo = new Nodo3D();
            nodo = LlamadaMetodo.Generar3DAsignacion();

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
                        Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre, nodo.Valor);

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

        public Nodo3D Generar3DMetodoHijoAsignacion(Simbolo padrepadre, String temporal)
        {
            Nodo3D nodo = new Nodo3D();
            nodo = LlamadaMetodo.Generar3DHijoAsignacion(padrepadre, temporal);

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
                        Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre, nodo.Valor);

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

        public Nodo3D Generar3DMetodoArregloAsignacion()
        {
            Nodo3D nodo = LlamadaMetodoArreglo.Generar3DAsignacion();
            return nodo;
        }

        public Nodo3D Generar3DMetodoArregloHijoAsignacion(Simbolo padre, String temporal)
        {
            Nodo3D nodo = LlamadaMetodoArreglo.Generar3DHijoAsignacion(padre, temporal);
            return nodo;
        }
        
        //////////////////////super acceso
        public Nodo3D Generar3DVariableSuper(String temporalposicion)
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = Padre.BuscarVariable(this.Nombre);


            if (sim != null)
            {
                if (Hijo == null)
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

                    String pos = TitusTools.GetTemp();
                    String heap = TitusTools.GetTemp();
                    nodo.Valor = TitusTools.GetTemp();
                    nodo.Codigo += "\t\t" + heap + " = " + temporalposicion + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                    nodo.Codigo += "\t\t" + nodo.Valor + " = Heap[" + heap + "];\n";
                }
                else//si tiene hijo 
                {
                    if (!sim.Tipo.Equals(Constante.TEntero) && !sim.Tipo.Equals(Constante.TDecimal) && !sim.Tipo.Equals(Constante.TCaracter) && !sim.Tipo.Equals(Constante.TCadena) && !sim.Tipo.Equals(Constante.TBooleano))
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
                                String heap = TitusTools.GetTemp();
                                String temp = TitusTools.GetTemp();
                                nodo.Codigo += "\t\t" + heap + " = " + temporalposicion + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
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
                                TitusTools.InsertarError(Constante.TErrorSemantico, "No exite la clase " + sim.Tipo, TitusTools.GetRuta(), this.Fila, this.Columna);
                            }
                        }
                        else
                        {
                            TitusTools.InsertarError(Constante.TErrorSemantico, "No se puede acceder al arreglo " + decla.Nombre + " como objeto.", TitusTools.GetRuta(), this.Fila, this.Columna);
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

        public Nodo3D Generar3DArregloSuper(String temporalposicion)
        {
            Nodo3D nodo = new Nodo3D();
            nodo = LlamadaArreglo.Generar3DSuper(temporalposicion);

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

        public Nodo3D Generar3DMetodoSuper(String temporaposicion)
        {
            Nodo3D nodo = new Nodo3D();
            nodo = LlamadaMetodo.Generar3DSuper(temporaposicion);

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

        public Nodo3D Generar3DMetodoArregloSuper(String temporalposicion)
        {
            Nodo3D nodo = LlamadaMetodoArreglo.Generar3DSuper(temporalposicion);/////cambair a super
            return nodo;
        }
        


        ////////////////////////super asignacion
        public Nodo3D Generar3DVariableSuperAsignacion(String temporal)
        {
            Nodo3D nodo = new Nodo3D();

            Simbolo sim = Padre.BuscarVariable(this.Nombre);


            if (sim != null)
            {
                if (Hijo == null)
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

                    String heap = TitusTools.GetTemp();
                    nodo.Codigo += "\t\t" + heap + " = " + temporal + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                    nodo.Valor = "Heap[" + heap + "]";

                }
                else//si tiene hijo 
                {
                    if (!sim.Tipo.Equals(Constante.TEntero) && !sim.Tipo.Equals(Constante.TDecimal) && !sim.Tipo.Equals(Constante.TCaracter) && !sim.Tipo.Equals(Constante.TCadena) && !sim.Tipo.Equals(Constante.TBooleano))
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
                                String heap = TitusTools.GetTemp();
                                String temp = TitusTools.GetTemp();
                                nodo.Codigo += "\t\t" + heap + " = " + temporal + " + " + sim.Posicion.ToString() + ";//posicion de la variable " + sim.Nombre + "\n";
                                nodo.Codigo += "\t\t" + temp + " = Heap[" + heap + "];\n";

                                Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre, temp);

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

        public Nodo3D Generar3DArregloSuperAsignacion(String temporal)
        {
            Nodo3D nodo = new Nodo3D();


            if (Hijo != null)
            {
                nodo = LlamadaArreglo.Generar3DSuperAsignacion(temporal);
                if (!nodo.Tipo.Equals(Constante.TEntero) && !nodo.Tipo.Equals(Constante.TDecimal) && !nodo.Tipo.Equals(Constante.TCaracter) && !nodo.Tipo.Equals(Constante.TCadena) && !nodo.Tipo.Equals(Constante.TBooleano))
                {
                    //nodo.Tipo = sim.Tipo;//el simbolo trae el tipo de la variable 
                    Simbolo papa = Padre.BuscarClasePadre();//obtenemos quien es la clase padre para obtener a que archivo pertenece
                    FClase clasepapa = (FClase)papa.Valor;//castemos la clase
                    Simbolo padre = papa.BuscarClase(nodo.Tipo, clasepapa.ArchivoPadre);//buscamos la clase para saber su estructura

                    if (padre != null)
                    {
                        Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre, nodo.Valor);

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
            else
            {
                nodo = LlamadaArreglo.Generar3DSuperAsignacion(temporal);
            }

            return nodo;
        }

        public Nodo3D Generar3DMetodoSuperAsignacion(String temporaposicion)
        {
            Nodo3D nodo = new Nodo3D();
            nodo = LlamadaMetodo.Generar3DSuperAsignacion(temporaposicion);

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
                        Nodo3D hijo = Hijo.Generar3DHijoAsignacion(padre, nodo.Valor);

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

        public Nodo3D Generar3DMetodoArregloSuperAsignacion(String temporal)
        {
            Nodo3D nodo = LlamadaMetodoArreglo.Generar3DSuperAsignacion(temporal);
            return nodo;
        }

    }
}
