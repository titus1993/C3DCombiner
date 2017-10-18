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

        public FLlamadaObjeto(String Tipo, String nombre, int fila, int columna, Object valor)
        {
            this.Tipo = Tipo;
            this.Nombre = nombre;
            this.Fila = fila;
            this.Columna = columna;
            this.LlamadaMetodo = null;
            this.LlamadaArreglo = null;

            if (Tipo.Equals(Constante.TVariableArreglo))
            {
                this.LlamadaArreglo = (FLlamadaArreglo)valor;
            }
            else if (Tipo.Equals(Constante.TMetodo))
            {
                this.LlamadaMetodo = (FLlamadaMetodo)valor;
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

        public Variable Ejecutar(Objeto objeto, Objeto padre, int pos)
        {
            if (this.Tipo.Equals(Constante.TVariable))
            {
                Variable actual = objeto.TablaVariables.BuscarVariable(this.Nombre);
                if (actual != null)
                {
                    //exite la variable en el ambito actual ahora verificamos si tiene hijos
                    if (this.Hijo != null)
                    {
                        if (!actual.Tipo.Equals(Constante.TCadena) && !actual.Tipo.Equals(Constante.TCaracter) && !actual.Tipo.Equals(Constante.TEntero) && !actual.Tipo.Equals(Constante.TDecimal) && !actual.Tipo.Equals(Constante.TBool))
                        {
                            if ((FNodoExpresion)actual.Valor != null)
                            {
                                Objeto nuevo = ((FNodoExpresion)actual.Valor).Obj;
                                if (nuevo != null)
                                {
                                    return Hijo.EjecutarHijo(nuevo, objeto, pos);
                                }
                                else
                                {
                                    TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "La variable " + this.Hijo.Nombre + " esta nula", this.Hijo.Fila, this.Hijo.Columna);
                                }
                            }
                            else
                            {
                                TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "La variable " + actual.Nombre + " esta nula", Fila, Columna);
                            }
                        }
                        else
                        {
                            TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "La variable " + actual.Nombre + " no es un Als", Fila, Columna);
                        }
                    }
                    else
                    {
                        //devolvemos el valor de la variable

                        return actual;
                    }
                }
                else
                {
                    TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, this.Fila, this.Columna);
                }
            }
            else if (this.Tipo.Equals(Constante.TMetodo))
            {
                if (LlamadaMetodo.Tipo.Equals(Constante.Graphik))
                {
                    ArrayList<FNodoExpresion> listaparametros = new ArrayList<>();
                    for (FNodoExpresion par : LlamadaMetodo.Parametros)
                    {
                        FNodoExpresion nuevopar = par.ResolverExpresion(padre, pos);
                        if (nuevopar.Tipo.Equals(Constante.TVariableArreglo))
                        {
                            nuevopar = nuevopar.PosArreglo;
                        }
                        listaparametros.add(nuevopar);
                    }

                    Variable actual = objeto.TablaVariables.BuscarFuncion(LlamadaMetodo, listaparametros);
                    if (actual != null)
                    {
                        //primero ejecutamos el metodo
                        FMetodo metodo = (FMetodo)actual.Valor;
                        //obtenemos el return
                        Variable retorno = metodo.EjecutarMetodo(LlamadaMetodo, objeto, actual, padre, pos);

                        //metodo.Ejecutar(objeto)
                        //exite el metodo en el ambito actual ahora verificamos si tiene hijos
                        if (this.Hijo != null)
                        {
                            //comprobamos si es null para notificar error
                            if (retorno != null && !actual.Tipo.Equals(Constante.TVacio))
                            {
                                if (!actual.Tipo.Equals(Constante.TCadena) && !actual.Tipo.Equals(Constante.TCaracter) && !actual.Tipo.Equals(Constante.TEntero) && !actual.Tipo.Equals(Constante.TDecimal) && !actual.Tipo.Equals(Constante.TBool))
                                {

                                    Objeto nuevo = ((FNodoExpresion)retorno.Valor).Obj;
                                    if (nuevo != null)
                                    {
                                        return Hijo.EjecutarHijo(nuevo, objeto, pos);
                                    }
                                    else
                                    {
                                        if (this.Hijo.Hijo == null)
                                        {
                                            return retorno;
                                        }
                                        else
                                        {
                                            TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "El metodo " + this.Hijo.Nombre + " esta nula", this.Hijo.Fila, this.Hijo.Columna);
                                        }
                                    }
                                }
                                else
                                {
                                    TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "El metodo " + actual.Nombre + " no retonra un Als, retorna un tipo " + actual.Tipo, Fila, Columna);
                                }
                            }
                            else
                            {
                                if (actual.Tipo.Equals(Constante.TVacio))
                                {
                                    TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "El metodo " + actual.Nombre + " es de tipo vacio ", Fila, Columna);
                                }
                                else
                                {
                                    TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "El metodo " + actual.Nombre + " no retorno ningun valor ", Fila, Columna);
                                }
                            }

                        }
                        else
                        {
                            return retorno;
                        }
                    }
                    else
                    {
                        TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se encontro el metodo " + this.Nombre, this.Fila, this.Columna);
                    }
                }
                else
                {
                    //resolvemos los parametros
                    ArrayList<FNodoExpresion> listaparametros = new ArrayList<>();
                    for (FNodoExpresion par : LlamadaMetodo.Parametros)
                    {
                        FNodoExpresion nuevopar = par.ResolverExpresion(padre, pos);
                        if (nuevopar.Tipo.Equals(Constante.TVariableArreglo))
                        {
                            if (nuevopar.PosArreglo != null)
                            {
                                nuevopar = nuevopar.PosArreglo;
                            }
                        }
                        listaparametros.add(nuevopar);
                    }

                    EjecutarHaskell ex = new EjecutarHaskell();
                    return ex.Ejecutar(listaparametros, LlamadaMetodo.Nombre, LlamadaMetodo);

                }
            }
            else if (this.Tipo.Equals(Constante.TVariableArreglo))
            {
                Variable actual = objeto.TablaVariables.BuscarVariable(this.Nombre);
                if (actual != null)
                {
                    //exite la variable en el ambito actual ahora verificamos si tiene hijos
                    if (this.Hijo != null)
                    {
                        TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se puede acceder a objeto en arreglos, piensa XD", Fila, Columna);
                    }
                    else
                    {
                        //devolvemos el valor de la variable
                        if (this.LlamadaArreglo != null)
                        {
                            return actual;
                        }
                    }
                }
                else
                {
                    TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, this.Fila, this.Columna);
                }
            }
            return null;
        }

        private Variable EjecutarHijo(Objeto objeto, Objeto padre, int pos)
        {
            if (this.Tipo.Equals(Constante.TVariable))
            {
                Variable actual = objeto.TablaVariables.BuscarVariable(this.Nombre);
                if (actual != null)
                {

                    //exite la variable en el ambito actual ahora verificamos si tiene hijos
                    if (actual.Visibilidad.Equals(Constante.TPublico) || actual.Visibilidad.Equals(Constante.TProtegido))
                    {
                        if (this.Hijo != null)
                        {
                            if (!actual.Tipo.Equals(Constante.TCadena) && !actual.Tipo.Equals(Constante.TCaracter) && !actual.Tipo.Equals(Constante.TEntero) && !actual.Tipo.Equals(Constante.TDecimal) && !actual.Tipo.Equals(Constante.TBool))
                            {
                                if ((FNodoExpresion)actual.Valor != null)
                                {
                                    Objeto nuevo = ((FNodoExpresion)actual.Valor).Obj;

                                    return Hijo.EjecutarHijo(nuevo, objeto, pos);

                                }
                                else
                                {
                                    TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "La variable " + actual.Nombre + " esta nula", Fila, Columna);
                                }
                            }
                            else
                            {
                                TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "La variable " + actual.Nombre + " no es un Als", Fila, Columna);
                            }
                        }
                        else
                        {
                            //devolvemos el valor de la variable
                            return actual;
                        }
                    }
                    else
                    {
                        TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se puede acceder a la variable " + actual.Visibilidad + " " + actual.Nombre, Fila, Columna);
                        return null;
                    }
                }
                else
                {
                    TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, this.Fila, this.Columna);
                }
            }
            else if (this.Tipo.Equals(Constante.TMetodo))
            {
                if (LlamadaMetodo.Tipo.Equals(Constante.Graphik) || LlamadaMetodo.Tipo.Equals(Constante.Haskell))
                {
                    ArrayList<FNodoExpresion> listaparametros = new ArrayList<>();
                    for (FNodoExpresion par : LlamadaMetodo.Parametros)
                    {
                        FNodoExpresion nuevopar = par.ResolverExpresion(padre, pos);
                        if (nuevopar.Tipo.Equals(Constante.TVariableArreglo))
                        {
                            nuevopar = nuevopar.PosArreglo;
                        }
                        listaparametros.add(nuevopar);
                    }
                    Variable actual = objeto.TablaVariables.BuscarFuncion(LlamadaMetodo, listaparametros);
                    if (actual != null)
                    {
                        //primero ejecutamos el metodo
                        FMetodo metodo = (FMetodo)actual.Valor;

                        if (metodo.Visibilidad.Equals(Constante.TPublico) || metodo.Visibilidad.Equals(Constante.TProtegido))
                        {
                            //obtenemos el return
                            Variable retorno = metodo.EjecutarMetodo(LlamadaMetodo, objeto, actual, padre, pos);

                            //metodo.Ejecutar(objeto)
                            //exite el metodo en el ambito actual ahora verificamos si tiene hijos
                            if (this.Hijo != null)
                            {
                                //comprobamos si es null para notificar error
                                if (retorno != null && !actual.Tipo.Equals(Constante.TVacio))
                                {
                                    if (!actual.Tipo.Equals(Constante.TCadena) && !actual.Tipo.Equals(Constante.TCaracter) && !actual.Tipo.Equals(Constante.TEntero) && !actual.Tipo.Equals(Constante.TDecimal) && !actual.Tipo.Equals(Constante.TBool))
                                    {

                                        Objeto nuevo = ((FNodoExpresion)retorno.Valor).Obj;
                                        if (nuevo != null)
                                        {
                                            return Hijo.EjecutarHijo(nuevo, objeto, pos);
                                        }
                                        else
                                        {
                                            if (this.Hijo.Hijo == null)
                                            {
                                                return retorno;

                                            }
                                            else
                                            {
                                                TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "El metodo " + this.Hijo.Nombre + " esta nula", this.Hijo.Fila, this.Hijo.Columna);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "El metodo " + actual.Nombre + " no retonra un Als, retorna un tipo " + actual.Tipo, Fila, Columna);
                                    }
                                }
                                else
                                {
                                    if (actual.Tipo.Equals(Constante.TVacio))
                                    {
                                        TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "El metodo " + actual.Nombre + " es de tipo vacio ", Fila, Columna);
                                    }
                                    else
                                    {
                                        TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "El metodo " + actual.Nombre + " no retorno ningun valor ", Fila, Columna);
                                    }
                                }

                            }
                            else
                            {
                                return retorno;

                            }
                        }
                        else
                        {
                            TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se puede acceder al metodo " + metodo.Visibilidad + " " + metodo.Nombre, Fila, Columna);
                            return null;
                        }

                    }
                    else
                    {
                        TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se encontro el metodo " + this.Nombre, this.Fila, this.Columna);
                    }
                }
            }
            else if (this.Tipo.Equals(Constante.TVariableArreglo))
            {
                Variable actual = objeto.TablaVariables.BuscarVariable(this.Nombre);
                if (actual != null)
                {
                    //exite la variable en el ambito actual ahora verificamos si tiene hijos
                    if (this.Hijo != null)
                    {
                        TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se puede acceder a objeto en arreglos, piensa XD", Fila, Columna);
                    }
                    else
                    {
                        //devolvemos el valor de la variable

                        return actual;
                    }
                }
                else
                {
                    TitusNotificaciones.InsertarError(Constante.TErrorSemantico, "No se encontro la variable " + this.Nombre, this.Fila, this.Columna);
                }
            }
            return null;
        }
    }
}
