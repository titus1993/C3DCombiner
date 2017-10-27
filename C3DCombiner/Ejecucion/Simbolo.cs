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
            }
            return cadena;
        }

        private String GenerarClase()
        {
            String cadena = "";
            foreach (Simbolo sim in Ambito.TablaSimbolo)
            {
                if (!sim.Rol.Equals(Constante.DECLARACION))
                {
                    cadena += sim.Generar3D();
                }
            }
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
            cadena += declaracion.Generar3D();
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

    }
}
