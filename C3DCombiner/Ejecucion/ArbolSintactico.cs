﻿using C3DCombiner.Funciones;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Ejecucion
{
    class ArbolSintactico
    {
        public ArbolSintactico(ParseTreeNode arbol, int tipo, String ruta)
        {
            if (tipo == 0)
            {

            }else if (tipo == 1)
            {
                TitusTools.Rutas.Add(ruta);
                Archivo archivo = (Archivo)GenerarTablaSimboloTree.RecorrerArbol(arbol);
                TitusTools.Rutas.RemoveAt(TitusTools.Rutas.Count - 1);
                archivo.Ejecutar();
            }else if (tipo == 3)
            {

            }
        }
        
    }
}
