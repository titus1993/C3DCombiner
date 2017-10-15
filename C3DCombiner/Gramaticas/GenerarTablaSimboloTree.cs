using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner
{
    class GenerarTablaSimboloTree
    {
        public Ejecutar3DC Ejecutar { get; set; }
        public GenerarTablaSimboloTree(ParseTree Arbol, String Ruta)
        {
            Ejecutar = (Ejecutar3DC)RecorrerArbol(Arbol.Root);
            Ejecutar.Ruta = Ruta;
        }


        public Object RecorrerArbol(ParseTreeNode Nodo)
        {
            switch (Nodo.Term.Name)
            {
                case Constante.INICIO:
                    
                    /*foreach (ParseTreeNodeList hijo in (List<ParseTreeNodeList>)RecorrerArbol(Nodo.ChildNodes[0])) //enviamos a analizar el encabezado
                    {
                        if (hijo[0].Term.Name == Constante.TDefine)
                        {
                            if (hijo[1].Term.Name == "numero")
                            {
                                ejecucion.SetDefineNumber(Double.Parse(hijo[1].Token.ValueString));
                            }
                            else if (hijo[1].Term.Name == Constante.Cadena)
                            {
                                ejecucion.SetDefineRuta((String)hijo[1].Token.Value);
                            }
                        }
                        else if (hijo[0].Term.Name == Constante.TIncluye)
                        {
                            EIncluye incluye = new Encabezado.EIncluye(hijo[1].Token.Text + ".sbs", "");

                            ejecucion.AgregarIncluye(new Simbolo(incluye.Archivo, Constante.TIncluye, Constante.Cadena, hijo[1].Token.Location.Line + 1, hijo[1].Token.Location.Column + 1, null, incluye));
                        }
                    }
                    
                    return ejecucion;*/

                

                default:
                    return null;

            }
        }        
    }
}
