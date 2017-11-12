using C3DCombiner.Ejecucion;
using C3DCombiner.Funciones;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner
{
    static class GenerarTablaSimbolo3D
    {
        
        public static Object RecorrerArbol(ParseTreeNode Nodo)
        {
            switch (Nodo.Term.Name)
            {
                case Constante.INICIO:
                    {
                        //LISTA_CLASE;
                        List<Simbolo> clases = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[0]);
                        Ambito ambito = new Ambito(Constante.ARCHIVO, clases);


                        Ejecucion3D ArchivoEjecucion = new Ejecucion3D(ambito);
                        return ArchivoEjecucion;
                    }

                case Constante.LISTA_SENTENCIAS:
                    {
                        List<Simbolo> tabla = new List<Simbolo>();
                        if (Nodo.ChildNodes.Count > 0)
                        {
                            tabla = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[0]);
                        }
                        return tabla;
                    }

                case Constante.LISTA_SENTENCIA:
                    {
                        List<Simbolo> tabla = new List<Simbolo>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            foreach (Simbolo sim in (List<Simbolo>)RecorrerArbol(nodo))
                            {
                                tabla.Add(sim);
                            }
                        }
                        return tabla;
                    }

                case Constante.SENTENCIA:
                    {
                        List<Simbolo> lista = new List<Simbolo>();
                        if (Nodo.ChildNodes.Count == 2)
                        {
                            List<Simbolo> cuerpo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);
                            FMetodo metodo = new FMetodo(Constante.TPublico, Constante.TMain, Constante.TMain, new List<Simbolo>(), new Ambito(Constante.TMain, cuerpo), Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                            Simbolo s = new Simbolo(metodo.Visibilidad, metodo.Tipo, metodo.Nombre, Constante.TMain, metodo.Fila, metodo.Columna, metodo.Ambito, metodo);

                            Simbolo anterior = null;

                            foreach (Simbolo sim in metodo.Ambito.TablaSimbolo)
                            {
                                sim.Anterior = anterior;
                                if (anterior != null)
                                {
                                    anterior.Siguiente = sim;
                                    sim.Anterior = anterior;
                                }

                                anterior = sim;
                            }

                            lista.Add(s);
                        }
                        else
                        {
                            List<Simbolo> cuerpo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[2]);
                            FMetodo metodo = new FMetodo(Constante.TPublico, Constante.TVoid, Nodo.ChildNodes[1].Token.ValueString, new List<Simbolo>(), new Ambito(Constante.TMain, cuerpo), Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                            Simbolo s = new Simbolo(metodo.Visibilidad, metodo.Tipo, metodo.Nombre, Constante.TMetodo, metodo.Fila, metodo.Columna, metodo.Ambito, metodo);

                            Simbolo anterior = null;

                            foreach (Simbolo sim in metodo.Ambito.TablaSimbolo)
                            {
                                sim.Anterior = anterior;
                                if (anterior != null)
                                {
                                    anterior.Siguiente = sim;
                                    sim.Anterior = anterior;
                                }

                                anterior = sim;
                            }

                            lista.Add(s);
                        }
                        return lista;
                    }

                case Constante.LISTA_INSTRUCCIONES:
                    {
                        List<Simbolo> tabla = new List<Simbolo>();
                        if (Nodo.ChildNodes.Count > 0)
                        {
                            tabla = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[0]);
                        }
                        return tabla;
                    }

                case Constante.LISTA_INSTRUCCION:
                    {
                        List<Simbolo> tabla = new List<Simbolo>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Simbolo sim = (Simbolo)RecorrerArbol(nodo);

                            tabla.Add(sim);
                        }
                        return tabla;
                    }

                case Constante.INSTRUCCION:
                    {
                        if (Nodo.ChildNodes.Count == 1)
                        {
                            switch (Nodo.ChildNodes[0].Term.Name)
                            {
                                case Constante.Etiqueta:
                                    return new Simbolo(Constante.TPublico, Constante.Etiqueta, Nodo.ChildNodes[0].Token.ValueString, Constante.Etiqueta, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.Etiqueta), null);

                                case Constante.Id:
                                    return new Simbolo(Constante.TPublico, Constante.LLAMADA_METODO, Nodo.ChildNodes[0].Token.ValueString, Constante.LLAMADA_METODO, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.LLAMADA_METODO), null);

                                default:
                                    return RecorrerArbol(Nodo.ChildNodes[0]);
                            }
                        }
                        else if (Nodo.ChildNodes.Count == 2)
                        {
                            if (Nodo.ChildNodes[0].Token.ValueString.Equals(Constante.TGoto))
                            {
                                return new Simbolo(Constante.TPublico, Constante.TGoto, Nodo.ChildNodes[1].Token.ValueString, Constante.TGoto, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.TGoto), null);
                            }
                            else
                            {
                                return new Simbolo(Constante.TPublico, Constante.TError, Nodo.ChildNodes[1].Token.ValueString, Constante.TError, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.TError), null);
                            }
                            
                        }
                        else if (Nodo.ChildNodes.Count == 3)
                        {
                            String tipo = (String)RecorrerArbol(Nodo.ChildNodes[1]);
                            String temp = Nodo.ChildNodes[2].Token.ValueString;
                            FPrint print = new FPrint(tipo, temp);
                            Simbolo sim = new Simbolo(Constante.TPublico, Constante.TPrint, Constante.TPrint, Constante.TPrint, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.TPrint), print);
                            print.Padre = sim;
                            return sim;
                        }
                        else
                        {
                            String Tipo = Constante.TIf;
                            if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.TIfFalse))
                            {
                                Tipo = Constante.TIfFalse;
                            }
                            FNodoExpresion cond = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                            String etq = Nodo.ChildNodes[3].Token.ValueString;

                            FIf si = new FIf(Tipo, cond, etq);
                            Simbolo sim = new Simbolo(Constante.TPublico, Constante.TIf, Constante.TIf, Constante.TIf, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.TIf), si);
                            si.Padre = sim;
                            return sim;
                        }
                    }

                case Constante.ASIGNACION:
                    if (Nodo.ChildNodes.Count == 2)
                    {
                        if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.Temporal))
                        {
                            FNodoExpresion val = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                            FAsignacion3D asi = new FAsignacion3D(Constante.Temporal, null, val, Nodo.ChildNodes[0].Token.ValueString);
                            Simbolo sim = new Simbolo(Constante.TPublico, Constante.ASIGNACION, Constante.ASIGNACION, Constante.ASIGNACION, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.ASIGNACION), asi);
                            
                            return sim;
                        }
                        else
                        {
                            FNodoExpresion val = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                            FAsignacion3D asi = new FAsignacion3D(Nodo.ChildNodes[0].Term.Name, null, val, "");
                            Simbolo sim = new Simbolo(Constante.TPublico, Constante.ASIGNACION, Constante.ASIGNACION, Constante.ASIGNACION, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.ASIGNACION), asi);

                            return sim;
                        }
                    }
                    else
                    {
                        FNodoExpresion acceos = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                        FNodoExpresion val = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[2]);
                        FAsignacion3D asi = new FAsignacion3D(Nodo.ChildNodes[0].Token.ValueString, acceos, val, "");
                        Simbolo sim = new Simbolo(Constante.TPublico, Constante.ASIGNACION, Constante.ASIGNACION, Constante.ASIGNACION, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.ASIGNACION), asi);

                        return sim;
                    }

                case Constante.TPrint:
                    return Nodo.ChildNodes[0].Term.Name;


                case Constante.RELACIONAL:
                    {
                        FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                        FNodoExpresion der = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[2]);
                        return new FNodoExpresion(izq, der, Nodo.ChildNodes[1].Token.ValueString, Nodo.ChildNodes[1].Token.ValueString, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, null);
                    }

                case Constante.ARITMETICA:

                    if (Nodo.ChildNodes.Count == 3)
                    {
                        FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                        FNodoExpresion der = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[2]);
                        return new FNodoExpresion(izq, der, Nodo.ChildNodes[1].Token.ValueString, Nodo.ChildNodes[1].Token.ValueString, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, null);
                    }
                    else
                    {
                        if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.TMenos))
                        {
                            FNodoExpresion der = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                            return new FNodoExpresion(null, der, Nodo.ChildNodes[0].Token.ValueString, Nodo.ChildNodes[0].Token.ValueString, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                        }
                        else if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.THeap))
                        {
                            FNodoExpresion der = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                            return new FNodoExpresion(null, der, Constante.THeap, Constante.THeap, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                        }
                        else
                        {
                            FNodoExpresion der = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                            return new FNodoExpresion(null, der, Constante.TStack, Constante.TStack, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                        }
                    }

                case Constante.EXP:
                    {
                        return RecorrerArbol(Nodo.ChildNodes[0]);
                    }

                case Constante.Entero:
                    return new FNodoExpresion(null, null, Constante.TEntero, Constante.TEntero, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);

                case Constante.Decimal:
                    return new FNodoExpresion(null, null, Constante.TDecimal, Constante.TDecimal, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);

                case Constante.TH:
                    return new FNodoExpresion(null, null, Constante.TH, Constante.TH, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);

                case Constante.TP:
                    return new FNodoExpresion(null, null, Constante.TP, Constante.TP, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);

                case Constante.Temporal:
                    return new FNodoExpresion(null, null, Constante.Temporal, Nodo.Token.ValueString, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);
                                    
            }
            return null;
        }
    }
}
