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
        public static List<String> Rutas = new List<String>();
        public static Object RecorrerArbol(ParseTreeNode Nodo)
        {
            switch (Nodo.Term.Name)
            {
                case Constante.INICIO:
                    {
                        //LISTA_IMPORTAR + LISTA_CLASE;
                        List<String> importar = (List<String>)RecorrerArbol(Nodo.ChildNodes[0]);
                        List<Simbolo> clases = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);
                        Ambito ambito = new Ambito(Constante.ARCHIVO, clases);

                        Archivo archivo = new Archivo(importar, ambito, GenerarTablaSimboloTree.Rutas[GenerarTablaSimboloTree.Rutas.Count - 1]);
                        return archivo;
                    }

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
                        List<Simbolo> tabla = new List<Simbolo>();
                        Simbolo hermano = null;
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Simbolo sim = (Simbolo)RecorrerArbol(nodo);
                            sim.Hermano = hermano;
                            hermano = sim;
                            tabla.Add(sim);
                        }
                        return tabla;
                    }

                case Constante.CLASE:
                    {
                        if (Nodo.ChildNodes.Count == 3)
                        {
                            List<Simbolo> lista = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[2]);
                            FClase clase = new FClase(Nodo.ChildNodes[1].Token.ValueString, "", new Ambito(Nodo.ChildNodes[1].Token.ValueString, lista));
                            return new Simbolo(Constante.TPublico, Constante.TVoid, clase.Nombre, Constante.TClase, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, clase.Ambito, clase);
                        }
                        else
                        {
                            List<Simbolo> lista = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[3]);
                            FClase clase = new FClase(Nodo.ChildNodes[1].Token.ValueString, Nodo.ChildNodes[2].Token.ValueString, new Ambito(Nodo.ChildNodes[1].Token.ValueString, lista));
                            return new Simbolo(Constante.TPublico, Constante.TVoid, clase.Nombre, Constante.TClase, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, clase.Ambito, clase);
                        }
                    }

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
                        return tabla;
                    }

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
                        if (Nodo.ChildNodes[1].Term.Name.Equals(Constante.TMetodo) || Nodo.ChildNodes[2].Term.Name.Equals(Constante.TMetodo))
                        {
                            if (Nodo.ChildNodes.Count == 5)
                            {
                                String visibilidad = (String)RecorrerArbol(Nodo.ChildNodes[0]);
                                String id = Nodo.ChildNodes[2].Token.ValueString;
                                List<Simbolo> parametros = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[3]);
                                List<Simbolo> cuerpo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[4]);

                                FMetodo metodo = new FMetodo(visibilidad, Constante.TVoid, id, parametros, new Ambito(id, cuerpo), Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1);

                                Simbolo m = new Simbolo(metodo.Visibilidad, metodo.Tipo, metodo.Nombre, Constante.TMetodo, metodo.Fila, metodo.Columna, metodo.Ambito, metodo);

                                metodo.Padre = m;

                                if (parametros.Count > 0)
                                {
                                    Simbolo Hermano = null;
                                    foreach (Simbolo s in parametros)
                                    {
                                        s.Padre = m;
                                        ((FParametro)s.Valor).Padre = m;
                                        Hermano = s;
                                    }

                                    if (metodo.Ambito.TablaSimbolo.Count > 0)
                                    {
                                        metodo.Ambito.TablaSimbolo[0].Hermano = Hermano;
                                    }
                                }


                                foreach (Simbolo s in metodo.Ambito.TablaSimbolo)
                                {
                                    s.Padre = m;
                                }
                                return m;
                            }
                            else
                            {
                                String visibilidad = (String)RecorrerArbol(Nodo.ChildNodes[1]);
                                String id = Nodo.ChildNodes[3].Token.ValueString;
                                List<Simbolo> parametros = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[4]);
                                List<Simbolo> cuerpo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[5]);

                                FMetodo metodo = new FMetodo(visibilidad, Constante.TVoid, id, parametros, new Ambito(id, cuerpo), Nodo.ChildNodes[2].Token.Location.Line + 1, Nodo.ChildNodes[2].Token.Location.Column + 1);

                                Simbolo m = new Simbolo(metodo.Visibilidad, metodo.Tipo, metodo.Nombre, Constante.TMetodo, metodo.Fila, metodo.Columna, metodo.Ambito, metodo);

                                metodo.Padre = m;

                                if (parametros.Count > 0)
                                {
                                    Simbolo Hermano = null;
                                    foreach (Simbolo s in parametros)
                                    {
                                        s.Padre = m;
                                        ((FParametro)s.Valor).Padre = m;
                                        Hermano = s;
                                    }

                                    if (metodo.Ambito.TablaSimbolo.Count > 0)
                                    {
                                        metodo.Ambito.TablaSimbolo[0].Hermano = Hermano;
                                    }
                                }


                                foreach (Simbolo s in metodo.Ambito.TablaSimbolo)
                                {
                                    s.Padre = m;
                                }
                                return m;
                            }
                        }
                        else if (Nodo.ChildNodes[1].Term.Name.Equals(Constante.TFuncion) || Nodo.ChildNodes[2].Term.Name.Equals(Constante.TFuncion))
                        {
                            if (Nodo.ChildNodes.Count == 7)
                            {
                                String visibilidad = (String)RecorrerArbol(Nodo.ChildNodes[0]);
                                String tipo = (String)RecorrerArbol(Nodo.ChildNodes[2]);
                                int dimensiones = (int)RecorrerArbol(Nodo.ChildNodes[3]);
                                String id = Nodo.ChildNodes[4].Token.ValueString;
                                List<Simbolo> parametros = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[5]);
                                List<Simbolo> cuerpo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[6]);

                                FMetodo metodo = new FMetodo(visibilidad, tipo, dimensiones, id, parametros, new Ambito(id, cuerpo), Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1);

                                Simbolo m = new Simbolo(metodo.Visibilidad, metodo.Tipo, metodo.Nombre, Constante.TMetodo, metodo.Fila, metodo.Columna, metodo.Ambito, metodo);

                                metodo.Padre = m;

                                if (parametros.Count > 0)
                                {
                                    Simbolo Hermano = null;
                                    foreach (Simbolo s in parametros)
                                    {
                                        s.Padre = m;
                                        ((FParametro)s.Valor).Padre = m;
                                        Hermano = s;
                                    }

                                    if (metodo.Ambito.TablaSimbolo.Count > 0)
                                    {
                                        metodo.Ambito.TablaSimbolo[0].Hermano = Hermano;
                                    }
                                }


                                foreach (Simbolo s in metodo.Ambito.TablaSimbolo)
                                {
                                    s.Padre = m;
                                }
                                return m;
                            }
                            else
                            {
                                String visibilidad = (String)RecorrerArbol(Nodo.ChildNodes[1]);
                                String tipo = (String)RecorrerArbol(Nodo.ChildNodes[3]);
                                int dimensiones = (int)RecorrerArbol(Nodo.ChildNodes[4]);
                                String id = Nodo.ChildNodes[5].Token.ValueString;
                                List<Simbolo> parametros = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[6]);
                                List<Simbolo> cuerpo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[7]);

                                FMetodo metodo = new FMetodo(visibilidad, tipo, dimensiones, id, parametros, new Ambito(id, cuerpo), Nodo.ChildNodes[2].Token.Location.Line + 1, Nodo.ChildNodes[2].Token.Location.Column + 1);

                                Simbolo m = new Simbolo(metodo.Visibilidad, metodo.Tipo, metodo.Nombre, Constante.TMetodo, metodo.Fila, metodo.Columna, metodo.Ambito, metodo);

                                metodo.Padre = m;

                                if (parametros.Count > 0)
                                {
                                    Simbolo Hermano = null;
                                    foreach (Simbolo s in parametros)
                                    {
                                        s.Padre = m;
                                        ((FParametro)s.Valor).Padre = m;
                                        Hermano = s;
                                    }

                                    if (metodo.Ambito.TablaSimbolo.Count > 0)
                                    {
                                        metodo.Ambito.TablaSimbolo[0].Hermano = Hermano;
                                    }
                                }


                                foreach (Simbolo s in metodo.Ambito.TablaSimbolo)
                                {
                                    s.Padre = m;
                                }
                                return m;
                            }
                        }
                        else//constructor
                        {
                            List<Simbolo> parametros = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);
                            List<Simbolo> cuerpo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[2]);

                            FMetodo metodo = new FMetodo(Constante.TVoid, Constante.TVoid, Constante.TConstructor, parametros, new Ambito(Constante.TConstructor, cuerpo), Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);

                            Simbolo m = new Simbolo(metodo.Visibilidad, metodo.Tipo, metodo.Nombre, Constante.TConstructor, metodo.Fila, metodo.Columna, metodo.Ambito, metodo);

                            metodo.Padre = m;

                            if (parametros.Count > 0)
                            {
                                Simbolo Hermano = null;
                                foreach (Simbolo s in parametros)
                                {
                                    s.Padre = m;
                                    ((FParametro)s.Valor).Padre = m;
                                    Hermano = s;
                                }

                                if (metodo.Ambito.TablaSimbolo.Count > 0)
                                {
                                    metodo.Ambito.TablaSimbolo[0].Hermano = Hermano;
                                }
                            }


                            foreach (Simbolo s in metodo.Ambito.TablaSimbolo)
                            {
                                s.Padre = m;
                            }
                            return m;
                        }
                    }

                case Constante.DECLARACION:
                    {
                        List<Simbolo> tabla = new List<Simbolo>();
                        String tipo = RecorrerArbol(Nodo.ChildNodes[0]).ToString();
                        if (Nodo.ChildNodes.Count == 2)
                        {

                            foreach (ParseTreeNode tn in Nodo.ChildNodes[1].ChildNodes)
                            {
                                FDeclaracion dec = new FDeclaracion(Constante.TProtegido, tipo, tn.Token.ValueString, new List<FNodoExpresion>(), new Ambito(Constante.DECLARACION), tn.Token.Location.Line + 1, tn.Token.Location.Column, null);
                                Simbolo sim = new Simbolo(dec.Visibilidad, dec.Tipo, dec.Nombre, Constante.DECLARACION, dec.Fila, dec.Columna, dec.Ambito, dec)
                                {
                                    Tamaño = 1
                                };
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
                                    Simbolo sim = new Simbolo(dec.Visibilidad, dec.Tipo, dec.Nombre, Constante.DECLARACION, dec.Fila, dec.Columna, dec.Ambito, dec)
                                    {
                                        Tamaño = 1
                                    };
                                    dec.Padre = sim;
                                    tabla.Add(sim);
                                }
                            }
                            else
                            {
                                List<FNodoExpresion> dim = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[2]);
                                FDeclaracion dec = new FDeclaracion(Constante.TProtegido, tipo, Nodo.ChildNodes[1].Token.ValueString, dim, new Ambito(Constante.DECLARACION), Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column, null);
                                Simbolo sim = new Simbolo(dec.Visibilidad, dec.Tipo, dec.Nombre, Constante.DECLARACION, dec.Fila, dec.Columna, dec.Ambito, dec)
                                {
                                    Tamaño = 1
                                };
                                dec.Padre = sim;
                                tabla.Add(sim);
                            }
                        }
                        return tabla;
                    }

                case Constante.DIMENSIONES_METODO:
                    {
                        if (Nodo.ChildNodes.Count == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return Nodo.ChildNodes[0].ChildNodes.Count;
                        }
                    }

                case Constante.LISTA_PARAMETROS:
                    {
                        List<Simbolo> lista = new List<Simbolo>();

                        if (Nodo.ChildNodes.Count != 0)
                        {
                            lista = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[0]);
                        }
                        return lista;
                    }

                case Constante.LISTA_PARAMETRO:
                    {
                        List<Simbolo> lista = new List<Simbolo>();
                        Simbolo hermano = null;
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            Simbolo s = (Simbolo)RecorrerArbol(nodo);
                            s.Hermano = hermano;
                            hermano = s;
                            lista.Add(s);

                        }
                        return lista;
                    }

                case Constante.PARAMETRO:
                    {
                        String tipo = (String)RecorrerArbol(Nodo.ChildNodes[0]);
                        int dimensiones = (int)RecorrerArbol(Nodo.ChildNodes[1]);
                        FParametro p = new FParametro(tipo, Nodo.ChildNodes[2].Token.ValueString, dimensiones, Nodo.ChildNodes[2].Token.Location.Line + 1, Nodo.ChildNodes[2].Token.Location.Column + 1);
                        Simbolo s = new Simbolo(Constante.TProtegido, p.Tipo, p.Nombre, Constante.PARAMETRO, Nodo.ChildNodes[2].Token.Location.Line + 1, Nodo.ChildNodes[2].Token.Location.Column + 1, new Ambito(Constante.PARAMETRO), p);
                        p.Padre = s;
                        return s;
                    }

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
                        if (Nodo.ChildNodes.Count == 1)
                        {

                            return RecorrerArbol(Nodo.ChildNodes[0]);
                        }
                        else
                        {
                            FNodoExpresion valor = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                            List<Simbolo> lista = new List<Simbolo>
                            {
                                new Simbolo(Constante.TProtegido, "", "", Constante.TRetorno, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.TRetorno), valor)
                            };
                            return lista;
                        }
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
                        FAsignacion asigna = null;
                        int Fila, Columna;
                        if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.Id))
                        {
                            Fila = Nodo.ChildNodes[0].Token.Location.Line;
                            Columna = Nodo.ChildNodes[0].Token.Location.Column;
                            if (Nodo.ChildNodes[1].Term.Name.Equals(Constante.LISTA_DIMENSIONES))
                            {
                                //asig arreglo
                                List<FNodoExpresion> d = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[1]);
                                FLlamadaArreglo la = new FLlamadaArreglo(Nodo.ChildNodes[0].Token.ValueString, d, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                FLlamadaObjeto lo = new FLlamadaObjeto(Constante.LLAMADA_ARREGLO, la.Nombre, la.Fila, la.Columna, la);


                                FNodoExpresion val = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[2]);

                                asigna = new FAsignacion(Constante.LLAMADA_OBJETO, new Ambito(Constante.ASIGNACION), val, lo);

                            }
                            else
                            {
                                //asig variable
                                FLlamadaObjeto lo = (FLlamadaObjeto)RecorrerArbol(Nodo.ChildNodes[0]);

                                FNodoExpresion val = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);

                                asigna = new FAsignacion(Constante.LLAMADA_OBJETO, new Ambito(Constante.ASIGNACION), val, lo);
                            }
                        }
                        else if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.OBJETO))
                        {
                            Fila = Nodo.ChildNodes[1].Token.Location.Line;
                            Columna = Nodo.ChildNodes[1].Token.Location.Column;
                            //objeto
                            if (Nodo.ChildNodes[2].Term.Name.Equals(Constante.LISTA_DIMENSIONES))
                            {
                                //asig arreglo
                                FLlamadaObjeto loaux = (FLlamadaObjeto)RecorrerArbol(Nodo.ChildNodes[0]);

                                List<FNodoExpresion> d = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[2]);
                                FLlamadaArreglo la = new FLlamadaArreglo(Nodo.ChildNodes[1].Token.ValueString, d, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1);
                                FLlamadaObjeto laaux = new FLlamadaObjeto(Constante.LLAMADA_ARREGLO, la.Nombre, la.Fila, la.Columna, la);
                                loaux.InsertarHijo(laaux);
                                FLlamadaObjeto lo = loaux;

                                FNodoExpresion val = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[3]);

                                asigna = new FAsignacion(Constante.LLAMADA_OBJETO, new Ambito(Constante.ASIGNACION), val, lo);

                            }
                            else
                            {
                                //asig variable
                                FLlamadaObjeto loaux = (FLlamadaObjeto)RecorrerArbol(Nodo.ChildNodes[0]);
                                FLlamadaObjeto idaux = new FLlamadaObjeto(Constante.Id, Nodo.ChildNodes[1].Token.ValueString, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, Nodo.ChildNodes[1].Token.ValueString);
                                loaux.InsertarHijo(idaux);
                                FLlamadaObjeto lo = loaux;

                                FNodoExpresion val = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[2]);

                                asigna = new FAsignacion(Constante.LLAMADA_OBJETO, new Ambito(Constante.ASIGNACION), val, lo);
                            }
                        }
                        else
                        {
                            Fila = Nodo.ChildNodes[1].Token.Location.Line;
                            Columna = Nodo.ChildNodes[1].Token.Location.Column;
                            //++ o --
                            FNodoExpresion objeto = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);

                            FNodoExpresion val = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[2]);

                            asigna = new FAsignacion(Nodo.ChildNodes[1].Term.Name, new Ambito(Constante.ASIGNACION), val, val);
                        }

                        Simbolo sim = new Simbolo(Constante.TProtegido, "", "", Constante.ASIGNACION, Fila, Columna, asigna.Ambito, asigna);

                        asigna.Padre = sim;
                        asigna.Operacion.Padre = sim;
                        asigna.Valor.Padre = sim;

                        List<Simbolo> list = new List<Simbolo>
                        {
                            sim
                        };
                        return list;
                    }

                case Constante.LLAMADA:
                    {
                        List<Simbolo> tabla = new List<Simbolo>();
                        if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.Id))
                        {
                            List<FNodoExpresion> lista = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[1]);
                            if (Nodo.ChildNodes[1].Term.Name.Equals(Constante.LISTA_EXPS))
                            {
                                FLlamadaMetodo lm = new FLlamadaMetodo(Nodo.ChildNodes[0].Token.ValueString, lista, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                FLlamadaObjeto lo = new FLlamadaObjeto(Constante.LLAMADA_METODO, lm.Nombre, lm.Fila, lm.Columna, lm);

                                Simbolo llamada = new Simbolo(Constante.TProtegido, "", "", Constante.LLAMADA_METODO, lo.Fila, lo.Columna, new Ambito(Constante.LLAMADA_METODO), lo);
                                tabla.Add(llamada);
                            }
                            else
                            {
                                if (lista.Count == 1)
                                {
                                    FLlamadaMetodo lm = new FLlamadaMetodo(Nodo.ChildNodes[0].Token.ValueString, lista, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                    FLlamadaObjeto lo = new FLlamadaObjeto(Constante.LLAMADA_METODO, lm.Nombre, lm.Fila, lm.Columna, lm);

                                    Simbolo llamada = new Simbolo(Constante.TProtegido, "", "", Constante.LLAMADA_METODO, lo.Fila, lo.Columna, new Ambito(Constante.LLAMADA_METODO), lo);
                                    tabla.Add(llamada);
                                }
                                else
                                {
                                    TitusTools.InsertarError(Constante.TErrorSintactico, "No se puede llamar a un arreglo", GenerarTablaSimboloTree.Rutas[GenerarTablaSimboloTree.Rutas.Count - 1].ToString(), Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1);
                                }
                            }
                        }
                        else if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.OBJETO))
                        {
                            List<FNodoExpresion> lista = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[2]);
                            if (Nodo.ChildNodes[2].Term.Name.Equals(Constante.LISTA_EXPS))
                            {
                                FLlamadaObjeto loaux = (FLlamadaObjeto)RecorrerArbol(Nodo.ChildNodes[0]);
                                FLlamadaMetodo lm = new FLlamadaMetodo(Nodo.ChildNodes[1].Token.ValueString, lista, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1);
                                FLlamadaObjeto lo = new FLlamadaObjeto(Constante.LLAMADA_METODO, lm.Nombre, lm.Fila, lm.Columna, lm);

                                loaux.InsertarHijo(lo);

                                Simbolo llamada = new Simbolo(Constante.TProtegido, "", "", Constante.LLAMADA_METODO, loaux.Fila, loaux.Columna, new Ambito(Constante.LLAMADA_METODO), loaux);
                                tabla.Add(llamada);
                            }
                            else
                            {
                                if (lista.Count == 1)
                                {
                                    FLlamadaObjeto loaux = (FLlamadaObjeto)RecorrerArbol(Nodo.ChildNodes[0]);
                                    FLlamadaMetodo lm = new FLlamadaMetodo(Nodo.ChildNodes[1].Token.ValueString, lista, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1);
                                    FLlamadaObjeto lo = new FLlamadaObjeto(Constante.LLAMADA_METODO, lm.Nombre, lm.Fila, lm.Columna, lm);

                                    loaux.InsertarHijo(lo);

                                    Simbolo llamada = new Simbolo(Constante.TProtegido, "", "", Constante.LLAMADA_METODO, loaux.Fila, loaux.Columna, new Ambito(Constante.LLAMADA_METODO), loaux);
                                    tabla.Add(llamada);
                                }
                                else
                                {
                                    TitusTools.InsertarError(Constante.TErrorSintactico, "No se puede llamar a un arreglo", GenerarTablaSimboloTree.Rutas[GenerarTablaSimboloTree.Rutas.Count - 1].ToString(), Nodo.ChildNodes[0].Token.Location.Line, Nodo.ChildNodes[0].Token.Location.Column);
                                }
                            }
                        }
                        else if (Nodo.ChildNodes[0].Term.Name.Equals(Constante.TOutString))
                        {
                            FNodoExpresion valor = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                            FImprimir imprimir = new FImprimir(valor);

                            Simbolo llamada = new Simbolo(Constante.TProtegido, "", "", Constante.TImprimir, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.TImprimir), imprimir);
                            imprimir.Padre = llamada;
                            imprimir.Valor.Padre = llamada;
                            tabla.Add(llamada);
                        }
                        else
                        {
                            List<FNodoExpresion> parametros = (List<FNodoExpresion>)RecorrerArbol(Nodo.ChildNodes[1]);

                            FSuper super = new FSuper(parametros);

                            Simbolo s = new Simbolo(Constante.TProtegido, "", "", Constante.TSuper, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, new Ambito(Constante.TSuper), super);
                            super.Padre = s;

                            foreach (FNodoExpresion n in super.Parametros)
                            {
                                n.Padre = s;
                            }

                            tabla.Add(s);
                        }

                        return tabla;
                    }

                case Constante.SI:
                    {
                        FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                        List<Simbolo> tablasimbolo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[2]);
                        List<FSinoSi> sinosi = (List<FSinoSi>)RecorrerArbol(Nodo.ChildNodes[3]);
                        FSino sino = (FSino)RecorrerArbol(Nodo.ChildNodes[4]);

                        FSi si = new FSi(condicion, new Ambito(Constante.TSi, tablasimbolo), sinosi, sino);
                        Simbolo sim = new Simbolo("", "", "", Constante.TSi, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, si.Ambito, si);


                        si.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>
                        {
                            sim
                        };
                        condicion.Padre = sim;
                        //asignamos el padre del ambito de si
                        foreach (Simbolo s in tablasimbolo)
                        {
                            s.Padre = sim;
                        }

                        //asignamos el padre a los ambitos de sinosi
                        foreach (FSinoSi fsns in sinosi)
                        {
                            fsns.Condicion.Padre = sim;
                            fsns.Padre = sim;
                            foreach (Simbolo s in fsns.Ambito.TablaSimbolo)
                            {
                                s.Padre = sim;
                            }
                        }

                        foreach (Simbolo s in sino.Ambito.TablaSimbolo)
                        {
                            sino.Padre = sim;
                            s.Padre = sim;
                        }

                        return list;

                    }

                case Constante.LISTA_SINOSIS:
                    {
                        return RecorrerArbol(Nodo.ChildNodes[0]);
                    }

                case Constante.LISTA_SINOSI:
                    {
                        List<FSinoSi> tabla = new List<FSinoSi>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            tabla.Add((FSinoSi)RecorrerArbol(nodo));
                        }
                        return tabla;
                    }

                case Constante.SINOSI:
                    {
                        FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                        List<Simbolo> tablasimbolo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[2]);

                        FSinoSi sinosi = new FSinoSi(condicion, new Ambito(Constante.TSinoSi, tablasimbolo));
                        return sinosi;
                    }

                case Constante.SINO:
                    {
                        List<Simbolo> tablasimbolo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);

                        FSino sino = new FSino(new Ambito(Constante.TSinoSi, tablasimbolo));
                        return sino;
                    }

                case Constante.ELEGIR:
                    {
                        FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                        List<FCaso> casos = (List<FCaso>)RecorrerArbol(Nodo.ChildNodes[2]);
                        FCaso defecto = (FCaso)RecorrerArbol(Nodo.ChildNodes[3]);


                        FElegir elegir = new FElegir(condicion, new Ambito(Constante.TElegir), casos, defecto);
                        Simbolo sim = new Simbolo("", "", "", Constante.TElegir, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, elegir.Ambito, elegir);


                        elegir.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>
                        {
                            sim
                        };
                        foreach (FCaso cas in casos)
                        {
                            cas.Padre = sim;
                            foreach (Simbolo sm in cas.Ambito.TablaSimbolo)
                            {
                                sm.Padre = sim;
                            }
                        }

                        if (defecto != null)
                        {
                            defecto.Padre = sim;
                            foreach (Simbolo sm in defecto.Ambito.TablaSimbolo)
                            {
                                sm.Padre = sim;
                            }
                        }

                        return list;
                    }

                case Constante.LISTA_CASOS:
                    {
                        List<FCaso> lcasos = new List<FCaso>();
                        foreach (ParseTreeNode nodo in Nodo.ChildNodes)
                        {
                            lcasos.Add((FCaso)RecorrerArbol(nodo));
                        }
                        return lcasos;
                    }

                case Constante.CASO:
                    {
                        FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                        List<Simbolo> tabla = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);
                        FCaso caso = new FCaso(condicion, new Ambito(Constante.CASO, tabla));
                        return caso;
                    }

                case Constante.DEFECTO:
                    {
                        if (Nodo.ChildNodes.Count > 0)
                        {

                            FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                            List<Simbolo> tabla = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[1]);
                            FCaso caso = new FCaso(new Ambito(Constante.CASO, tabla));
                            return caso;
                        }
                        else
                        {
                            return null;
                        }
                    }

                case Constante.MIENTRAS:
                    {
                        FNodoExpresion condicion = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                        List<Simbolo> tablasimbolo = (List<Simbolo>)RecorrerArbol(Nodo.ChildNodes[2]);

                        FMientras mientras = new FMientras(condicion, new Ambito(Constante.TMientras, tablasimbolo));
                        Simbolo sim = new Simbolo("", "", "", Constante.TMientras, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, mientras.Ambito, mientras);


                        mientras.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>
                        {
                            sim
                        };
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


                        hacer.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>
                        {
                            sim
                        };
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


                        repetir.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>
                        {
                            sim
                        };
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



                        para.AccionAnterior.Padre = sim;
                        condicion.Padre = para.AccionAnterior;
                        accionposterior.Padre = para.AccionAnterior;

                        para.Padre = sim;
                        List<Simbolo> list = new List<Simbolo>
                        {
                            sim
                        };
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
                        List<Simbolo> list = new List<Simbolo>
                        {
                            sim
                        };
                        foreach (Simbolo s in tablasimbolo)
                        {
                            s.Padre = sim;
                        }

                        return list;
                    }

                case Constante.LITERALES:
                    {
                        return RecorrerArbol(Nodo.ChildNodes[0]);
                    }

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
                                        izq.Hermano = nodo;
                                    }
                                    break;

                                case Constante.TAumento:
                                    {
                                        FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                                        nodo = new FNodoExpresion(izq, null, Constante.TAumento, Constante.TAumento, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, null);
                                        izq.Hermano = nodo;
                                    }
                                    break;

                                case Constante.TDecremento:
                                    {
                                        FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                                        nodo = new FNodoExpresion(izq, null, Constante.TDecremento, Constante.TDecremento, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, null);
                                        izq.Hermano = nodo;
                                    }
                                    break;

                                case Constante.TNot:
                                    {
                                        FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                                        nodo = new FNodoExpresion(null, izq, Constante.TNot, Constante.TNot, Nodo.ChildNodes[1].Token.Location.Line + 1, Nodo.ChildNodes[1].Token.Location.Column + 1, null);
                                        izq.Hermano = nodo;
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
                                                    izq.Hermano = nodo;
                                                }
                                                break;

                                            case Constante.TParseDouble:
                                                {
                                                    FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                                    nodo = new FNodoExpresion(null, izq, Constante.TParseDouble, Constante.TParseDouble, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                                                    izq.Hermano = nodo;
                                                }
                                                break;

                                            case Constante.TIntToStr:
                                                {
                                                    FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                                    nodo = new FNodoExpresion(null, izq, Constante.TIntToStr, Constante.TIntToStr, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                                                    izq.Hermano = nodo;
                                                }
                                                break;

                                            case Constante.TDoubleToStr:
                                                {
                                                    FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                                    nodo = new FNodoExpresion(null, izq, Constante.TDoubleToStr, Constante.TDoubleToStr, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                                                    izq.Hermano = nodo;
                                                }
                                                break;

                                            case Constante.TDoubleToInt:
                                                {
                                                    FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                                    nodo = new FNodoExpresion(null, izq, Constante.TDoubleToInt, Constante.TDoubleToInt, Nodo.ChildNodes[0].Token.Location.Line + 1, Nodo.ChildNodes[0].Token.Location.Column + 1, null);
                                                    izq.Hermano = nodo;
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
                                foreach (FNodoExpresion n in p)
                                {
                                    n.Hermano = nodo;
                                }
                            }
                            else
                            {
                                FNodoExpresion izq = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[0]);
                                FNodoExpresion der = (FNodoExpresion)RecorrerArbol(Nodo.ChildNodes[1]);
                                nodo = new FNodoExpresion(izq, der, Nodo.ChildNodes[2].Term.Name, Nodo.ChildNodes[2].Term.Name, Nodo.ChildNodes[2].Token.Location.Line + 1, Nodo.ChildNodes[2].Token.Location.Column + 1, null);
                                izq.Hermano = nodo;
                                der.Hermano = nodo;
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

                case Constante.TContinuar:
                    {
                        List<Simbolo> lista = new List<Simbolo>
                        {
                            new Simbolo(Constante.TProtegido, "", "", Constante.TContinuar, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, new Ambito(Constante.TContinuar), null)
                        };
                        return lista;
                    }


                case Constante.TSalir:
                    {
                        List<Simbolo> lista = new List<Simbolo>
                        {
                            new Simbolo(Constante.TProtegido, "", "", Constante.TSalir, Nodo.Token.Location.Line + 1, Nodo.Token.Location.Column + 1, new Ambito(Constante.TSalir), null)
                        };
                        return lista;
                    }
            }
            return null;
        }
    }
}
