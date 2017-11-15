using C3DCombiner.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Ejecucion
{
    class Simbolo
    {
        public int Fila, Columna, Posicion, Tamaño;
        public String Nombre, Rol, Tipo, Visibilidad;
        public Ambito Ambito = null;
        public Simbolo Hermano = null;
        public Simbolo Padre = null;
        public Object Valor = null;

        public Simbolo Siguiente = null;
        public Simbolo Anterior = null;


        public int este = 0;

        public Simbolo(String visibilidad, String tipo, String nombre, String rol, int fila, int columna, Ambito ambito, Object valor)
        {
            this.Visibilidad = visibilidad;
            this.Tipo = tipo;
            this.Nombre = nombre;
            this.Rol = rol;
            this.Fila = fila;
            this.Columna = columna;
            this.Ambito = ambito;
            this.Valor = valor;
            this.Tamaño = this.Ambito.Tamaño;
            this.Posicion = -1;
        }

        public Simbolo(Simbolo sim)
        {
            this.Visibilidad = sim.Visibilidad;
            this.Tipo = sim.Tipo;
            this.Nombre = sim.Nombre;
            this.Rol = sim.Rol;
            this.Fila = sim.Fila;
            this.Columna = sim.Columna;
            this.Ambito = sim.Ambito;
            this.Tamaño = sim.Tamaño;
            this.Posicion = sim.Posicion;
            this.Hermano = sim.Hermano;
            this.Padre = sim.Padre;
            this.Valor = sim.Valor;
            this.este = sim.este;
        }

        public String Generar3DConMain()
        {
            String cadena = "";
            switch (Rol)
            {
                case Constante.TClase:
                    cadena = GenerarClaseConMain();
                    break;

                case Constante.TConstructor:
                    cadena = GenerarConstructor();
                    break;

                case Constante.DECLARACION:
                    cadena = GenerarDeclaracion();
                    break;

                case Constante.TMetodo:
                    cadena = GenerarMetodo();
                    break;

                case Constante.TPrincipal:
                    cadena = GenerarPrincipal();
                    break;

                case Constante.MIENTRAS:
                    cadena = GenerarMientras();
                    break;

                case Constante.HACER:
                    cadena = GenerarHacer();
                    break;

                case Constante.TRepetir:
                    cadena = GenerarRepetir();
                    break;

                case Constante.TLoop:
                    cadena = GenerarLoop();
                    break;

                case Constante.TSi:
                    cadena = GenerarSi();
                    break;

                case Constante.TElegir:
                    cadena = GenerarElegir();
                    break;

                case Constante.TPara:
                    cadena = GenerarPara();
                    break;

                case Constante.TX:
                    cadena = GenerarX();
                    break;

                case Constante.TContinuar:
                    cadena = GenerarContinuar();
                    break;

                case Constante.TSalir:
                    cadena = GenerarSalir();
                    break;

                case Constante.TRetorno:
                    cadena = GenerarRetornar();
                    break;

                case Constante.TImprimir:
                    cadena = GenerarImprimir();
                    break;

                case Constante.ASIGNACION:
                    cadena = Generar3DAsignacion();
                    break;

                case Constante.LLAMADA_METODO:
                    cadena = GenerarLlamadaMetodo();
                    break;

                case Constante.TSuper:
                    cadena = GenerarSuper();
                    break;
            }
            return cadena;
        }

        public String Generar3D()
        {
            String cadena = "";
            switch (Rol)
            {
                case Constante.TClase:
                    cadena = GenerarClase();
                    break;

                case Constante.DECLARACION:
                    cadena = GenerarDeclaracion();
                    break;

                case Constante.TMetodo:
                    cadena = GenerarMetodo();
                    break;

                case Constante.TConstructor:
                    cadena = GenerarConstructor();
                    break;

                case Constante.MIENTRAS:
                    cadena = GenerarMientras();
                    break;

                case Constante.HACER:
                    cadena = GenerarHacer();
                    break;

                case Constante.TRepetir:
                    cadena = GenerarRepetir();
                    break;

                case Constante.TLoop:
                    cadena = GenerarLoop();
                    break;

                case Constante.TX:
                    cadena = GenerarX();
                    break;

                case Constante.TSi:
                    cadena = GenerarSi();
                    break;

                case Constante.TElegir:
                    cadena = GenerarElegir();
                    break;

                case Constante.TPara:
                    cadena = GenerarPara();
                    break;

                case Constante.TContinuar:
                    cadena = GenerarContinuar();
                    break;

                case Constante.TSalir:
                    cadena = GenerarSalir();
                    break;

                case Constante.TRetorno:
                    cadena = GenerarRetornar();
                    break;

                case Constante.TImprimir:
                    cadena = GenerarImprimir();
                    break;

                case Constante.ASIGNACION:
                    cadena = Generar3DAsignacion();
                    break;

                case Constante.LLAMADA_METODO:
                    cadena = GenerarLlamadaMetodo();
                    break;

                case Constante.TSuper:
                    cadena = GenerarSuper();
                    break;
            }
            return cadena;
        }

        private String GenerarClase()
        {
            String cadena = "";
            FClase clase = (FClase)Valor;
            cadena += clase.Generar3D();
            return cadena;            
        }

        private String GenerarClaseConMain()
        {
            String cadena = "";
            FClase clase = (FClase)Valor;
            cadena += clase.Generar3DconMain();
            return cadena;
        }

        private String GenerarConstructor()
        {
            String cadena = "";

            FMetodo constructor = (FMetodo)Valor;
            cadena += constructor.Generar3DConstructor();

            return cadena;
        }

        private String GenerarSuper()
        {
            String cadena = "";

            FSuper super = (FSuper)Valor;
            cadena += super.Generar3D();

            return cadena;
        }

        private String GenerarPrincipal()
        {
            String cadena = "";
            FMetodo metodo = (FMetodo)Valor;
            cadena += metodo.GenerarPrincipal3D();
            return cadena;
        }

        private String GenerarMetodo()
        {
            String cadena = "";
            FMetodo metodo = (FMetodo)Valor;
            cadena += metodo.Generar3D();
            return cadena;
        }

        private String GenerarDeclaracion()
        {
            String cadena = "";
            FDeclaracion declaracion = (FDeclaracion)Valor;
            cadena += declaracion.Generar3D(2);//es le envia 2 para que se aparte la posicion del this y el retorno
            return cadena;
        }

        private String GenerarLlamadaMetodo()
        {
            String cadena = "";
            FLlamadaObjeto llamada = (FLlamadaObjeto)Valor;
            cadena += llamada.Generar3D().Codigo;
            return cadena;
        }

        //ciclos
        private String GenerarMientras()
        {
            String cadena = "";
            FMientras mientras = (FMientras)Valor;
            cadena += mientras.Generar3D();
            return cadena;
        }

        private String GenerarHacer()
        {
            String cadena = "";
            FHacer hacer = (FHacer)Valor;
            cadena += hacer.Generar3D();
            return cadena;
        }

        private String GenerarRepetir()
        {
            String cadena = "";
            FRepetir repetir = (FRepetir)Valor;
            cadena += repetir.Generar3D();
            return cadena;
        }

        private String GenerarLoop()
        {
            String cadena = "";
            FLoop loop = (FLoop)Valor;
            cadena += loop.Generar3D();
            return cadena;
        }

        private String GenerarPara()
        {
            String cadena = "";
            FPara para = (FPara)Valor;
            cadena += para.Generar3D();
            return cadena;
        }

        private String GenerarX()
        {
            String cadena = "";
            FX para = (FX)Valor;
            cadena += para.Generar3D();
            return cadena;
        }

        //sentencias
        private String GenerarSi()
        {
            String cadena = "";
            FSi si = (FSi)Valor;
            cadena += si.Generar3D();
            return cadena;
        }

        private String GenerarElegir()
        {
            String cadena = "";
            FElegir elegir = (FElegir)Valor;
            cadena += elegir.Generar3D();
            return cadena;
        }

        //otros
        private String GenerarContinuar()
        {
            String cadena = "";
            cadena += "\t\t§continuar§;\n";
            return cadena;
        }

        private String GenerarSalir()
        {
            String cadena = "";
            cadena += "\t\t§salir§;\n";
            return cadena;
        }

        private String GenerarRetornar()
        {
            String cadena = "";
            Nodo3D val = ((FNodoExpresion)Valor).Generar3D();
            Simbolo metodopadre = BuscarMetodoPadre();
            FMetodo met = (FMetodo)metodopadre.Valor;
            String tipometodo = metodopadre.Tipo;

            if (met.Dimensiones > 0)
            {
                tipometodo = "arreglo " + tipometodo;
            }

            if (val.Tipo.Equals(Constante.TBooleano))
            {
                if (val.V == "" && val.F == "")
                {//si trae etiquetas viene de una relacional si no es un bool nativo

                }
                else
                {
                    var cad = "";

                    var auxtemp = TitusTools.GetTemp();
                    var salida = TitusTools.GetEtq();

                    cad += "\t" + val.V + ":\n";
                    cad += "\t\t" + auxtemp + " = 1;\n";
                    cad += "\t\t" + "goto " + salida + ";\n";
                    cad += "\t" + val.F + ":\n";
                    cad += "\t\t" + auxtemp + " = 0;\n";
                    cad += "\t" + salida + ":\n";

                    val.Valor = auxtemp;
                    cad += "\t\t" + val.Valor + " =  - " + auxtemp + ";\n";
                    val.Codigo = val.Codigo + cad;
                }
            }

            if (tipometodo.Equals(val.Tipo))
            {
                String retorno = TitusTools.GetTemp();
                cadena += val.Codigo;
                cadena += "\t\t" + retorno + " = P + 1;//Posicion de retorno\n";
                cadena += "\t\t" + "Stack[" + retorno + "] = " + val.Valor + ";//Asignacion de retorno\n";

                cadena += "\t\t§retornar§;\n";
            }
            else
            {
                TitusTools.InsertarError(Constante.TErrorSemantico, "El metodo " + met.Nombre + " solo puede retornar un tipo " + tipometodo + ", no un tipo " + val.Tipo, TitusTools.GetRuta() ,Fila, Columna);
            }

            
            return cadena;
        }

        private String GenerarImprimir()
        {
            String cadena = "";
            FImprimir imprimir = (FImprimir)Valor;
            cadena += imprimir.Generar3D();
            return cadena;
        }

        private String Generar3DAsignacion()
        {
            String cadena = "";
            FAsignacion asignacion = (FAsignacion)Valor;
            cadena += asignacion.Generar3D();
            return cadena;
        }


        //Codigo para busqueda de variables clases metodos y constructores

        public Simbolo BuscarClasePadre()
        {
            Simbolo aux = this;
            while (aux.Padre != null)
            {
                aux = aux.Padre;
            }

            return aux;
        }

        public Simbolo BuscarConstructor(String nombreClase, List<Nodo3D> parametros)
        {
            Simbolo padreclase = BuscarClasePadre();

            FClase pc = (FClase)padreclase.Valor;

            Simbolo clase = BuscarClase(nombreClase, pc.ArchivoPadre);

            if (clase != null)
            {
                Boolean encontrado = false;
                foreach (Simbolo c in clase.Ambito.TablaSimbolo)
                {
                    if (c.Rol.Equals(Constante.TConstructor) && !encontrado)
                    {
                        FMetodo metodo = (FMetodo)c.Valor;
                        if (metodo.Parametros.Count == parametros.Count)
                        {
                            Boolean error = false;
                            for (int i = 0; i < metodo.Parametros.Count; i++)
                            {
                                FParametro p = (FParametro)metodo.Parametros[i].Valor;
                                String tipometodo = p.Tipo;

                                String tipoparametro = parametros[i].Tipo;
                                if (p.Dimensiones > 0)
                                {
                                    tipometodo = "arreglo " + tipometodo;
                                }

                                if (!tipometodo.Equals(tipoparametro))
                                {
                                    error = true;
                                    break;
                                }
                            }
                            if (!error)
                            {
                                return c;
                            }
                        }
                    }
                }
                if (!encontrado)
                {
                    FMetodo m = new FMetodo(Constante.TPublico, Constante.TConstructor,0, clase.Nombre, new List<Simbolo>(), new Ambito(clase.Nombre),clase.Fila, clase.Columna);
                    return new Simbolo(m.Visibilidad, Constante.TConstructor, m.Nombre, Constante.TConstructor, m.Fila, m.Columna, m.Ambito, m);
                }
            }
            return null;
        }

        public Simbolo BuscarMetodoPadre()
        {
            Simbolo aux = this;
            while (aux.Padre != null && !aux.Ambito.Nombre.Equals("§global§"))
            {
                aux = aux.Padre;
            }

            return aux;
        }


        public Simbolo BuscarMetodo(String nombre, List<Nodo3D> parametros)
        {
            Simbolo padreclase = BuscarClasePadre();

            Boolean encontrado = false;

            foreach (Simbolo sim in padreclase.Ambito.TablaSimbolo)
            {
                if ((sim.Rol.Equals(Constante.TMetodo) || sim.Rol.Equals(Constante.TFuncion)) && sim.Nombre.Equals(nombre))
                {
                    FMetodo metodo = (FMetodo)sim.Valor;
                    if (metodo.Parametros.Count == parametros.Count)
                    {
                        Boolean error = false;
                        for (int i = 0; i < metodo.Parametros.Count; i++)
                        {
                            FParametro p = (FParametro)metodo.Parametros[i].Valor;
                            String tipometodo = p.Tipo;

                            String tipoparametro = parametros[i].Tipo;
                            if (p.Dimensiones > 0)
                            {
                                tipometodo = "arreglo " + tipometodo;
                            }

                            if (!tipometodo.Equals(tipoparametro))
                            {
                                error = true;
                                break;
                            }
                        }
                        if (!error)
                        {
                            return new Simbolo (sim);
                        }
                    }
                }
            }

            if (!encontrado)
            {
                Simbolo here = BuscarMetodoHerencia(nombre, parametros);

                if (here != null)
                {
                    Simbolo nuevo = new Simbolo(here);
                    nuevo.este += padreclase.Ambito.Tamaño;
                    here = nuevo;
                }

                return here;
            }
            return null;
        }

        public Simbolo BuscarMetodoHerencia(String nombre, List<Nodo3D> parametros)
        {
            Simbolo var = null;
            Simbolo sim = BuscarClasePadre();

            FClase clase = (FClase)sim.Valor;

            if (!clase.Herencia.Equals(""))
            {
                Simbolo bc = BuscarClase(clase.Herencia, clase.ArchivoPadre);
                if (bc != null)
                {
                    var = bc.BuscarMetodo(nombre, parametros);
                    if (var != null)
                    {
                        if (!var.Visibilidad.Equals(Constante.TPublico) && !var.Visibilidad.Equals(Constante.TProtegido))
                        {
                            var = null;
                        }
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro una clase para herederar con el nombre " + clase.Herencia, TitusTools.GetRuta(), sim.Fila, sim.Columna);
                }
            }

            return var;
        }

        public Simbolo BuscarVariable(String nombre)
        {
            Simbolo sim = null;

            Simbolo aux = Hermano;

            if (Hermano != null)
            {
                aux = Hermano;
            }else if (Padre != null)
            {
                aux = Padre;
            }
            while (aux != null)
            {
                if ((aux.Rol.Equals(Constante.PARAMETRO) || aux.Rol.Equals(Constante.DECLARACION)) && aux.Nombre.Equals(nombre))
                {
                    sim = new Simbolo(aux);
                    break;
                }
                if (aux.Hermano == null)
                {
                    aux = aux.Padre;
                }
                else
                {
                    aux = aux.Hermano;
                }                
            }
            //buscamos en todo el ambito global
            if (sim == null)
            {
                Simbolo global = BuscarClasePadre();

                foreach (Simbolo decla in global.Ambito.TablaSimbolo)
                {
                    if ((decla.Rol.Equals(Constante.PARAMETRO) || decla.Rol.Equals(Constante.DECLARACION)) && decla.Nombre.Equals(nombre))
                    {
                        sim = new  Simbolo(decla);
                        break;
                    }
                }
            }
            //buscamos en las herencias si no lo hemos encontrado
            if (sim == null)
            {
                sim = BuscarVariableHerencia(nombre);
            }
            return sim;
        }

        private Simbolo BuscarVariableHerencia(String nombre)
        {
            Simbolo var = null;
            Simbolo sim = BuscarClasePadre();

            FClase clase = (FClase)sim.Valor;

            if (!clase.Herencia.Equals(""))
            {
                Simbolo bc = BuscarClase(clase.Herencia, clase.ArchivoPadre);
                if (bc != null)
                {
                    var = bc.BuscarVariable(nombre);
                    if (var != null)
                    {
                        if (var.Visibilidad.Equals(Constante.TPublico) || var.Visibilidad.Equals(Constante.TProtegido))
                        {

                            var.Posicion += sim.Tamaño;
                        }
                        else
                        {
                            var = null;
                        }
                    }
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "No se encontro una clase para herederar con el nombre " + clase.Herencia, TitusTools.GetRuta(), sim.Fila, sim.Columna);
                }
            }

            return var;
        }

        public Simbolo BuscarClase(String nombre, Archivo archivo)
        {
            Simbolo aux = null;

            //primero buscamos en las clases locales

            foreach (Simbolo sim in archivo.Clases.TablaSimbolo)
            {
                if (sim.Nombre.Equals(nombre))
                {
                    aux = new Simbolo(sim);
                    break;
                }
            }

            //si no existe en el ambito de clases local las buscamos en los imports
            if (aux == null)
            {
                foreach(String import in archivo.Imports)
                {
                    if (aux != null)
                    {
                        break;
                    }
                    aux = BuscarClaseImports(nombre, import);
                }
            }

            return aux;
        }

        private Simbolo BuscarClaseImports(String nombre, String import)
        {
            Simbolo clase = null;
            //buscamos cada uno de los imports para ver si coinciden con el de los archivos
            foreach (Archivo archivo in TitusTools.Archivos_Importados)
            {
                //si coinciden los imports buscamos en sus clases locales
                if (archivo.Ruta.Equals(import))
                {
                    clase = BuscarClase(nombre, archivo);
                    break;
                }
            }

            return clase;
        }
    }
}
