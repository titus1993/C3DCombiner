using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Forms
{
    static class GraficarAST
    {

        static string graph;

        static int cont = 0;

        static String path = Path.GetTempPath();
        public static String GenerarArbol(ParseTreeNode raiz)
        {
            int n = cont;
            cont++;
            System.IO.StreamWriter f = new System.IO.StreamWriter(path + "AST" + n.ToString() + ".txt");
            f.Write("digraph G{ rankir=TB; node [shape = box, fontsize=12, fontname=\"Arial\", style=filled, fillcolor=grey88];");
            graph = "";
            Generar(raiz);
            f.Write(graph);
            f.Write("}");
            f.Close();
            GenerarGrafica(n);
            return path + "AST" + n.ToString() + ".jpg";
        }

        private static void Generar(ParseTreeNode raiz)
        {
            graph = graph + "nodo" + raiz.GetHashCode() + "[label=\"" + raiz.ToString() + " \"]; \n";
            if (raiz.ChildNodes.Count > 0)
            {
                ParseTreeNode[] hijos = raiz.ChildNodes.ToArray();
                for (int i = 0; i < raiz.ChildNodes.Count; i++)
                {
                    Generar(hijos[i]);
                    graph = graph + "\"nodo" + raiz.GetHashCode() + "\"-> \"nodo" + hijos[i].GetHashCode() + "\" \n";
                }
            }
        }

        private static void GenerarGrafica(int n)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("dot.exe");
                startInfo.Arguments = "-Tjpg  " + path + "AST" + n.ToString() + ".txt -o " + path + "AST" + n.ToString() + ".jpg";
                Process.Start(startInfo);
            }
            catch (Exception e)
            {
                
            }
        }

    }
}
