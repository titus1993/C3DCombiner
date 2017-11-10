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
                TitusTools.Rutas.Add(ruta);
                Archivo archivo = (Archivo)GenerarTablaSimboloOLC.RecorrerArbol(arbol);
                TitusTools.Rutas.RemoveAt(TitusTools.Rutas.Count - 1);
                if (archivo != null)
                {
                    TitusTools.Archivos_Importados.Add(archivo);
                    archivo.GenerarImports();
                }
            }
            else if (tipo == 1)
            {
                TitusTools.Rutas.Add(ruta);
                Archivo archivo = (Archivo)GenerarTablaSimboloTree.RecorrerArbol(arbol);
                TitusTools.Rutas.RemoveAt(TitusTools.Rutas.Count - 1);
                if (archivo != null)
                {
                    TitusTools.Archivos_Importados.Add(archivo);
                    archivo.GenerarImports();
                }
            }else if (tipo == 2)
            {
                TitusTools.Rutas.Add(ruta);
                Ejecucion3D Ejecucion = (Ejecucion3D)GenerarTablaSimbolo3D.RecorrerArbol(arbol);
                TitusTools.Rutas.RemoveAt(TitusTools.Rutas.Count - 1);
                if (Ejecucion != null)
                {
                    Ejecucion.Ejecutar();
                }
            }
        }
    }
}
