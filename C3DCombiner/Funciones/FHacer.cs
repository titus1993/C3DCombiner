﻿using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FHacer
    {
        public Ambito Ambito;
        public FNodoExpresion Condicion;

        public Simbolo Padre;

        public FHacer(FNodoExpresion Condicion, Ambito Ambito)
        {
            this.Condicion = Condicion;
            this.Ambito = Ambito;
            this.Padre = null;
        }
    }
}
