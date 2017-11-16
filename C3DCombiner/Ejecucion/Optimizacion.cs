using C3DCombiner.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Ejecucion
{
    static class Optimizacion
    {
        static int size = 100;
        static List<Simbolo> Optimizado = new List<Simbolo>();
        static List<List<Simbolo>> ListaMirillas = new List<List<Simbolo>>();

        public static void Optimizar(Ambito Metodos)
        {
            Optimizado = new List<Simbolo>();
            int tamanio = size;

            foreach (Simbolo metodo in Metodos.TablaSimbolo)
            {
                List<Simbolo> Mirilla = new List<Simbolo>();
                int i = 0;
                List<Simbolo> Ambito = new List<Simbolo>();
                int l = 0;
                int tope = metodo.Ambito.TablaSimbolo.Count;
                for (l = 0; l < tope; l++)
                {
                    Mirilla.Add(metodo.Ambito.TablaSimbolo[l]);
                    if (!(i < size) || !(l < tope - 1))
                    {
                        AplicarReglas(Mirilla);
                        //metodo.Ambito.TablaSimbolo = Mirilla;
                        foreach (Simbolo sim in Mirilla)
                        {
                            Ambito.Add(sim);
                        }
                        Mirilla = new List<Simbolo>();
                        i = -1;
                    }
                    else
                    {
                        i++;
                    }
                }
                metodo.Ambito.TablaSimbolo = Ambito;
            }

            ImprimirOptimizacion(Metodos);
        }

        private static void ImprimirOptimizacion(Ambito ambito)
        {
            TitusTools.Codigo3DOptmizado.Text = "";
            String cad = "";
            foreach (Simbolo metodo in ambito.TablaSimbolo)
            {
                if (metodo.Rol.Equals(Constante.TMetodo))
                {
                    cad += "void " + metodo.Nombre + "(){\n";
                }
                else
                {
                    cad += "main(){\n";
                }

                foreach (Simbolo sim in metodo.Ambito.TablaSimbolo)
                {
                    cad += sim.Generar3DOptimizado();
                }

                cad += "}\n\n";
            }
            TitusTools.Codigo3DOptmizado.Text = cad;
        }

        static void AplicarReglas(List<Simbolo> Mirilla)
        {
            Regla1(Mirilla);
            Regla2(Mirilla);
            Regla3(Mirilla);
            Regla4(Mirilla);
            Regla5(Mirilla);
            Regla6(Mirilla);
            Regla7(Mirilla);
            Regla8(Mirilla);
            Regla9(Mirilla);
            Regla10(Mirilla);
            Regla11(Mirilla);
            Regla12(Mirilla);
            Regla13(Mirilla);
            Regla14(Mirilla);
            Regla15(Mirilla);
            Regla16(Mirilla);
            Regla17(Mirilla);
            Regla18(Mirilla);
            Regla19(Mirilla);
        }

        static void Regla1(List<Simbolo> Mirilla)
        {
            FAsignacion3D anterior = null;
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;

                    if (asignacion.Tipo.Equals(Constante.Temporal))
                    {
                        if (asignacion.Valor.Izquierda == null && asignacion.Valor.Derecha == null)
                        {
                            if (anterior != null)
                            {
                                if (anterior.Temporal.Equals(asignacion.Valor.Nombre) && anterior.Valor.Nombre.Equals(asignacion.Temporal))
                                {
                                    TitusTools.InsertarReporteOptimizacion("Regla 1", anterior.Temporal + " = " + anterior.Valor.Nombre + " <=> " + asignacion.Temporal + " = " + asignacion.Valor.Nombre, sim.Fila, sim.Columna);
                                    Mirilla.Remove(sim);
                                }
                            }
                            else
                            {
                                anterior = asignacion;
                            }
                        }
                        else
                        {
                            anterior = null;
                        }
                    }
                    else
                    {
                        anterior = null;
                    }
                }
                else
                {
                    anterior = null;
                }
            }
        }

        static void Regla2(List<Simbolo> Mirilla)
        {
            int star = -1;
            int end = -1;
            int tope = Mirilla.Count;
            Simbolo gt = null;
            for (int i = 0; i < tope; i++)
            {
                if (gt == null)
                {
                    if (Mirilla[i].Rol.Equals(Constante.TGoto))
                    {
                        gt = Mirilla[i];
                        star = i;
                    }
                }
                else
                {
                    if (Mirilla[i].Rol.Equals(Constante.Etiqueta))
                    {
                        if (Mirilla[i].Nombre.Equals(gt.Nombre))
                        {

                            end = i;

                            i = i - ((end - star) - 1);
                            Mirilla.RemoveRange(star + 1, end - star - 1);
                            tope = Mirilla.Count;

                            if (end - star - 1 > 0)
                            {
                                TitusTools.InsertarReporteOptimizacion("Regla 2", "goto " + gt.Nombre, gt.Fila, gt.Columna);

                            }

                            gt = null;
                            star = -1;
                            end = -1;
                        }
                        else
                        {
                            gt = null;
                            star = -1;
                        }
                    }
                }
            }
        }

        static void Regla3(List<Simbolo> Mirilla)
        {
            int tope = Mirilla.Count;
            Simbolo si = null;
            Simbolo gt = null;
            for (int i = 0; i < tope; i++)
            {
                Simbolo aux = Mirilla[i];
                if (aux.Tipo == Constante.TIf)
                {
                    FIf f = (FIf)aux.Valor;
                    if (f.Tipo.Equals(Constante.TIf))
                    {
                        si = aux;
                    }
                }
                else
                {
                    if (si != null && gt == null)
                    {
                        if (aux.Rol.Equals(Constante.TGoto))
                        {
                            gt = aux;
                        }
                        else
                        {
                            si = null;
                        }
                    }
                    else
                    {
                        if (gt != null)
                        {
                            if (aux.Rol.Equals(Constante.Etiqueta))
                            {
                                FIf f = (FIf)si.Valor;
                                if (f.Etiqueta.Equals(aux.Nombre))
                                {
                                    TitusTools.InsertarReporteOptimizacion("Regla 3", "if " + f.Condicion.Generar3DOptimizado() + " goto " + f.Etiqueta, si.Fila, si.Columna);
                                    f.Tipo = Constante.TIfFalse;
                                    f.Etiqueta = gt.Nombre;
                                    Mirilla.Remove(gt);
                                    Mirilla.Remove(aux);
                                    tope = Mirilla.Count;
                                    i = i - 2;
                                    si = null;
                                    gt = null;

                                }
                            }
                            else
                            {
                                si = null;
                                gt = null;
                            }
                        }
                    }
                }
            }
        }

        static void Regla4(List<Simbolo> Mirilla)
        {
            int tope = Mirilla.Count;
            Simbolo si = null;
            for (int i = 0; i < tope; i++)
            {
                Simbolo aux = Mirilla[i];
                if (aux.Rol == Constante.TIf)
                {
                    FIf f = (FIf)aux.Valor;

                    if ((f.Condicion.Izquierda.Tipo.Equals(Constante.TEntero) || f.Condicion.Izquierda.Tipo.Equals(Constante.TDecimal)) && (f.Condicion.Derecha.Tipo.Equals(Constante.TEntero) || f.Condicion.Derecha.Tipo.Equals(Constante.TDecimal)))
                    {
                        if (f.Condicion.ResolverExpresion().Bool)
                        {
                            si = aux;
                        }
                    }
                }
                else
                {
                    if (si != null)
                    {
                        if (aux.Rol.Equals(Constante.TGoto))
                        {
                            FIf f = (FIf)si.Valor;
                            TitusTools.InsertarReporteOptimizacion("Regla 4", "if " + f.Condicion.Generar3DOptimizado() + " goto " + f.Etiqueta, si.Fila, si.Columna);
                            aux.Nombre = f.Etiqueta;
                            Mirilla.Remove(si);
                            tope = Mirilla.Count;
                            i = i - 1;
                        }
                    }
                    si = null;
                }
            }
        }

        static void Regla5(List<Simbolo> Mirilla)
        {
            int tope = Mirilla.Count;
            Simbolo si = null;
            for (int i = 0; i < tope; i++)
            {
                Simbolo aux = Mirilla[i];
                if (aux.Rol == Constante.TIf)
                {
                    FIf f = (FIf)aux.Valor;

                    if ((f.Condicion.Izquierda.Tipo.Equals(Constante.TEntero) || f.Condicion.Izquierda.Tipo.Equals(Constante.TDecimal)) && (f.Condicion.Derecha.Tipo.Equals(Constante.TEntero) || f.Condicion.Derecha.Tipo.Equals(Constante.TDecimal)))
                    {
                        if (!f.Condicion.ResolverExpresion().Bool)
                        {
                            si = aux;
                        }
                    }
                }
                else
                {
                    if (si != null)
                    {
                        if (aux.Rol.Equals(Constante.TGoto))
                        {
                            FIf f = (FIf)si.Valor;
                            TitusTools.InsertarReporteOptimizacion("Regla 5", "if " + f.Condicion.Generar3DOptimizado() + " goto " + f.Etiqueta, si.Fila, si.Columna);
                            Mirilla.Remove(si);
                            tope = Mirilla.Count;
                            i = i - 1;
                        }
                    }
                    si = null;
                }
            }
        }

        static void Regla6(List<Simbolo> Mirilla)
        {
            int tope = Mirilla.Count;
            Simbolo go = null;
            Simbolo et = null;
            for (int i = 0; i < tope; i++)
            {
                Simbolo aux = Mirilla[i];
                if (aux.Rol == Constante.TGoto && go == null && et == null)
                {
                    go = aux;
                }
                else
                {
                    if (go != null && et == null)
                    {
                        if (aux.Rol.Equals(Constante.Etiqueta))
                        {
                            if (aux.Nombre.Equals(go.Nombre))
                            {
                                et = aux;
                            }
                            else
                            {
                                go = null;
                            }
                        }
                    }
                    else
                    {
                        if (aux.Rol.Equals(Constante.TGoto))
                        {

                            TitusTools.InsertarReporteOptimizacion("Regla 6", "goto " + go.Nombre, go.Fila, go.Columna);
                            go.Nombre = aux.Nombre;
                            go = null;
                            et = null;
                        }
                        else
                        {
                            go = null;
                            et = null;
                        }
                    }
                }
            }
        }

        static void Regla7(List<Simbolo> Mirilla)
        {
            int tope = Mirilla.Count;
            Simbolo go = null;
            Simbolo et = null;
            for (int i = 0; i < tope; i++)
            {
                Simbolo aux = Mirilla[i];
                if (aux.Rol == Constante.TIf && go == null && et == null)
                {
                    go = aux;
                }
                else
                {
                    if (go != null && et == null)
                    {
                        if (aux.Rol.Equals(Constante.Etiqueta))
                        {
                            FIf f = (FIf)go.Valor;
                            if (aux.Nombre.Equals(f.Etiqueta))
                            {
                                et = aux;
                            }
                            else
                            {
                                go = null;
                            }
                        }
                    }
                    else
                    {
                        if (aux.Rol.Equals(Constante.TGoto))
                        {

                            
                            FIf f = (FIf)go.Valor;
                            TitusTools.InsertarReporteOptimizacion("Regla 7", "if " + f.Condicion.Generar3DOptimizado() + "goto " + f.Etiqueta, go.Fila, go.Columna);
                            f.Etiqueta = aux.Nombre;
                            go = null;
                            et = null;
                        }
                        else
                        {
                            go = null;
                            et = null;
                        }
                    }
                }
            }
        }

        static void Regla8(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;
                    if (asignacion.Tipo.Equals(Constante.Temporal))
                    {
                        if (asignacion.Valor.Tipo.Equals(Constante.TMas))
                        {
                            FNodoExpresion izq = asignacion.Valor.Izquierda;
                            FNodoExpresion der = asignacion.Valor.Derecha;

                            if (izq.Tipo.Equals(Constante.TEntero) || der.Tipo.Equals(Constante.TEntero))
                            {
                                if (izq.Tipo == Constante.Temporal && asignacion.Temporal.Equals(izq.Nombre))
                                {
                                    if (der.Entero == 0)
                                    {
                                        TitusTools.InsertarReporteOptimizacion("Regla 8", asignacion.Temporal + " = " + izq.Nombre + " + 0;", sim.Fila, sim.Columna);
                                        Mirilla.Remove(sim);
                                    }
                                }
                                else
                                {
                                    if (izq.Entero == 0 && asignacion.Temporal.Equals(der.Nombre))
                                    {
                                        TitusTools.InsertarReporteOptimizacion("Regla 8", asignacion.Temporal + " = 0 + " + izq.Nombre + ";", sim.Fila, sim.Columna);
                                        Mirilla.Remove(sim);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Regla9(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;
                    if (asignacion.Tipo.Equals(Constante.Temporal))
                    {
                        if (asignacion.Valor.Tipo.Equals(Constante.TMenos))
                        {
                            FNodoExpresion izq = asignacion.Valor.Izquierda;
                            FNodoExpresion der = asignacion.Valor.Derecha;

                            if (izq != null)
                            {
                                if (izq.Tipo.Equals(Constante.TEntero) || der.Tipo.Equals(Constante.TEntero))
                                {
                                    if (izq.Tipo == Constante.Temporal && asignacion.Temporal.Equals(izq.Nombre))
                                    {
                                        if (der.Entero == 0)
                                        {
                                            TitusTools.InsertarReporteOptimizacion("Regla 9", asignacion.Temporal + " = " + izq.Nombre + " - 0;", sim.Fila, sim.Columna);
                                            Mirilla.Remove(sim);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Regla10(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;
                    if (asignacion.Tipo.Equals(Constante.Temporal))
                    {
                        if (asignacion.Valor.Tipo.Equals(Constante.TPor))
                        {
                            FNodoExpresion izq = asignacion.Valor.Izquierda;
                            FNodoExpresion der = asignacion.Valor.Derecha;

                            if (izq.Tipo.Equals(Constante.TEntero) || der.Tipo.Equals(Constante.TEntero))
                            {
                                if (izq.Tipo == Constante.Temporal && asignacion.Temporal.Equals(izq.Nombre))
                                {
                                    if (der.Entero == 1)
                                    {
                                        TitusTools.InsertarReporteOptimizacion("Regla 10", asignacion.Temporal + " = " + izq.Nombre + " * 1;", sim.Fila, sim.Columna);
                                        Mirilla.Remove(sim);
                                    }
                                }
                                else
                                {
                                    if (izq.Entero == 1 && asignacion.Temporal.Equals(der.Nombre))
                                    {
                                        TitusTools.InsertarReporteOptimizacion("Regla 10", asignacion.Temporal + " = 1 * " + izq.Nombre + ";", sim.Fila, sim.Columna);
                                        Mirilla.Remove(sim);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Regla11(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;
                    if (asignacion.Tipo.Equals(Constante.Temporal))
                    {
                        if (asignacion.Valor.Tipo.Equals(Constante.TDivision))
                        {
                            FNodoExpresion izq = asignacion.Valor.Izquierda;
                            FNodoExpresion der = asignacion.Valor.Derecha;

                            if (izq != null)
                            {
                                if (izq.Tipo.Equals(Constante.TEntero) || der.Tipo.Equals(Constante.TEntero))
                                {
                                    if (izq.Tipo == Constante.Temporal && asignacion.Temporal.Equals(izq.Nombre))
                                    {
                                        if (der.Entero == 1)
                                        {
                                            TitusTools.InsertarReporteOptimizacion("Regla 11", asignacion.Temporal + " = " + izq.Nombre + " / 1;", sim.Fila, sim.Columna);
                                            Mirilla.Remove(sim);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Regla12(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;

                    if (asignacion.Valor.Tipo.Equals(Constante.TMas))
                    {
                        FNodoExpresion izq = asignacion.Valor.Izquierda;
                        FNodoExpresion der = asignacion.Valor.Derecha;

                        if (izq.Tipo.Equals(Constante.TEntero) || der.Tipo.Equals(Constante.TEntero))
                        {
                            if (izq.Tipo == Constante.Temporal || izq.Tipo == Constante.TH || izq.Tipo == Constante.TP)
                            {
                                if (der.Entero == 0)
                                {
                                    asignacion.Valor = izq;
                                    TitusTools.InsertarReporteOptimizacion("Regla 12", asignacion.Temporal + " = " + izq.Nombre + " + 0;", sim.Fila, sim.Columna);
                                    //Mirilla.Remove(sim);
                                }
                            }
                            else
                            {
                                if (izq.Entero == 0)
                                {
                                    asignacion.Valor = der;
                                    TitusTools.InsertarReporteOptimizacion("Regla 12", asignacion.Temporal + " = 0 + " + der.Nombre + ";", sim.Fila, sim.Columna);
                                    //Mirilla.Remove(sim);
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Regla13(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;

                    if (asignacion.Valor.Tipo.Equals(Constante.TMenos))
                    {
                        FNodoExpresion izq = asignacion.Valor.Izquierda;
                        FNodoExpresion der = asignacion.Valor.Derecha;

                        if (izq != null)
                        {
                            if (izq.Tipo.Equals(Constante.TEntero) || der.Tipo.Equals(Constante.TEntero))
                            {
                                if (der.Entero == 0)
                                {
                                    asignacion.Valor = izq;
                                    TitusTools.InsertarReporteOptimizacion("Regla 13", asignacion.Temporal + " = " + izq.Nombre + " - 0;", sim.Fila, sim.Columna);
                                    //Mirilla.Remove(sim);
                                }
                            }
                        }

                    }
                }
            }
        }

        static void Regla14(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;

                    if (asignacion.Valor.Tipo.Equals(Constante.TPor))
                    {
                        FNodoExpresion izq = asignacion.Valor.Izquierda;
                        FNodoExpresion der = asignacion.Valor.Derecha;

                        if (izq.Tipo.Equals(Constante.TEntero) || der.Tipo.Equals(Constante.TEntero))
                        {
                            if (der.Entero == 1)
                            {
                                asignacion.Valor = izq;
                                TitusTools.InsertarReporteOptimizacion("Regla 14", asignacion.Temporal + " = " + izq.Nombre + " * 1;", sim.Fila, sim.Columna);
                                //Mirilla.Remove(sim);
                            }

                        }
                    }
                }
            }
        }

        static void Regla15(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;
                    if (asignacion.Valor.Tipo.Equals(Constante.TDivision))
                    {
                        FNodoExpresion izq = asignacion.Valor.Izquierda;
                        FNodoExpresion der = asignacion.Valor.Derecha;

                        if (der.Tipo.Equals(Constante.TEntero))
                        {
                            if (der.Entero == 1)
                            {
                                asignacion.Valor = izq;
                                TitusTools.InsertarReporteOptimizacion("Regla 15", asignacion.Temporal + " = " + izq.Nombre + " / 1;", sim.Fila, sim.Columna);
                                //Mirilla.Remove(sim);
                            }
                        }
                    }
                }
            }
        }

        static void Regla16(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;

                    if (asignacion.Valor.Tipo.Equals(Constante.TPotenciaOCL))
                    {
                        FNodoExpresion izq = asignacion.Valor.Izquierda;
                        FNodoExpresion der = asignacion.Valor.Derecha;

                        if (der.Tipo.Equals(Constante.TEntero))
                        {
                            if (izq.Tipo == Constante.TP || izq.Tipo == Constante.Temporal || izq.Tipo == Constante.TH)
                            {
                                if (der.Entero == 2)
                                {
                                    asignacion.Valor = new FNodoExpresion(izq, izq, Constante.TPor, Constante.TPor, asignacion.Valor.Fila, asignacion.Valor.Columna, null);
                                    TitusTools.InsertarReporteOptimizacion("Regla 16", asignacion.Temporal + " = " + izq.Nombre + " ^ 2;", sim.Fila, sim.Columna);
                                    //Mirilla.Remove(sim);
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Regla17(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;

                    if (asignacion.Valor.Tipo.Equals(Constante.TPor))
                    {
                        FNodoExpresion izq = asignacion.Valor.Izquierda;
                        FNodoExpresion der = asignacion.Valor.Derecha;

                        if (der.Tipo.Equals(Constante.TEntero))
                        {
                            if (izq.Tipo == Constante.TP || izq.Tipo == Constante.Temporal || izq.Tipo == Constante.TH)
                            {
                                if (der.Entero == 2)
                                {
                                    asignacion.Valor = new FNodoExpresion(izq, izq, Constante.TMas, Constante.TMas, asignacion.Valor.Fila, asignacion.Valor.Columna, null);
                                    TitusTools.InsertarReporteOptimizacion("Regla 17", asignacion.Temporal + " = " + izq.Nombre + "  * 2;", sim.Fila, sim.Columna);
                                    //Mirilla.Remove(sim);
                                }
                            }
                        }
                        else if (izq.Tipo.Equals(Constante.TEntero))
                        {
                            if (der.Tipo == Constante.TP || der.Tipo == Constante.Temporal || der.Tipo == Constante.TH)
                            {
                                if (izq.Entero == 2)
                                {
                                    asignacion.Valor = new FNodoExpresion(der, der, Constante.TMas, Constante.TMas, asignacion.Valor.Fila, asignacion.Valor.Columna, null);
                                    TitusTools.InsertarReporteOptimizacion("Regla 17", asignacion.Temporal + " = 2 * " + der.Nombre + ";", sim.Fila, sim.Columna);
                                    //Mirilla.Remove(sim);
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Regla18(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;

                    if (asignacion.Valor.Tipo.Equals(Constante.TPor))
                    {
                        FNodoExpresion izq = asignacion.Valor.Izquierda;
                        FNodoExpresion der = asignacion.Valor.Derecha;

                        if (der.Tipo.Equals(Constante.TEntero))
                        {
                            if (izq.Tipo == Constante.TP || izq.Tipo == Constante.Temporal || izq.Tipo == Constante.TH)
                            {
                                if (der.Entero == 0)
                                {
                                    asignacion.Valor = der;
                                    TitusTools.InsertarReporteOptimizacion("Regla 18", asignacion.Temporal + " = " + izq.Nombre + " * 0;", sim.Fila, sim.Columna);
                                    //Mirilla.Remove(sim);
                                }
                            }
                        }
                        else if (izq.Tipo.Equals(Constante.TEntero))
                        {
                            if (der.Tipo == Constante.TP || der.Tipo == Constante.Temporal || der.Tipo == Constante.TH)
                            {
                                if (izq.Entero == 0)
                                {
                                    asignacion.Valor = izq;
                                    TitusTools.InsertarReporteOptimizacion("Regla 18", asignacion.Temporal + " = 0 * " + der.Nombre + ";", sim.Fila, sim.Columna);
                                    //Mirilla.Remove(sim);
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Regla19(List<Simbolo> Mirilla)
        {
            foreach (Simbolo sim in Mirilla.ToList())
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;

                    if (asignacion.Valor.Tipo.Equals(Constante.TDivision))
                    {
                        FNodoExpresion izq = asignacion.Valor.Izquierda;
                        FNodoExpresion der = asignacion.Valor.Derecha;

                        if (izq.Tipo.Equals(Constante.TEntero))
                        {
                            if (der.Tipo == Constante.TP || der.Tipo == Constante.Temporal || der.Tipo == Constante.TH)
                            {
                                if (izq.Entero == 0)
                                {
                                    asignacion.Valor = izq;
                                    TitusTools.InsertarReporteOptimizacion("Regla 19", asignacion.Temporal + " = 0 / " + der.Nombre + ";", sim.Fila, sim.Columna);
                                    //Mirilla.Remove(sim);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}




