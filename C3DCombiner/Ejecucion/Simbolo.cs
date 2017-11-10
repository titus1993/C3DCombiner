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
            cadena += "\t\t§retornar§;\n"; 
            return cadena;
        }

        private String GenerarImprimir()
        {
            String cadena = "";
            FImprimir imprimir = (FImprimir)Valor;
            cadena += imprimir.Generar3D();
            return cadena;
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
                aux = Padre.Hermano;
            }
            while (aux != null)
            {
                if ((aux.Rol.Equals(Constante.PARAMETRO) || aux.Rol.Equals(Constante.DECLARACION)) && aux.Nombre.Equals(nombre))
                {
                    sim = aux;
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



            return sim;
        }

        public void BuscarVariableHerencia()
        {

        }
    }
}
