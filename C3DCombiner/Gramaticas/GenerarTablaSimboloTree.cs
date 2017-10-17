using C3DCombiner.Funciones;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner
{
    static class GenerarTablaSimboloTree
    {

        public static Object RecorrerArbol(ParseTreeNode Nodo)
        {
            switch (Nodo.Term.Name)
            {
                case Constante.INICIO:
                    {
                        //LISTA_IMPORTAR + LISTA_CLASE;
                        Object importar = RecorrerArbol(Nodo.ChildNodes[0]);
                        Object clases = RecorrerArbol(Nodo.ChildNodes[1]);
                    }
                    break;

                case Constante.LISTA_IMPORTAR:
                    {
                        List<String> importar = new List<String>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            foreach (String a in (List<String>)RecorrerArbol(nodo))
                            {
                                importar.Add(a);
                            }
                        }
                        return importar;
                    }

                case Constante.IMPORTAR:
                    {
                        List<String> archivos = new List<String>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            foreach (String a in (List<String>)RecorrerArbol(nodo))
                            {
                                archivos.Add(a);
                            }

                        }
                        return archivos;
                    }

                case Constante.LISTA_ARCHIVO:
                    {
                        List<String> archivos = new List<String>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            String a = (String)RecorrerArbol(nodo);
                            archivos.Add(a);
                        }
                        return archivos;
                    }

                case Constante.ARCHIVO:
                    {
                        String cadena = "";
                        if (Nodo.ChildNodes.Count == 1)
                        {
                            cadena = Nodo.ChildNodes[0].Token.ValueString;
                        }
                        else
                        {
                            cadena = Nodo.ChildNodes[0].Token.ValueString + Nodo.ChildNodes[1].Token.ValueString;
                        }
                        return cadena;
                    }

                case Constante.LISTA_CLASE:
                    {
                        List<FClase> clases = new List<FClase>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.CLASE:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_SENTENCIAS:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_SENTENCIA:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.SENTENCIA:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.VISIBILIDAD:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.FUNCION:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.DECLARACION:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.DIMENSIONES_METODO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_DIMENSION_METODO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.DIMENSION_METODO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_PARAMETROS:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_PARAMETRO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.PARAMETRO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_INSTRUCCIONES:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_INSTRUCCION:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.INSTRUCCION:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.TIPO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_ID:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_DIMENSIONES:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.DIMENSION:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.ASIGNACION:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LLAMADA:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.SI:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_SINOSIS:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_SINOSI:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.SINOSI:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.SINO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;


                case Constante.ELEGIR:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_CASOS:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.CASO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.DEFECTO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.MIENTRAS:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.HACER:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.REPETIR:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.PARA:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LOOP:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LITERALES:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_EXPS:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.LISTA_EXP:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.EXP:
                    {
                        FNodoExpresion nodo = null;
                        if (Nodo.ChildNodes.Count == 1)
                        {
                            nodo = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);

                        }
                        else if (Nodo.ChildNodes.Count == 2)
                        {
                            switch (Nodo.ChildNodes[1].Term.Name)
                            {
                                case Constante.TMenos:
                                    {
                                        FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                                        nodo = new FNodoExpresion(null, izq, Constante.TMenos, Constante.TMenos, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, null);
                                    }
                                    break;

                                case Constante.TAumento:
                                    {
                                        FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                                        nodo = new FNodoExpresion(izq, null, Constante.TAumento, Constante.TAumento, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, null);
                                    }
                                    break;

                                case Constante.TDecremento:
                                    {
                                        FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                                        nodo = new FNodoExpresion(izq, null, Constante.TDecremento, Constante.TDecremento, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, null);
                                    }
                                    break;

                                case Constante.TNot:
                                    {
                                        FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                                        nodo = new FNodoExpresion(null, izq, Constante.TNot, Constante.TNot, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, null);
                                    }
                                    break;

                            }
                        }
                        else if (Nodo.ChildNodes.Count == 3)
                        {
                            FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                            FNodoExpresion der = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                            nodo = new FNodoExpresion(izq, der, Nodo.ChildNodes[2].Term.Name, Nodo.ChildNodes[2].Term.Name, Nodo.ChildNodes[2].Token.Location.Line + 1, Nodo.ChildNodes[2].Token.Location.Column + 1, null);
                        }
                        return nodo;
                    }

                case Constante.OBJETO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.HIJO:
                    {
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Object o = RecorrerArbol(nodo);
                        }
                    }
                    break;

                case Constante.Entero:
                    return new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);


                case Constante.Decimal:
                    return new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);


                case Constante.Caracter:
                    return new FNodoExpresion(null, null, Constante.TCaracter, Constante.TCaracter, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);


                case Constante.Cadena:
                    return new FNodoExpresion(null, null, Constante.TCadena, Constante.TCadena, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);


                case Constante.TFalse:
                    return new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);


                case Constante.TTrue:
                    return new FNodoExpresion(null, null, Constante.TBooleano, Constante.TBooleano, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);



                default:
                    { }
                    return null;

            }
            return null;
        }
    }
}
