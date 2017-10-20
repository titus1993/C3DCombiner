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
                        return RecorrerArbol(Nodo.ChildNodes[0]);
                    }

                case Constante.LISTA_SENTENCIA:
                    {
                        List<Simbolo> tabla = new List<Simbolo>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            tabla.Add((Simbolo)RecorrerArbol(nodo));
                        }
                    }
                    break;

                case Constante.SENTENCIA:
                    {
                        return RecorrerArbol(Nodo.ChildNodes[0]);
                    }

                case Constante.VISIBILIDAD:
                    {
                        if (Nodo.ChildNodes.Count == 1)
                        {
                            return Nodo.ChildNodes[0].Term.Name;
                        }
                        else
                        {
                            return Constante.TPublico;
                        }
                    }

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
                        List<Simbolo> tabla = new List<Simbolo>();
                        String tipo = RecorrerArbol(Nodo.ChildNodes[0]).ToString();
                        if (Nodo.ChildNodes.Count == 2)
                        {

                            foreach (ParseTreeNode tn in Nodo.ChildNodes[1].ChildNodes)
                            {
                                FDeclaracion dec = new FDeclaracion(Constante.TProtegido, tipo, tn.Token.ValueString, new List<FNodoExpresion>(), new Ambito(Constante.DECLARACION), tn.Token.Location.Line + 1, tn.Token.Location.Column, null);
                                Simbolo sim = new Simbolo(dec.Visibilidad, dec.Tipo, dec.Nombre, Constante.DECLARACION, dec.Fila, dec.Columna, dec.Ambito, dec);
                                sim.Tamaño = 1;
                                dec.Padre = sim;
                                tabla.Add(sim);
                            }
                        }
                        else
                        {
                            if (Nodo.ChildNodes[2].Term.Name.Equals(Constante.EXP))
                            {
                                FNodoExpresion val = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[2]);
                                foreach (ParseTreeNode tn in Nodo.ChildNodes[1].ChildNodes)
                                {
                                    FDeclaracion dec = new FDeclaracion(Constante.TProtegido, tipo, tn.Token.ValueString, new List<FNodoExpresion>(), new Ambito(Constante.DECLARACION), tn.Token.Location.Line + 1, tn.Token.Location.Column, val);
                                    Simbolo sim = new Simbolo(dec.Visibilidad, dec.Tipo, dec.Nombre, Constante.DECLARACION, dec.Fila, dec.Columna, dec.Ambito, dec);
                                    sim.Tamaño = 1;
                                    dec.Padre = sim;
                                    tabla.Add(sim);
                                }
                            }
                            else
                            {
                                List<FNodoExpresion> dim = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[2]);
                                FDeclaracion dec = new FDeclaracion(Constante.TProtegido, tipo, Nodo.ChildNodes[1].Token.ValueString, dim, new Ambito(Constante.DECLARACION), Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column, null);
                                Simbolo sim = new Simbolo(dec.Visibilidad, dec.Tipo, dec.Nombre, Constante.DECLARACION, dec.Fila, dec.Columna, dec.Ambito, dec);
                                sim.Tamaño = 1;
                                dec.Padre = sim;
                                tabla.Add(sim);
                            }
                        }
                        return tabla;
                    }

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
                        return RecorrerArbol(Nodo.ChildNodes[0]);
                    }

                case Constante.LISTA_INSTRUCCION:
                    {
                        List<Simbolo> tabla = new List<Simbolo>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Simbolo hermano = null;
                            foreach (Simbolo sim in (List<Simbolo>)RecorrerArbol(nodo))
                            {
                                sim.Hermano = hermano;
                                hermano = sim;
                                tabla.Add(sim);
                            }
                        }
                        return tabla;
                    }

                case Constante.INSTRUCCION:
                    {
                        return RecorrerArbol(Nodo.ChildNodes[0]);
                    }

                case Constante.TIPO:
                    {
                        switch (Nodo.ChildNodes[0].Term.Name)
                        {
                            case Constante.TEntero:
                                return Constante.TEntero;

                            case Constante.TDecimal:
                                return Constante.TEntero;

                            case Constante.TCaracter:
                                return Constante.TEntero;

                            case Constante.TCadena:
                                return Constante.TEntero;

                            case Constante.TBooleano:
                                return Constante.TEntero;

                            default:
                                return Nodo.ChildNodes[0].Token.ValueString;
                        }
                    }

                case Constante.LISTA_DIMENSIONES:
                    {
                        List<FNodoExpresion> d = new List<FNodoExpresion>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            d.Add((FNodoExpresion)RecorrerArbol(nodo));
                        }
                        return d;
                    }

                case Constante.DIMENSION:
                    {
                        return RecorrerArbol(Nodo.ChildNodes[0]);
                    }

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
                        FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                        List<Simbolo> tablasimbolo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[2]);

                        FMientras mientras = new FMientras(condicion, new Ambito(Constante.TMientras, tablasimbolo));
                        Simbolo sim = new Simbolo("", "", "", Constante.TMientras, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, mientras.Ambito, mientras);
                        sim.Posicion = -1;

                        mientras.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>();
                        list.Add(sim);

                        foreach (Simbolo s in tablasimbolo)
                        {
                            s.Padre = sim;
                        }

                        return list;
                    }

                case Constante.HACER:
                    {
                        FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[3]);
                        List<Simbolo> tablasimbolo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);

                        FHacer hacer = new FHacer(condicion, new Ambito(Constante.THacer, tablasimbolo));
                        Simbolo sim = new Simbolo("", "", "", Constante.THacer, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, hacer.Ambito, hacer);
                        sim.Posicion = -1;

                        hacer.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>();
                        list.Add(sim);

                        foreach (Simbolo s in tablasimbolo)
                        {
                            s.Padre = sim;
                        }

                        return list;
                    }

                case Constante.REPETIR:
                    {
                        FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[3]);
                        List<Simbolo> tablasimbolo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);

                        FRepetir repetir = new FRepetir(condicion, new Ambito(Constante.TRepetir, tablasimbolo));
                        Simbolo sim = new Simbolo("", "", "", Constante.TRepetir, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, repetir.Ambito, repetir);
                        sim.Posicion = -1;

                        repetir.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>();
                        list.Add(sim);

                        foreach (Simbolo s in tablasimbolo)
                        {
                            s.Padre = sim;
                        }

                        return list;
                    }

                case Constante.PARA:
                    {
                        List<Simbolo> accionanterior = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);
                        FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[2]);
                        FNodoExpresion accionposterior = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[3]);

                        List<Simbolo> tablasimbolo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[4]);

                        FPara para = new FPara(accionanterior[0], condicion, accionposterior, new Ambito(Constante.TRepetir, tablasimbolo));
                        Simbolo sim = new Simbolo("", "", "", Constante.TPara, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, para.Ambito, para);
                        sim.Posicion = -1;


                        para.AccionAnterior.Padre = sim;
                        condicion.Padre = para.AccionAnterior;
                        accionposterior.Padre = para.AccionAnterior;

                        para.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>();
                        list.Add(sim);

                        foreach (Simbolo s in tablasimbolo)
                        {
                            s.Padre = para.AccionAnterior;
                        }

                        if (para.AccionAnterior.Rol.Equals(Constante.DECLARACION))
                        {
                            para.AccionAnterior.Tamaño = 1;
                            para.AccionAnterior.Posicion = 0;
                            para.Ambito.Tamaño++;
                            sim.Tamaño++;

                            Simbolo Hermano = para.AccionAnterior;
                            foreach (Simbolo s in para.Ambito.TablaSimbolo)
                            {
                                s.Hermano = Hermano;
                                Hermano = s;
                                if (s.Posicion > -1)
                                {
                                    s.Posicion++;
                                }
                            }
                        }

                        return list;
                    }

                case Constante.LOOP:
                    {
                        List<Simbolo> tablasimbolo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);

                        FLoop loop = new FLoop(new Ambito(Constante.TRepetir, tablasimbolo));
                        Simbolo sim = new Simbolo("", "", "", Constante.TLoop, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, loop.Ambito, loop);

                        loop.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>();
                        list.Add(sim);

                        foreach (Simbolo s in tablasimbolo)
                        {
                            s.Padre = sim;
                        }

                        return list;
                    }

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
                        List<FNodoExpresion> le = new List<FNodoExpresion>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            le = (List<FNodoExpresion>)RecorrerArbol(nodo);
                        }
                        return le;
                    }

                case Constante.LISTA_EXP:
                    {
                        List<FNodoExpresion> le = new List<FNodoExpresion>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            le.Add((FNodoExpresion)RecorrerArbol(nodo));
                        }
                        return le;
                    }

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

                                default:
                                    {
                                        switch (Nodo.ChildNodes[0].Term.Name)
                                        {
                                            case Constante.TParseInt:
                                                {
                                                    FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                                    nodo = new FNodoExpresion(null, izq, Constante.TParseInt, Constante.TParseInt, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                                                }
                                                break;

                                            case Constante.TParseDouble:
                                                {
                                                    FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                                    nodo = new FNodoExpresion(null, izq, Constante.TParseDouble, Constante.TParseDouble, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                                                }
                                                break;

                                            case Constante.TIntToStr:
                                                {
                                                    FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                                    nodo = new FNodoExpresion(null, izq, Constante.TIntToStr, Constante.TIntToStr, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                                                }
                                                break;

                                            case Constante.TDoubleToStr:
                                                {
                                                    FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                                    nodo = new FNodoExpresion(null, izq, Constante.TDoubleToStr, Constante.TDoubleToStr, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                                                }
                                                break;

                                            case Constante.TDoubleToInt:
                                                {
                                                    FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                                    nodo = new FNodoExpresion(null, izq, Constante.TDoubleToInt, Constante.TDoubleToInt, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                                                }
                                                break;
                                        }
                                    }
                                    break;

                            }
                        }
                        else if (Nodo.ChildNodes.Count == 3)
                        {
                            if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.TNuevo))
                            {
                                List<FNodoExpresion> p = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[2]);
                                FNuevo fn = new FNuevo(Nodo.ChildNodes[1].Token.ValueString, p, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                nodo = new FNodoExpresion(null, null, Constante.TNuevo, Constante.TNuevo, fn.Fila, fn.Columna, fn);
                            }
                            else
                            {
                                FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                                FNodoExpresion der = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                nodo = new FNodoExpresion(izq, der, Nodo.ChildNodes[2].Term.Name, Nodo.ChildNodes[2].Term.Name, Nodo.ChildNodes[2].Token.Location.Line + 1, Nodo.ChildNodes[2].Token.Location.Column + 1, null);
                            }
                        }
                        return nodo;
                    }

                case Constante.OBJETO:
                    {
                        FLlamadaObjeto lo = null;
                        int i = 0;
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            if (i == 0)
                            {
                                lo = (FLlamadaObjeto)RecorrerArbol(nodo);
                                i++;
                            }
                            else
                            {
                                FLlamadaObjeto loaux = (FLlamadaObjeto)RecorrerArbol(nodo);
                                lo.InsertarHijo(loaux);
                            }
                        }
                        return lo;
                    }

                case Constante.HIJO:
                    {
                        if (Nodo.ChildNodes.Count == 1)
                        {
                            switch (Nodo.ChildNodes[0].Term.Name)
                            {
                                case Constante.TSuper:
                                    return new FLlamadaObjeto(Constante.TSuper, Constante.TSuper, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);

                                case Constante.TSelf:
                                    return new FLlamadaObjeto(Constante.TSelf, Constante.TSelf, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);


                                default:
                                    return new FLlamadaObjeto(Constante.Id, Nodo.ChildNodes[0].Token.ValueString, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);

                            }
                        }
                        else
                        {
                            switch (Nodo.ChildNodes[1].Term.Name)
                            {
                                case Constante.LISTA_DIMENSIONES:
                                    {
                                        if (Nodo.ChildNodes[1].ChildNodes.Count == 1)
                                        {
                                            List<FNodoExpresion> p = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[1]);
                                            FLlamadaMetodoArrelgoTree lma = new FLlamadaMetodoArrelgoTree(Nodo.ChildNodes[0].Token.ValueString, p, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                            return new FLlamadaObjeto(Constante.LLAMADA_METODO_ARREGLO, lma.Nombre, lma.Fila, lma.Columna, lma);
                                        }
                                        else
                                        {
                                            List<FNodoExpresion> d = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[1]);
                                            FLlamadaArreglo la = new FLlamadaArreglo(Nodo.ChildNodes[0].Token.ValueString, d, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                            return new FLlamadaObjeto(Constante.LLAMADA_ARREGLO, la.Nombre, la.Fila, la.Columna, la);
                                        }
                                    }

                                case Constante.LISTA_EXPS:
                                    {
                                        List<FNodoExpresion> p = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[1]);
                                        FLlamadaMetodo lm = new FLlamadaMetodo(Nodo.ChildNodes[0].Token.ValueString, p, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                        return new FLlamadaObjeto(Constante.LLAMADA_METODO, lm.Nombre, lm.Fila, lm.Columna, lm);
                                    }
                            }
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

                case Constante.TSelf:
                    return new FNodoExpresion(null, null, Constante.TSelf, Constante.TSelf, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, "");

                case Constante.LLAMADA_EXP:
                    {
                        FLlamadaObjeto lo = null;
                        if (Nodo.ChildNodes.Count == 1)
                        {
                            lo = (FLlamadaObjeto)RecorrerArbol(Nodo.ChildNodes[0]);
                        }
                        else if (Nodo.ChildNodes.Count == 2)
                        {
                            switch (Nodo.ChildNodes[1].Term.Name)
                            {
                                case Constante.Id:
                                    {
                                        FLlamadaObjeto loaux = (FLlamadaObjeto)RecorrerArbol(Nodo.ChildNodes[0]);
                                        FLlamadaObjeto idaux = new FLlamadaObjeto(Constante.Id, Nodo.ChildNodes[1].Token.ValueString, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, Nodo.ChildNodes[1].Token.ValueString);
                                        loaux.InsertarHijo(idaux);
                                        lo = loaux;
                                    }
                                    break;

                                case Constante.LISTA_DIMENSIONES:
                                    {
                                        if (Nodo.ChildNodes[1].ChildNodes.Count == 1)
                                        {
                                            List<FNodoExpresion> p = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[1]);
                                            FLlamadaMetodoArrelgoTree lma = new FLlamadaMetodoArrelgoTree(Nodo.ChildNodes[0].Token.ValueString, p, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                            lo = new FLlamadaObjeto(Constante.LLAMADA_METODO_ARREGLO, lma.Nombre, lma.Fila, lma.Columna, lma);
                                        }
                                        else
                                        {
                                            List<FNodoExpresion> d = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[1]);
                                            FLlamadaArreglo la = new FLlamadaArreglo(Nodo.ChildNodes[0].Token.ValueString, d, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                            lo = new FLlamadaObjeto(Constante.LLAMADA_ARREGLO, la.Nombre, la.Fila, la.Columna, la);
                                        }
                                    }
                                    break;

                                case Constante.LISTA_EXPS:
                                    {
                                        List<FNodoExpresion> p = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[1]);
                                        FLlamadaMetodo lm = new FLlamadaMetodo(Nodo.ChildNodes[0].Token.ValueString, p, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                        lo = new FLlamadaObjeto(Constante.LLAMADA_METODO, lm.Nombre, lm.Fila, lm.Columna, lm);
                                    }
                                    break;
                            }
                        }
                        else if (Nodo.ChildNodes.Count == 3)//si tienen objeto la llamada arreglo y metodo
                        {
                            switch (Nodo.ChildNodes[2].Term.Name)
                            {
                                case Constante.LISTA_DIMENSIONES:
                                    {
                                        FLlamadaObjeto loaux = (FLlamadaObjeto)RecorrerArbol(Nodo.ChildNodes[0]);
                                        if (Nodo.ChildNodes[2].ChildNodes.Count == 1)
                                        {
                                            List<FNodoExpresion> p = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[2]);
                                            FLlamadaMetodoArrelgoTree lma = new FLlamadaMetodoArrelgoTree(Nodo.ChildNodes[1].Token.ValueString, p, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1);
                                            FLlamadaObjeto lmaaux = new FLlamadaObjeto(Constante.LLAMADA_METODO_ARREGLO, lma.Nombre, lma.Fila, lma.Columna, lma);
                                            loaux.InsertarHijo(lmaaux);
                                            lo = loaux;
                                        }
                                        else
                                        {
                                            List<FNodoExpresion> d = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[2]);
                                            FLlamadaArreglo la = new FLlamadaArreglo(Nodo.ChildNodes[1].Token.ValueString, d, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1);
                                            FLlamadaObjeto laaux = new FLlamadaObjeto(Constante.LLAMADA_ARREGLO, la.Nombre, la.Fila, la.Columna, la);
                                            loaux.InsertarHijo(laaux);
                                            lo = loaux;
                                        }
                                    }
                                    break;

                                case Constante.LISTA_EXPS:
                                    {
                                        FLlamadaObjeto loaux = (FLlamadaObjeto)RecorrerArbol(Nodo.ChildNodes[0]);
                                        List<FNodoExpresion> p = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[2]);
                                        FLlamadaMetodo lm = new FLlamadaMetodo(Nodo.ChildNodes[1].Token.ValueString, p, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1);
                                        FLlamadaObjeto lmaux = new FLlamadaObjeto(Constante.LLAMADA_METODO, lm.Nombre, lm.Fila, lm.Columna, lm);
                                        loaux.InsertarHijo(lmaux);
                                        lo = loaux;
                                    }
                                    break;
                            }
                        }
                        return new FNodoExpresion(null, null, Constante.LLAMADA_OBJETO, Constante.LLAMADA_OBJETO, lo.Fila, lo.Columna, lo);
                    }

                case Constante.Id:
                    return new FLlamadaObjeto(Constante.Id, Nodo.Token.ValueString, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, Nodo.Token.ValueString);

                case Constante.TSuper:
                    return new FLlamadaObjeto(Constante.TSuper, Constante.TSuper, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, null);

            }
            return null;
        }
    }
}
