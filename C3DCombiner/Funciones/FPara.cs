using C3DCombiner.Ejecucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3DCombiner.Funciones
{
    class FPara
    {

        public Simbolo AccionAnterior;
        public Ambito Ambito;
        public FNodoExpresion Condicion;
        public Simbolo AccionSiguiente;

        public Simbolo Padre;

        public FPara(Simbolo AccionAnterior, FNodoExpresion Condicion, Simbolo AccionSiguiente, Ambito Ambito)
        {
            this.AccionAnterior = AccionAnterior;
            this.Condicion = Condicion;
            this.AccionSiguiente = AccionSiguiente;
            this.Ambito = Ambito;
            this.Padre = null;
        }


        public String Generar3D()
        {

            String cadena = "";

            String retorno = TitusTools.GetEtq();
            if (!TitusTools.HayErrores())
            {
                cadena = "\t\t//Comienza para\n";
                cadena += AccionAnterior.Generar3D();//asignacion de variables
                cadena += "\t" + retorno + ":\n";


                Nodo3D cond = Condicion.Generar3D();

                cadena += cond.Codigo;
                if (cond.Tipo == Constante.TBooleano)
                {
                    

                    if (cond.V == "" || cond.F == "")
                    {
                        cond.V = TitusTools.GetEtq();
                        cond.F = TitusTools.GetEtq();


                        cadena += "\t" + "if " + cond.Valor + " == 1 goto " + cond.V + ";\n";
                        cadena += "\t" + "goto " + cond.F + ";\n";

                    }
                    cadena += "\t" + cond.V + ":\n";
                    foreach (Simbolo sim in Ambito.TablaSimbolo)//cuerpo si es verdadero
                    {
                        cadena += sim.Generar3D();
                    }
                    cadena += AccionSiguiente.Generar3D();//actualizacion de varaibles   
                    cadena += "\t\t" + "goto " + retorno + ";\n";
                    cadena += "\t" + cond.F + "://Termina para\n";
                }
                else
                {
                    TitusTools.InsertarError(Constante.TErrorSemantico, "El ciclo for esperaba un tipo booleano no un tipo " + cond.Tipo, TitusTools.GetRuta(), Padre.Fila, Padre.Columna);
                }
            }

            return cadena;
        }
    }
}
