using C3DCombiner.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Ejecucion
{
    static class Tabla3D
    {

        public static int H = 0;

        public static int P = 0;

        public static List<Variable> Tabla = new List<Variable>();

        public static List<FNodoExpresion> Heap = new List<FNodoExpresion>();

        public static List<FNodoExpresion> Stack = new List<FNodoExpresion>();
        public static void IniciarTabla()
        {
            Tabla = new List<Variable>();
            H = 0;
            P = 0;
            Heap.Clear();
            Stack.Clear();
        }

        public static void ApartarInicio()
        {
            Tabla.Add(null);
        }

        public static void InsertarVariable(Variable var)
        {
            var.Nombre = var.Nombre.ToLower();
            Tabla.Add(var);
        }

        public static void SacarVariable()
        {
            Tabla.RemoveAt(Tabla.Count - 1);
        }

        public static void InsertarAmbito(Ambito ambito)
        {
            //insertamos el tope de ambito
            Tabla.Add(new Variable(Constante.Tope, Constante.Tope, 0, 0, null, null));
            foreach (Simbolo sim in ambito.TablaSimbolo)
            {
                if (sim.Rol.Equals(Constante.ASIGNACION))
                {
                    FAsignacion3D asignacion = (FAsignacion3D)sim.Valor;
                    if (asignacion.Tipo.Equals(Constante.Temporal))
                    {
                        if (!ExisteVariable(asignacion.Temporal))
                        {
                            InsertarVariable(new Variable(asignacion.Temporal, Constante.Id, 0, 0, null, null));
                        }
                    }
                }
            }
        }

        public static void SacarAmbito()
        {
            for (int i = Tabla.Count - 1; i >= 0; i--)
            {
                if (!Tabla.ElementAt(i).Rol.Equals(Constante.Tope))
                {
                    SacarVariable();
                }
                else
                {
                    break;
                }
            }
        }

        public static Boolean ExisteVariable(String nombre)
        {
            for (int i = Tabla.Count - 1; i >= 0; i--)
            {
                if (!Tabla.ElementAt(i).Rol.Equals(Constante.Tope))
                {
                    if (Tabla.ElementAt(i).Nombre.Equals(nombre))
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        public static FMetodo BuscarMetodo(String nombre)
        {
            foreach (Variable var in Tabla)
            {
                if (var.Rol.Equals(Constante.TMetodo) && var.Nombre.Equals(nombre))
                {
                    return (FMetodo)var.Valor;
                }
                if (var.Rol.Equals(Constante.Tope))
                {
                    break;
                }
            }
            return null;
        }

        public static Variable BuscarVariable(String nombre)
        {
            for (int i = Tabla.Count - 1; i >= 0; i--)
            {
                if (!Tabla.ElementAt(i).Rol.Equals(Constante.Tope))
                {
                    if (Tabla.ElementAt(i).Nombre.Equals(nombre) && Tabla.ElementAt(i).Rol.Equals(Constante.Id))
                    {
                        return Tabla.ElementAt(i);
                    }
                }
                else
                {
                    break;
                }
            }
            return null;
        }

        public static void InsertarHeap(int pos, FNodoExpresion valor)
        {
            if (Heap.Count - 1 >= pos)
            {
                Heap.RemoveAt(pos);
                Heap.Insert(pos, valor);
            }
            else
            {
                int aumento = pos - Heap.Count;
                
                for (int i = 0; i < aumento; i++)
                {
                    Heap.Add(null);
                }
                Heap.Insert(pos, valor);
            }
        }

        public static void InsertarStack(int pos, FNodoExpresion valor)
        {
            if (Stack.Count - 1 >= pos)
            {
                Stack.RemoveAt(pos);
                Stack.Insert(pos, valor);
            }
            else
            {
                int aumento = pos - Stack.Count;
                
                for (int i = 0; i < aumento; i++)
                {
                    Stack.Add(null);
                }
                Stack.Insert(pos, valor);
            }
        }
    }
}
